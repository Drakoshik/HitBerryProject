using System.Collections;
using UnityEngine;

public class Placing : MonoBehaviour
{
    [SerializeField] private CapAnimation _capAnimation;
    [SerializeField] private Transform _endJumpPoint;

    [SerializeField] private ObjectPooller.ObjectInfo.ObjectType _fruitType;
    

    private void OnEnable()
    {
        var fruit = ObjectPooller.Instance.GetObject(_fruitType);
        fruit.transform.position = transform.position;
        var fruitAnimationComponent = fruit.GetComponent<JumpAnimation>();
        fruitAnimationComponent.OnCreate(_endJumpPoint, _capAnimation);
        fruitAnimationComponent.spawnEvent.AddListener(SpawnNewFruit);
    }
    
    

    private void SpawnNewFruit()
    {
        StartCoroutine(CR_SpawnNewFruit());
    }
    
    private IEnumerator CR_SpawnNewFruit()
    {
        yield return new WaitForSeconds(.5f);
        var fruit = ObjectPooller.Instance.GetObject(_fruitType);
        fruit.transform.position = transform.position;
        var fruitAnimationComponent = fruit.GetComponent<JumpAnimation>();
        fruitAnimationComponent.OnCreate(_endJumpPoint, _capAnimation);
        fruitAnimationComponent.spawnEvent.AddListener(SpawnNewFruit);

    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            ObjectPooller.Instance.HideObject(child.gameObject);
        }
    }
}
