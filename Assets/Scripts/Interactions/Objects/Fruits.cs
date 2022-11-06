using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpAnimation))]
public class Fruits : MonoBehaviour
{
    [SerializeField] private Color _fruitColor;

    public Color GetColor()
    {
        return _fruitColor;
    }
}
