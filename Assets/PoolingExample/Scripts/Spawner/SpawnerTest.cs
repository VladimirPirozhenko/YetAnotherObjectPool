using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class SpawnerTest : MonoBehaviour
{
    //[SerializeField] private Capsule capsulePrefab;
   // [SerializeField] private PoolingGameObject basicPrefab;
   // [SerializeField] private CapsulePool capsulePool;
    [SerializeField] private int initialSpawnedElements;
    [SerializeField] private Vector3 spawnPosition;
    private CapsulePool codePool;
    private PoolingGameObjectPool basicCodePool;
    private List<PoolingGameObjectPool> basicCodePools;
    private void Start()
    {
        // codePool = CapsulePool.Create<CapsulePool>(capsulePrefab, 1000);
        // basicCodePool = PoolingGameObjectPool.Create<PoolingGameObjectPool>(basicPrefab, 1000);
        basicCodePools = PoolUtils.CreatePoolsFromPrefabFolder<PoolingGameObjectPool,PoolingGameObject>("Prefabs/PoolableObjects",1000);
       // codePool = PoolUtils.CreatePoolFromPrefabPath<CapsulePool,Capsule>("Prefabs/PoolableObjects/Capsule",1000);
        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int i = 0; i < initialSpawnedElements; i++)
        {
            foreach (var pool in basicCodePools) 
            {
                pool.Spawn();
            }
            //codePool.Spawn(spawnPosition);
         //   basicCodePool.Spawn(spawnPosition);
           // capsulePool.Spawn(spawnPosition);
        }   
        sw.Stop();
        Debug.Log("Allocated " + initialSpawnedElements*2 + " objects in: " + sw.Elapsed.TotalSeconds + " Seconds, (" + sw.Elapsed.TotalMilliseconds + " ms).");
    }
    void Update()
    {
     
        if (Input.GetKey(KeyCode.Q))
        {
            codePool.Spawn(spawnPosition);
          //  basicCodePool.Spawn(spawnPosition);
           // capsulePool.Spawn(spawnPosition);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            codePool.Spawn(spawnPosition);
            //basicCodePool.ReturnAllElementsToPool();
            //capsulePool.ReturnAllElementsToPool();
        }
    }
}
