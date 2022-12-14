using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pooller : MonoBehaviour
{
    [SerializeField] private Fruits prefab;
    [SerializeField] private CapAnimation capAnimation;
    [SerializeField] private Transform endJumpPoint;

    private ObjectPool<Fruits> pool;
    private Fruits currentFruit;
    

    private void OnEnable()
    {
        pool ??= new ObjectPool<Fruits>(prefab, 5, this.transform, true);
        var fruit = pool.GetFreeElement();
        currentFruit = fruit;
        
        fruit.transform.position = transform.position;
        var fruitAnimationComponent = fruit.GetComponent<JumpAnimation>();
        fruitAnimationComponent.OnCreate(endJumpPoint, capAnimation);
        fruitAnimationComponent.spawnEvent.AddListener(SpawnNewFruit);
    }
    
    

    private void SpawnNewFruit()
    {
        StartCoroutine(CR_SpawnNewFruit());
    }
    
    private IEnumerator CR_SpawnNewFruit()
    {
        var fruitAnimationComponent = currentFruit.GetComponent<JumpAnimation>();
        fruitAnimationComponent.spawnEvent.RemoveAllListeners();
        yield return new WaitForSeconds(.5f);
        var fruit = pool.GetFreeElement();
        currentFruit = fruit;
        fruit.transform.position = transform.position;
        fruitAnimationComponent = fruit.GetComponent<JumpAnimation>();
        fruitAnimationComponent.OnCreate(endJumpPoint, capAnimation);
        fruitAnimationComponent.spawnEvent.AddListener(SpawnNewFruit);

    }

    private void OnDisable()
    {
        pool.HideAllElements();
    }
    
    
    
}
