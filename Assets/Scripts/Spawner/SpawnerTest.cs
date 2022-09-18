using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField] private PoolingGameObjectPool pool; 
    [SerializeField] private int initialSpawnedElements;
    [SerializeField] private Vector3 spawnPosition;
 
    private void Start()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

     
        for (int i = 0; i < initialSpawnedElements; i++)
        {
            pool.Spawn(spawnPosition);
        }   
        sw.Stop();
        Debug.Log("Allocated " + initialSpawnedElements + " objects in: " + sw.Elapsed.TotalSeconds + " Seconds, (" + sw.Elapsed.TotalMilliseconds + " ms).");
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
