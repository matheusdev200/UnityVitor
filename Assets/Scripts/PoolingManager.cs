using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolingManager : MonoBehaviour
{
    public static List<PooledObjectInfo> Pools = new List<PooledObjectInfo>();
    
    public static GameObject SpawnObject(GameObject objectPrefab, Vector3 spawnPosition, Quaternion spawnRotation)//substitui Instantiate();
    {
        PooledObjectInfo pool = Pools.Find(p => p.PoolName == objectPrefab.name); //express�o lambda
                                                                                  //express�o lambda simplifica um la�o de repeti��o numa lista procurando por algo em um if e interrompe
                                                                                  //a procura na hora que achar com o comando de break
        //PooledObjectInfo pool = null;
        //foreach (PooledObjectInfo p in Pools)
        //{
        //    if (p.PoolName == objectPrefab.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}
        //se n�o existir uma Piscina de objetos com esse nome, cria uma
        if (pool == null)
        {
            //pool = new PooledObjectInfo() { LookupString = objectPrefab.name + "(Clone)" };
            pool = new PooledObjectInfo() { PoolName = objectPrefab.name };
            Pools.Add(pool);
        }

        //verifica se tem algum objeto inativo na nossa piscina de objetos
        GameObject possibleObjectToSpawn = pool.InactiveObjects.FirstOrDefault(); //faz a mesma coisa do conjunto debaixo
                                                                                  //s� que utilizando uma fun��o da biblioteca System.Linq, que pega o primeiro objeto que ele achar daquele tipo v�lido
                                                                                  //que no caso � qualquer objeto inativo, e usa ele.

        //GameObject possibleObjectToSpawn = null;
        //foreach (GameObject obj in pool.InactiveObjects)
        //{
        //    if (obj != null)
        //    {
        //        possibleObjectToSpawn = obj;
        //        break;//freio
        //    }
        //}

        //se n�o tiver um objeto v�lido na piscina, adiciona um l�
        if (possibleObjectToSpawn == null)
        {
            possibleObjectToSpawn = Instantiate(objectPrefab, spawnPosition, spawnRotation);
        }
        else
        {
            possibleObjectToSpawn.transform.position = spawnPosition;
            possibleObjectToSpawn.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(possibleObjectToSpawn);
            possibleObjectToSpawn.SetActive(true);
        }

        return possibleObjectToSpawn;
    }
    //go de game object
    public static void ReturnObjectToPool(GameObject go)//substitui Destroy();
    {
        //PooledObjectInfo pool = Pools.Find(p => p.LookupString == go.name + "(Clone)");
        PooledObjectInfo pool = Pools.Find(p => $"{p.PoolName}(Clone)" == go.name);
        //no prefab -> sem clone //na cena -> com (Clone)
        if (pool == null)
        {
            Debug.Log("T� tentando voltar pra piscina um objeto que n�o � da piscina");
            //Destroy(go);
        }
        else
        {
            go.SetActive(false);
            pool.InactiveObjects.Add(go);
        }
    }
}
//abordagem do .net (C#) raiz
public class PooledObjectInfo //cada um � um pool novo
{
    public string PoolName;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}



// 1 cria a lista de piscinas
// 2 espera alguem pedir para Instanciar um objeto
// 3 cria a piscina correspondente pra cada objeto
// 4 usa a piscina quando pedimos o objeto
// 5 quando o objeto tem que voltar pra piscina, acha a piscina dele
// 6 devolve ele pra piscina.