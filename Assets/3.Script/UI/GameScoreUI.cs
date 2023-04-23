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

    // ���� �� ��
    // óġ�� ��
    // ����Ʈ óġ
    // ���� óġ
    // ��
    // ����

    public void GameOver()
    {
        _gameScoreText.text = "����";
    }

    public void GameClear()
    {
        _gameScoreText.text = "�¸�";
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

        scoreUnit1.Init("���� �� �� (" + GameManager.Game.height + ")", (GameManager.Game.height * 5).ToString());
        scoreUnit2.Init("óġ�� �� (" + GameManager.Game.defeatCommonEnemy + ")", (GameManager.Game.defeatCommonEnemy * 10).ToString());
        scoreUnit3.Init("����Ʈ óġ (" + GameManager.Game.defeatElite + ")", (GameManager.Game.defeatElite * 50).ToString());
        scoreUnit4.Init("���� óġ (" + GameManager.Game.defeatBoss + ")", (GameManager.Game.defeatBoss * 100).ToString());

        scoreResult.Init("����", sum.ToString());
    }

    public override void Hide()
    {
        base.Hide();
    }
}
