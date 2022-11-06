using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessData : Singleton<GameProcessData>
{
    [SerializeField] private Color[] _winColors;

    private int currentLevel = 1;

    public Color GetCurrenColor()
    {
        return _winColors[currentLevel - 1];
    }
    public void IsWin(Color colorToCompare)
    {
        if (CompareColor(colorToCompare, GetCurrenColor())
            >= 85)
        {
            Debug.Log("you win");
            currentLevel++;
        }
        else
        {
            Debug.Log("you lose");
        }
    }

    private float CompareColor(Color colorA, Color colorB)
    {

        float r = Mathf.Abs(colorA.r - colorB.r);
        float g = Mathf.Abs(colorA.g - colorB.g);
        float b = Mathf.Abs(colorA.b - colorB.b);

        return 100 - (((r + g + b) / 3) * 100);

    }
}
