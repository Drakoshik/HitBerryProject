using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlenderContentHandler : MonoBehaviour
{
    [SerializeField] private GameObject _fruitMixObject;
    [SerializeField] private List<GameObject> _fruitList;
    [SerializeField] private BlendButton _blendButton;
    
    private MeshRenderer _mainMeshRenderer;

    private bool _isFirstFruit = true;
    void Start()
    {
        _mainMeshRenderer = _fruitMixObject.GetComponent<MeshRenderer>();
        _blendButton.raycastReceivedEvent.AddListener(Mix);
        _fruitList = new List<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Fruits>()) return;
        print("fruit own");
        if (_isFirstFruit)
        {
            _mainMeshRenderer.material.color = other.GetComponent<Fruits>().GetColor();
            _isFirstFruit = false;
            
        }
        else
        {
            _mainMeshRenderer.material.color += other.GetComponent<Fruits>().GetColor();
        }
        
        _fruitList.Add(other.gameObject);
    }


    private void Mix()
    {
        print("YES YES YES YES YES!!!!!!!");
        if(_fruitList.Count<= 0) return;
        foreach (var fruit in _fruitList)
        {
            fruit.SetActive(false);
        }
        _fruitMixObject.SetActive(true);
        _blendButton.raycastReceivedEvent.RemoveAllListeners();
        StartCoroutine(CR_CheckWin());
    }

    private IEnumerator CR_CheckWin()
    {
        yield return new WaitForSeconds(2f);
        GameProcessData.Instance.IsWin(_mainMeshRenderer.material.color);
        _fruitMixObject.SetActive(false);
    }
    
}

