using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum SkillType
{
    None,
    ParticleEffect,//laser, onda, explosao, choque, raio, etcs
    Attack, //tipo a cabecada
    Projectile,
    Movement,
    Transition
}
[CreateAssetMenu(fileName = "Boss Skill", menuName = "Scriptables/Boss Skills", order = 5)]
public class BossSkillsScriptable : ScriptableObject
{
    [Header("Tempo de Preparo da Proxima Skill")]
    public float prepareTime;

    [Header("Skills da Fase 1")]
    public List<BossSkill> firstPhaseSkills = new List<BossSkill>();

    [Header("Skills da Fase 2")]
    public List<BossSkill> secondPhaseSkills = new List<BossSkill>();
}
[System.Serializable] //mandinga
public class BossSkill
{
    public SkillType skillType = SkillType.None;
    public string skillName;//laser, onda, mísseis, cabeçada, etcs

    public GameObject effectPrefab; //caso o ataque tenha algum efeito que precise ser instanciado
    public float effectDelay = 0f;

    public PlayableAsset skillAnimation;//animacao do ataque
    
    public float skillDamage;//dano da skill
    public bool rectangularCollision = false; //configura o tipo de colisao da skill, se e esfera ou cubo

    //intevalo de valores que sao a chance da skill //não é uma porcentagem
    public float skillChance;
}
