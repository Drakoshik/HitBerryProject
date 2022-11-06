using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject 
{
    ObjectPooller.ObjectInfo.ObjectType Type { get; }
}
