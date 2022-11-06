using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderContentHandler : MonoBehaviour
{
    [SerializeField] private GameObject _fruitMixObject;
    
    private MeshRenderer _mainMeshRenderer;

    private bool _isFirstFruit = true;
    void Start()
    {
        _mainMeshRenderer = _fruitMixObject.GetComponent<MeshRenderer>();
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
    }
    
    public float CompareColor (Color colorA, Color colorB)
    {

        float r = Mathf.Abs(colorA.r - colorB.r);
        float g = Mathf.Abs(colorA.g - colorB.g);
        float b = Mathf.Abs(colorA.b - colorB.b);

        return 100 - (((r + g + b) / 3) * 100);
    }

}
