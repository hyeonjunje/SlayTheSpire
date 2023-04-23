using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreUI : BaseUI
{
    [SerializeField] private Text _gameScoreText;
    [SerializeField] private Transform _scoreUnitParent;
    [SerializeField] private GameScoreUnit _scoreUnitPrefab;
    [SerializeField] private GameObject _scoreBar;

    // 오른 층 수
    // 처치한 적
    // 엘리트 처치
    // 보스 처치
    // 바
    // 총점

    public void GameOver()
    {
        _gameScoreText.text = "완패";
    }

    public void GameClear()
    {
        _gameScoreText.text = "승리";
    }

    public override void Show()
    {
        base.Show();

        _scoreUnitParent.DestroyAllChild();

        GameScoreUnit scoreUnit1 = Instantiate(_scoreUnitPrefab, _scoreUnitParent);
        GameScoreUnit scoreUnit2 = Instantiate(_scoreUnitPrefab, _scoreUnitParent);
        GameScoreUnit scoreUnit3 = Instantiate(_scoreUnitPrefab, _scoreUnitParent);
        GameScoreUnit scoreUnit4 = Instantiate(_scoreUnitPrefab, _scoreUnitParent);
        GameObject scoreBar = Instantiate(_scoreBar, _scoreUnitParent);
        GameScoreUnit scoreResult = Instantiate(_scoreUnitPrefab, _scoreUnitParent);

        int sum = 0;
        sum += GameManager.Game.height * 5;
        sum += GameManager.Game.defeatCommonEnemy * 10;
        sum += GameManager.Game.defeatElite * 50;
        sum += GameManager.Game.defeatBoss * 100;

        scoreUnit1.Init("오른 층 수 (" + GameManager.Game.height + ")", (GameManager.Game.height * 5).ToString());
        scoreUnit2.Init("처치한 적 (" + GameManager.Game.defeatCommonEnemy + ")", (GameManager.Game.defeatCommonEnemy * 10).ToString());
        scoreUnit3.Init("엘리트 처치 (" + GameManager.Game.defeatElite + ")", (GameManager.Game.defeatElite * 50).ToString());
        scoreUnit4.Init("보스 처치 (" + GameManager.Game.defeatBoss + ")", (GameManager.Game.defeatBoss * 100).ToString());

        scoreResult.Init("점수", sum.ToString());
    }

    public override void Hide()
    {
        base.Hide();
    }
}
