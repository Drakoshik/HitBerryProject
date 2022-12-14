using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private bool autoExpand;
    private GameObject container;
    private List<T> pool;

    public ObjectPool(T prefab, int count, Transform mainContainer, bool autoExpand)
    {
        this.prefab = prefab;
        // var empty = new GameObject();
        // empty.name = "kajhg";
        container = Object.Instantiate(new GameObject(), mainContainer);
        container.name = prefab.name;
        this.autoExpand = autoExpand;
        this.CreatePool(count, container.transform);
    }

    private void CreatePool(int count, Transform container)
    {
        this.pool = new List<T>();

        for (var i = 0; i < count; i++)
        {
            this.CreateObject(container);
        }

    }

    private T CreateObject(Transform container, bool isActive = false)
    {
        var createdObject = Object.Instantiate(this.prefab, container);
        createdObject.gameObject.SetActive(isActive);
        this.pool.Add(createdObject);
        return createdObject;
    }


    public T GetFreeElement()
    {
        foreach (var obj in pool.Where(obj =>
                     !obj.gameObject.activeInHierarchy))
        {
            obj.gameObject.SetActive(true);
            return obj;
        }
            
        
        if (this.autoExpand)
            return this.CreateObject(container.transform, true);
        
        throw new Exception("No free elements");
    }

    public void HideAllElements()
    {
        foreach (var obj in pool)
        {
            obj.gameObject.SetActive(false);
        }
    }
    

}
