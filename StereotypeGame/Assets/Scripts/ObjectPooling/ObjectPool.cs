using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }

    public bool AutoExpand { get; set; }
    
    public Transform Container { get; }

    private List<T> pool;

    public ObjectPool(T prefab, int count)
    {
        Prefab = prefab;
        Container = null;

        CreatePool(count);
    }

    public ObjectPool(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;

        CreatePool(count);
    }



    private void CreatePool(int count)
    {
        pool = new List<T>();
        for (int i = 0; i<count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool IsActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(IsActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var mono in pool)
        {
            if(!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }          
        }
        element = null;
        return false;
    }
    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;
        if (AutoExpand)
            return CreateObject(true);

        throw new Exception("нет свободных элементов в пуле " + typeof(T));
    }
}
