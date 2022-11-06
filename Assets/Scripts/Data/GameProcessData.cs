using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameProcessData : Singleton<GameProcessData>
{
    [SerializeField] private Color[] _winColors;
    [SerializeField] private HitController _hitController;

    [SerializeField] private ArrowAnimation _arrowButton;

    private Sequence _cameraSequence;

    private int currentLevel = 1;

    private void Start()
    {
        _arrowButton._button.onClick.AddListener(delegate { ShowColor(false); });
        _arrowButton.Show();
    }

    public Color GetCurrenColor()
    {
        return _winColors[currentLevel - 1];
    }
    public void IsWin(Color colorToCompare)
    {
        print(currentLevel - 1);
        print(CompareColor(colorToCompare, GetCurrenColor()));
        if (CompareColor(colorToCompare, GetCurrenColor())
            >= 85)
        {
            Debug.Log("you win");
            currentLevel++;
            ShowColor(false);
        }
        else
        {
            Debug.Log("you lose");
        }
        
        
    }

    public void DisableHits()
    {
        _hitController.gameObject.SetActive(false);
    }

    private float CompareColor(Color colorA, Color colorB)
    {

        float r = Mathf.Abs(colorA.r - colorB.r);
        float g = Mathf.Abs(colorA.g - colorB.g);
        float b = Mathf.Abs(colorA.b - colorB.b);

        return 100 - (((r + g + b) / 3) * 100);

    }

    private void ShowColor(bool isNext)
    {
        _cameraSequence.Kill();
        _cameraSequence = DOTween.Sequence();
        _cameraSequence.AppendCallback((delegate { _arrowButton.Hide(); }));
        _cameraSequence.Append(Camera.main.transform.DOLocalRotate(new Vector3(15f, -80f, 0f), 1f));
        _cameraSequence.Append(Camera.main.transform.DOLocalRotate(new Vector3(15f, -80f, 0f), 2f));
        _cameraSequence.AppendCallback((delegate
        {
            if(!isNext) return;
            
        }));
        _cameraSequence.Append(Camera.main.transform.DOLocalRotate(new Vector3(15f, 0f, 0f), 1f));
        _cameraSequence.AppendCallback((delegate { _arrowButton.Show(); }));

    }
}
