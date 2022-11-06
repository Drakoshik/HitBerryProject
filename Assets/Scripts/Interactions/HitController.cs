using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay((Input.mousePosition));
            if (Physics.Raycast(ray, out var hitObject))
            {
                if(hitObject.collider.gameObject.GetComponent<IHitHendler>() == null) return;
                print("slkjdhtlnsjhtglnsnd");
                hitObject.collider.gameObject.GetComponent<IHitHendler>().OnRaycastReceived();
            }
        }
    }
}
