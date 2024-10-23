using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum BossPhases
{
    Fase1,
    Fase2
}

public class BossBehaviour : MonoBehaviour, IBoss
{
    [Header("Vida Inicial do Boss")]
    public int maxHP;
    int currentHP; // COM METADE DOS PONTOS DE VIDA, TROCA DE FASE
    public BossPhases currentPhase = BossPhases.Fase1;

    [Header("Scriptable com os Ataques do Boss")]
    public BossSkillsScriptable mySkills;
    List<BossSkill> myCurrentSkills = new List<BossSkill>(); //as skills que ele esta usando no momento
    BossSkill lastUsedSkill; //variavel que salva qual a ultima skill que foi
                             //usada pra impedir de repetir skill
    List<BossSkill> grimoire = new List<BossSkill>(); //:)

    [Header("Playable Director")]
    public PlayableDirector bossDirector;

    //propriedades de combate
    [SerializeField] float prepareTime; // tempo de preparo entre skills
    public bool canTakeDamage; // variavel que habilita se ele pode receber dano
    public bool skillTerminou = false;

    public float currentDamage;

    [Header("Animações Especiais")]
    public PlayableAsset deathAnimation; //timeline de animacao de morte do boss

    //upgrade -> sistema de peso na porcentagem do sorteio das skills

    void OnEnable()
    {
        EventsMaster.OnStartBossFight += StartBossFight;
    }
    void OnDisable()
    {
        EventsMaster.OnStartBossFight -= StartBossFight;
    }
    void Start()
    {
        currentHP = maxHP; // ajeita os pontos de vida pra comecarem no maximo
        prepareTime = mySkills.prepareTime;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventsMaster.OnStartBossFight?.Invoke();
        }
    }
    public void TakeDamage(int d)//causa dano
    {
        currentHP -= d;

        if (currentHP <= maxHP / 2 && currentPhase == BossPhases.Fase1)
        {
            //muda para a fase 2
            currentPhase = BossPhases.Fase2;
            //altera o que precisar das mecanicas
            myCurrentSkills.Clear();//apagar tudo que tem na lista
            myCurrentSkills = mySkills.secondPhaseSkills;//troca a lista de skills
        }

        //incorporar a bool que deixa ele tomar dano ou nao

        if (currentHP <= 0)
        {
            //foi de arrasta
            StartCoroutine(BossDeathRoutine());
        }
    }
    public void StartBossFight()
    {
        //anota a lista de skills
        myCurrentSkills = mySkills.firstPhaseSkills;
        StartCoroutine(BossFightRoutine());
    }
    public void EndBossBattle()
    {
        StopCoroutine(BossFightRoutine());//para o comportamento do boss em batalha
        EventsMaster.OnFinishBossFight?.Invoke();
    }
    #region Comportamento do Boss em Batalha
    IEnumerator BossFightRoutine()
    {
        BossSkill nextSkill = null;
        skillTerminou = false;
        //talvez mande tocar a animacao de voltando pra posicao inicial

        //prepara para atacar
        yield return new WaitForSeconds(prepareTime);
        //depois de preparar ele escolhe uma skill

        //pega uma skill com o numero sorteado da lista das skills da fase que esta
        nextSkill = DrawSkill();

        if (nextSkill != null)
        {
            //se nao deu nenhum espirito de zica, entao tem uma skill pra usar
            StartCoroutine(ActivateSkill(nextSkill)); //usa a skill que foi sorteada
        }
        else
        {
            Debug.LogWarning("NAO ACHOU UMA SKILL PRA USAR :(");
        }
        //aguardar a skill terminar (pode ser um signal)
        yield return new WaitUntil(() => skillTerminou == true);
        StartCoroutine(BossFightRoutine());
    }

    //Rotina que dispara a skill do boss
    public IEnumerator ActivateSkill(BossSkill currentSkill)
    {
        currentDamage = currentSkill.skillDamage; // anota o dano da skill
        bossDirector.playableAsset = currentSkill.skillAnimation; //entrega a animacao da skill pro director
        bossDirector.Play();//manda tocar a animacao da skill

        yield return new WaitForSeconds(currentSkill.effectDelay);

        //upgrade: colocar os efeitos instanciaveis do boss no sistema de pooling
        //Instantiate(currentSkill.effectPrefab);
        //oto upgrade: vfx que for projetil ja vir com script de projetil (pra amarrar tambem no pooling)
        yield return new WaitForSeconds((float)bossDirector.playableAsset.duration);//espera a animacao tocar o tempo completo dela

        skillTerminou = true; //avisa que a skill terminou tudo que ela tinha que fazer
    }
    //upgrade: interrupcao do bagulho todo pra quando o boss tomar uma determinada quantidade de dano
    //ele mudar durante alguns segundos pra um estado mais vulneravel (ex: atirar no pocoto ate ele cair
    //em cima das patas e expor a cabeca na altura que o player consegue atirar)
    IEnumerator BossDeathRoutine()
    {
        bossDirector.playableAsset = deathAnimation;//manda tocar a animacao de morte e
                                                    //pro player nao tomar dano encostando nele
        EndBossBattle(); // chama o evento que acaba com a luta
        yield return null;
    }
    #endregion Comportamento do Boss em Batalha

    public BossSkill DrawSkill()
    {
        BossSkill drawnSkill = null;
        grimoire.Clear();

        float chanceTotal = 0f; //quanto vale todos os pesos somados
        float chance = 0f; //o numero que eu vou sortear (o bingo)

        foreach (BossSkill magia in myCurrentSkills)
        {
            grimoire.Add(magia);//escreve o grimorio do zero
        }

        if (lastUsedSkill != null)//testa se existe uma skill que nao pode sair nesta escolha
        {
            //Debug.Log($"A última skill usada foi <b>{lastUsedSkill.skillName}</b>");
            grimoire.Remove(lastUsedSkill);
        }

        for (int i = 0; i < grimoire.Count; i++) //passar em todas as skills
        {
            chanceTotal += grimoire[i].skillChance;//soma a chance delas no total
        }

        //sorteia um número pra chance que ta valendo
        chance = Random.Range(0, chanceTotal);

        //percorre a lista, comparando as skills com a chance que foi escolhida
        for (int i = 0; i < grimoire.Count; i++)
        {
            float chanceDaSkill = grimoire[i].skillChance;//anota a chance da skill na posicao i

            if (chance < chanceDaSkill)
            {
                //Debug.Log($"Sorteei a skill <b>{grimoire[i].skillName}</b>");
                drawnSkill = grimoire[i];//devolve a skill
                lastUsedSkill = grimoire[i];
                return drawnSkill;
            }
            //Control K+D
            chance -= chanceDaSkill;
        }
        return drawnSkill;
    }
}