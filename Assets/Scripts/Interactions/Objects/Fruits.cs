using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpAnimation))]
public class Fruits : MonoBehaviour, IPooledObject
{
    public ObjectPooller.ObjectInfo.ObjectType Type => type;

    [SerializeField] private ObjectPooller.ObjectInfo.ObjectType type;
    
    [SerializeField] private Color _fruitColor;
    private bool _used = false;

    public Color GetColor()
    {
        _used = true;
        return _fruitColor;
    }
    
    
    public bool GetIsUsed()
    {
        return _used;
    }

    
}
