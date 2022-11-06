using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

public class BlendButton : MonoBehaviour, IHitHendler
{
    public UnityEvent raycastReceivedEvent;

    public void OnRaycastReceived()
    {
        raycastReceivedEvent?.Invoke();
    }

}
