using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;
public class SpawnerTest : MonoBehaviour
{
    [SerializeField] private PoolingGameObjectPool pool; 
    [SerializeField] private int initialSpawnedElements;
    [SerializeField] private Vector3 spawnPosition;

    private void Start()
    {
        for (int i = 0; i < initialSpawnedElements; i++)
        {
            pool.Spawn(spawnPosition);
        }
    }
    void Update()
    {
     
        if (Input.GetKey(KeyCode.Q))
        {
            pool.Spawn(spawnPosition);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            pool.ReturnAllElementsToPool();
        }
    }
}
