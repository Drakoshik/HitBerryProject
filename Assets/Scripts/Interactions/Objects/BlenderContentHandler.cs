using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BlenderContentHandler : MonoBehaviour
{
    [SerializeField] private GameObject _fruitMixObject;
    [SerializeField] private List<GameObject> _fruitList;
    [SerializeField] private BlendButton _blendButton;

    [SerializeField] private BlenderGlassAnimation _glass;
    [SerializeField] private FruitMixCylinderAnimation _cylinder;
    [SerializeField] private CapAnimation _capHandler;
    
    private MeshRenderer _mainMeshRenderer;

    private bool _isFirstFruit = true;
    void Start()
    {
        _mainMeshRenderer = _fruitMixObject.GetComponent<MeshRenderer>();
        _blendButton.raycastReceivedEvent.AddListener(Mix);
        _fruitList = new List<GameObject>();
        // _capHandler.Open();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Fruits>()) return;
        _glass.PutInShake();

        if (_isFirstFruit)
        {
            _mainMeshRenderer.material.color = other.GetComponent<Fruits>().GetColor();
            _isFirstFruit = false;
            
        }
        else
        {
            if(!other.GetComponent<Fruits>().GetIsUsed())
                _mainMeshRenderer.material.color =
                    (_mainMeshRenderer.material.color + other.GetComponent<Fruits>().GetColor()) / 2;
        }
        
        _fruitList.Add(other.gameObject);
    }


    private void Mix()
    {
        if(_fruitList.Count<= 0) return;
        foreach (var fruit in _fruitList)
        {
            ObjectPooller.Instance.DestroyObject(fruit);
        }
        _fruitMixObject.SetActive(true);
        _cylinder.Shake();
        _glass.Shake();
        
        _blendButton.raycastReceivedEvent.RemoveAllListeners();
        GameProcessData.Instance.DisableHits();
        StartCoroutine(CR_CheckWin());
    }

    private IEnumerator CR_CheckWin()
    {
        yield return new WaitForSeconds(2f);
        GameProcessData.Instance.IsWin(_mainMeshRenderer.material.color);
    }
    
}

