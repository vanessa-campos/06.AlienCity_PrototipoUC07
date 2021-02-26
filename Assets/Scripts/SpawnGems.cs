using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGems : MonoBehaviour
{
    [SerializeField] GameObject Gema;
    [SerializeField] Transform[] SpawnPontos = new Transform[6];
    GameObject newGema;

    void Start()
    {
        RandomSpawn();RandomSpawn();RandomSpawn();       
    }

    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("gema").Length != 3)
        {
            RandomSpawn();
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("gema"))
        {
            if(item.transform.position == newGema.transform.position)
            {
                RandomSpawn();
            }
        }        
    }

    void RandomSpawn()    
    {
        newGema = Instantiate(Gema, SpawnPontos[Random.Range(0, 6)].position, Quaternion.identity);
        Destroy(newGema, Random.Range(3,7));           
    }

    /*
    void RandomSpawn()
    {
        //Sorteia um número para selecionar um dos pontos de spawn e instanciar uma gema nele
        int p1 = Random.Range(0, 6);
        Vector3 SpawnPosition = SpawnPontos[p1].position;
        GameObject newGema = Instantiate(Gema, SpawnPosition, Quaternion.identity);
        Destroy(newGema, Random.Range(3,7));

        //Sorteia um novo número e verifica se não é repetido para então selecionar o respectivo ponto de spawn e instanciar outra gema nele        
        int p2 = Random.Range(0, 6);
        do
        {
            p2 = Random.Range(0, 6);
        } while (p2 == p1);
        SpawnPosition = SpawnPontos[p2].position;
        newGema = Instantiate(Gema, SpawnPosition, Quaternion.identity);
        Destroy(newGema, Random.Range(3,7));
        int p3 = Random.Range(0, 6);
        do
        {
            p3 = Random.Range(0, 6);
        } while (p3 == p1 | p3 == p2);
        SpawnPosition = SpawnPontos[p3].position;
        newGema = Instantiate(Gema, SpawnPosition, Quaternion.identity);
        Destroy(newGema, Random.Range(3,7));
    }
    */
}
