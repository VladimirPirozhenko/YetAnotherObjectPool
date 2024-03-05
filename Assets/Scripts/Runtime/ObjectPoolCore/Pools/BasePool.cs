using Pools;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CreateablePoolsMonoBehaviour : MonoBehaviour
{
    private static GameObject parentObjectForPools;

    public static T Create<T>(GameObject gameObject = null) where T : MonoBehaviour
    {
        GameObject obj = gameObject;
        if (obj == null)
            obj = new GameObject(typeof(T).Name.ToString());
        if (parentObjectForPools == null)
            parentObjectForPools = new GameObject("Pools");
        obj.transform.SetParent(parentObjectForPools.transform);
        return obj.AddComponent<T>();
    }
}


public static class PoolUtils
{
    public static P CreatePoolFromPrefabPath<P, T>(string path, int capacity) where T : PoolingObject<T> where P : BasePool<T>
    {
        T prefab = Resources.Load<T>(path);
        if (prefab == null)
            throw new FileNotFoundException("No Poolable prefab found in " + path);

        P pool = BasePool<T>.Create<P>(prefab,capacity);
        return pool;
    }
    public static List<P> CreatePoolsFromPrefabFolder<P, T>(string path, int capacity) where T : PoolingObject<T> where P : BasePool<T>
    {
        var prefabList = Resources.LoadAll<T>(path).ToList();
        if (prefabList.Count == 0)
            throw new FileNotFoundException("No Poolable prefabs found in " + path + " folder");

        var poolList = new List<P>();
        foreach (var prefab in  prefabList)
        {
            P pool = BasePool<T>.Create<P>(prefab, capacity);
            poolList.Add(pool);
        }
        return poolList;
    }
}

public class BasePool<T> : CreateablePoolsMonoBehaviour where T : PoolingObject<T>
{
    public int Capacity
    { get { return pool.Capacity; } private set { capacity = value; } }
    public int InitialCapacity { get; private set; }

    [SerializeField] private int capacity = 100;
    [SerializeField] private T prefab;

    private ObjectPool<T> pool;

    private bool isAlreadyInit = false;

    private void InitInternal(int capacity)
    {
        this.capacity = capacity;
        InitialCapacity = capacity;
        pool = new ObjectPool<T>(CreateAction, GetAction, ReturnAction, DestroyAction, capacity);
        isAlreadyInit = true;
    }

    public static P Create<P>(T prefab, int capacity) where P : BasePool<T> 
    {
        P obj = Create<P>();
        obj.prefab = prefab;
        obj.InitInternal(capacity);
        return obj;
    }

    private void Awake()
    {
        if (isAlreadyInit || prefab == null)
            return;
        InitInternal(capacity);
    }

    #region Actions

    protected virtual T CreateAction()
    {
        T instance = Instantiate(prefab);
        instance.transform.SetParent(this.transform, false);
        instance.OwningPool = this;
        return instance;
    }

    protected virtual void GetAction(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    protected virtual void ReturnAction(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    protected virtual void DestroyAction(T instance)
    {
        Destroy(instance.gameObject);
    }

    #endregion Actions

    #region CountElements

    public int GetActiveElementCount()
    {
        return pool.GetActiveElementsCount();
    }

    public int GetInactiveElementCount()
    {
        return pool.GetInactiveElementsCount();
    }

    #endregion CountElements

    #region ContiansCheck

    public bool ContainsElement(T element)
    {
        return pool.ContainsElement(element);
    }

    public bool ContainsElement(T element, bool isActive)
    {
        return pool.ContainsElement(element, isActive);
    }

    #endregion ContiansCheck

    #region GetFromPool

    public T Spawn()
    {
        return pool.Get();
    }

    public T Spawn(Vector3 position)
    {
        T obj = pool.Get();
        obj.transform.position = position;
        return obj;
    }

    public bool TryGetFromPos(in Vector3 pos, out T element)
    {
        if (pool.TryGetFromPos(pos, out element))
        {
            return true;
        }
        return false;
    }

    public Component GetComponentFromPool<C>() where C : Component
    {
        return pool.GetComponentFromPool<C>();
    }

    public C GetComponentFromPool<C>(T obj) where C : Component
    {
        return pool.GetComponentFromPool<C>(obj);
    }

    #endregion GetFromPool

    #region ReturnToPool

    public void ReturnAllElementsToPool()
    {
        pool.ReturnAllElementsToPool();
    }

    public void ReturnToPool(T instance)
    {
        pool.ReturnToPool(instance);
    }

    #endregion ReturnToPool

    #region DestroyFromPool

    public void Destroy(T obj)
    {
        pool.Destroy(obj);
    }

    public void DestroyAllElements()
    {
        pool.DestroyAllElements();
    }

    public void DestroyAllElements(bool isActive)
    {
        pool.DestroyAllElements(isActive);
    }

    #endregion DestroyFromPool
}