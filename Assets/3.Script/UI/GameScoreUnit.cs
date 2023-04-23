using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreUnit : MonoBehaviour
{
    [SerializeField]
    private Text _scoreTitle, _score;

    public void Init(string scoreTitle, string score)
    {
        _scoreTitle.text = scoreTitle;
        _score.text = score;
    }
}
