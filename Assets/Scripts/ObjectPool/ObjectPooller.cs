using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    [SerializeField] private List<ObjectInfo> _objectsInfo;

    private Dictionary<ObjectInfo.ObjectType, Pool> _pools;

    private List<GameObject> _containers;

    public override void Awake()
    {
        base.Awake();
        
        InitPool();
    }

    private void OnDisable()
    {
        print("sdlfrkghjsghsdklflbnlkjzdf");
    }

    private void InitPool()
    {
        _pools = new Dictionary<ObjectInfo.ObjectType, Pool>();
        var empty = new GameObject();
        _containers = new List<GameObject>();

        foreach (var obj in _objectsInfo)
        {
            var container = Instantiate(empty, transform, false);
            container.name = obj.Type.ToString();
            _containers.Add(container);

            _pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var go = InstantiateObject(obj.Type, container.transform);
                _pools[obj.Type].Objects.Enqueue(go);
            }

        }
        Destroy(empty);
    }

    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        var go = Instantiate(_objectsInfo.Find(x => x.Type == type).Prefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectInfo.ObjectType type)
    {
        var obj = _pools[type].Objects.Count > 0
            ? _pools[type].Objects.Dequeue()
            : InstantiateObject(type, _pools[type].Container);
        
        obj.SetActive(true);
        return obj;
    }
    
    public void DestroyObject(GameObject obj){
        _pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }

    public void DestroyAll()
    {
        foreach (var obj in _objectsInfo)
        {
            for (var i = 0; i < _containers[_objectsInfo.IndexOf(obj)].transform.childCount; i++)
            {
                var child = _containers[_objectsInfo.IndexOf(obj)].transform.GetChild(i);
                if (!child.gameObject.activeInHierarchy) continue;
                _pools[child.GetComponent<IPooledObject>().Type].Objects.Enqueue(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

    }
}
