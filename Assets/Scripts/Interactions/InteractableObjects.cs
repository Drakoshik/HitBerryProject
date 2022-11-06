using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{

    [SerializeField] private Camera _camera;

    private IHitHendler _objectInteractions;
    
    private Vector2 _touchPosition;
    
    private void Update()
    {
        if ((Input.touchCount <= 0 || !Input.GetMouseButtonUp(0))) return;

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            var ray = _camera.ScreenPointToRay((touch.position));
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                hitObject.collider.gameObject.GetComponent<IHitHendler>().OnRaycastReceived();
            }
        }
    }
    }
