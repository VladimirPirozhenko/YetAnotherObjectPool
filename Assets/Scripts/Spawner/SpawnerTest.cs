using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;
public class SpawnerTest : MonoBehaviour
{
    [SerializeField] private PoolingGameObjectPool pool;
    [SerializeField] private int MaxElements;
   
    void Update()
    {
        if (pool.Capacity > MaxElements)
            return;
        pool.Spawn(new Vector3(0, 40, 0));
    }
}
