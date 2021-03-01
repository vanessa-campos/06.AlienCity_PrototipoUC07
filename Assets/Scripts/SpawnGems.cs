using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGems : MonoBehaviour
{
    [SerializeField] GameObject Gema;
    [SerializeField] Transform[] SpawnPoints;
    GameObject[] Gemas;
    GameObject newGema;

    void Start()
    {
        int r;
        newGema = Instantiate(Gema, SpawnPoints[r = Random.Range(0, 6)].position, Quaternion.identity);
        Debug.Log(newGema.name + " no " + SpawnPoints[r].name);
        Destroy(newGema, Random.Range(3, 7));
        RandomSpawn();
        RandomSpawn();
    }

    void Update()
    {
        Gemas = GameObject.FindGameObjectsWithTag("gema");
        if (Gemas.Length != 3)
        {
            RandomSpawn();
        }
    }

    void RandomSpawn()
    {
        int point = Random.Range(0, 6);
        foreach (var item in Gemas)
        {
            if (item.transform.position == SpawnPoints[point].position)
            {
                while (item.transform.position == SpawnPoints[point].position)
                {
                    point = Random.Range(0, 6);
                }
            }
        }
        newGema = Instantiate(Gema, SpawnPoints[point].position, Quaternion.identity);
        Debug.Log(newGema.name + " no " + SpawnPoints[point].name);
        Destroy(newGema, Random.Range(3, 7));
    }
}

