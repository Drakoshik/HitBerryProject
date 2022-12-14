using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class GameProcess : Singleton<GameProcess>
{
    [SerializeField] private Color[] _winColors;
    [SerializeField] private HitController _hitController;

    [SerializeField] private ArrowAnimation _arrowButton;
    [SerializeField] private SpriteRenderer _colorToMix;
    
    [SerializeField] private List<GameObject> _spawns;

    [SerializeField] private List<GameObject> _level1;
    [SerializeField] private List<GameObject> _level2;
    [SerializeField] private List<GameObject> _level3;

    [SerializeField] private ResultPopupAnimation _resultPopup;
    
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _resetButton;

    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private TextMeshProUGUI _percentageText;

    [SerializeField] private Transform _buttonHolder;

    private Sequence _cameraSequence;
    private Sequence _resultpopupSequence;

    private int currentLevel = 1;

    public override void Awake()
    {
        base.Awake();
        ChangeFruitSet();
    }

    private void Start()
    {
        _arrowButton._button.onClick.AddListener(delegate { ShowColor(false); });
        _arrowButton.Show();
        SetCurrentColorForMix();
        _nextButton.onClick.AddListener(OnNextClick);
        _resetButton.onClick.AddListener(OnResetClick);
    }


    public void SetCurrentColorForMix()
    {
        _colorToMix.color = GetCurrenColor();
    }
    public Color GetCurrenColor()
    {
        if (currentLevel >= 4) currentLevel = 1;
        
        return _winColors[currentLevel - 1];
    }
    public void IsWin(Color colorToCompare)
    {
        DisableHits();
        if (Math.Round(CompareColor(colorToCompare, GetCurrenColor()), 0,
                MidpointRounding.ToEven)
            >= 85)
        {
            _nextButton.gameObject.SetActive(true);
            _resetButton.gameObject.SetActive(true);
            _percentageText.text = Math.Round(CompareColor(colorToCompare, GetCurrenColor()), 0,
                                       MidpointRounding.ToEven).ToString() + 
                                            "%";
            _resultText.text = "You WIN";
            _resultPopup.Show();
            ShowResult();
            
        }
        else
        {
            _nextButton.gameObject.SetActive(false);
            _resetButton.gameObject.SetActive(true);

            _percentageText.text = Math.Round(CompareColor(colorToCompare, GetCurrenColor()), 0,
                                        MidpointRounding.ToEven).ToString() + 
                                            "%";
            _resultText.text = "You LOSE";
            _resultPopup.Show();
            ShowResult();
        }
        
        
    }

    private void OnNextClick()
    {
        currentLevel++;
        SetCurrentColorForMix();
        ShowColor(true);
        _resultPopup.Hide();
        EnableHits();
    }
    private void OnResetClick()
    {
        ShowColor(true);
        _resultPopup.Hide();
        EnableHits();
    }

    public void DisableHits()
    {
        _hitController.gameObject.SetActive(false);
        _arrowButton._button.interactable = false;
    }
    public void EnableHits()
    {
        _hitController.gameObject.SetActive(true);
        _arrowButton._button.interactable = true;
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
        if (Camera.main != null)
        {
            _cameraSequence.Append(Camera.main.transform.DOLocalRotate(new Vector3(15f, -60f, 0f), 1f));
            _cameraSequence.Append(Camera.main.transform.DOLocalRotate(new Vector3(15f, -60f, 0f), 2f));
            _cameraSequence.AppendCallback((delegate
            {
                if (!isNext) return;
                ChangeFruitSet();
            }));
            _cameraSequence.Append(Camera.main.transform.DOLocalRotate(new Vector3(25f, 0f, 0f), 1f));
        }

        _cameraSequence.AppendCallback((delegate { _arrowButton.Show(); }));

    }

    private void ChangeFruitSet()
    {
        foreach (var obj in _spawns)
        {
            obj.SetActive(false);
        }

        switch (currentLevel)
        {
            case 1:
                foreach (var obj in _level1)
                {
                    obj.SetActive(true);
                }
                break;
            case 2:
                foreach (var obj in _level2)
                {
                    obj.SetActive(true);
                }
                break;
            case 3:
                foreach (var obj in _level3)
                {
                    obj.SetActive(true);
                }
                break;
        }
        
    }

    private void ShowResult()
    {
        _resultpopupSequence.Kill();
        _resultpopupSequence = DOTween.Sequence();

        _resultpopupSequence.Append(_percentageText.transform.DOScale(new Vector3(1, 1, 1),
            0.7f).From(Vector3.zero));
        _resultpopupSequence.Append(_resultText.transform.DOScale(new Vector3(1, 1, 1),
            0.7f).From(Vector3.zero));
        _resultpopupSequence.Join(_buttonHolder.DOScale(new Vector2(1, 1),
            0.7f).From(Vector2.zero));

    }
}
