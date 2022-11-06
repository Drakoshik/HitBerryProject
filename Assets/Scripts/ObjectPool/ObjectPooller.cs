using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooller : Singleton<ObjectPooller>
{
    
    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            Apple,
            Banana,
            Cherries,
            Coconuts,
            GreenApple,
            Eggplant,
            Orange,
            Tomato
        }

        public ObjectType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [SerializeField] private List<ObjectInfo> objectsInfo;

    private Dictionary<ObjectInfo.ObjectType, Pool> pools;

    public override void Awake()
    {
        base.Awake();
        
        InitPool();
    }

    private void InitPool()
    {
        pools = new Dictionary<ObjectInfo.ObjectType, Pool>();
        var empty = new GameObject();

        foreach (var obj in objectsInfo)
        {
            var container = Instantiate(empty, transform, false);
            container.name = obj.Type.ToString();

            pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var go = InstantiateObject(obj.Type, container.transform);
                pools[obj.Type].Objects.Enqueue(go);
            }

        }
        Destroy(empty);
    }

    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        var go = Instantiate(objectsInfo.Find(x => x.Type == type).Prefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectInfo.ObjectType type)
    {
        var obj = pools[type].Objects.Count > 0
            ? pools[type].Objects.Dequeue()
            : InstantiateObject(type, pools[type].Container);
        
        obj.SetActive(true);
        return obj;
    }
    
    public void DestroyObject(GameObject obj){
        pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
        
    }
    
}
