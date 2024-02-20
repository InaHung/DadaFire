using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public IPoolable prefab;
    public int initailCount = 20;
    private Queue<IPoolable> objectPool = new Queue<IPoolable>();

    void Awake()
    {
        if (prefab != null)
            InitPool();
    }

    public void InitPool()
    {
        for (int i = 0; i < initailCount; i++)
        {
            IPoolable prefabObject = Instantiate(prefab);
            prefabObject.SetupPool(this);
            prefabObject.transform.SetParent(transform);
            objectPool.Enqueue(prefabObject);
            prefabObject.gameObject.SetActive(false);
        }
    }


    public IPoolable GetObject()
    {
        if (objectPool.Count > 0)
        {
            IPoolable obj = objectPool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            IPoolable obj = Instantiate(prefab);
            obj.SetupPool(this);
            obj.gameObject.SetActive(true);
            return obj;
        }


    }
    public void RecyclePoolObject(IPoolable obj)
    {
        obj.gameObject.SetActive(false);
        objectPool.Enqueue(obj);

    }

    public void SetPrefab(IPoolable iPoolable)
    {
        prefab = iPoolable;
    }
    
}
