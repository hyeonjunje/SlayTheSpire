using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Pattern
{
    public EnemyPatternData patternData;
    public IndentData indentData;
    public int amount;
}


public class EnemyPattern : MonoBehaviour
{
    private Enemy _enemy;

    public Pattern enemyFirstPattern;
    public List<Pattern> enemyPatterns;
    public bool isFirstPattern = false;  // ���� ó�� ������ �ִ°�

    [SerializeField] private Image _patternImage;
    [SerializeField] private Text _patternText;

    private int _patternTurn = 1;
    private Pattern _currentPattern;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Act()
    {
        // ù��° �Ͽ� �� ������ ������ ����
        ActPattern();

        _patternTurn++;
    }

    public void DicidePattern()
    {
        // ���� �����ϰ� ui�� �����ֱ�
        // �� �� ���� ��������Ʈ�� �� �Լ��� �־���� ��

        // ù��° ������ �ִ� ���̸� ù��° ������ ����� ��
        // �ƴϸ� ����(�ϴ���...)
        if (_patternTurn == 1 && isFirstPattern)
        {
            _currentPattern = enemyFirstPattern;
        }
        else
        {
            _currentPattern = enemyPatterns[Random.Range(0, enemyPatterns.Count)];
        }

        _patternImage.sprite = _currentPattern.patternData.patternIcon;
        _patternText.text = GetPatternAmount();
    }

    private void ActPattern()
    {
        switch (_currentPattern.patternData.patternType)
        {
            case EPatternType.Attack:
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power);
                break;
            case EPatternType.Defense:
                _enemy.CharacterStat.Shield += _currentPattern.amount + _enemy.CharacterStat.Agility;
                break;
            case EPatternType.Debuff:
            case EPatternType.Buff:
                GetIndent();
                break;
        }
    }

    private void GetIndent()
    {
        switch (_currentPattern.indentData.indent)
        {
            case EIndent.Weakening:
                battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                battleManager.Player.indent[(int)EIndent.Weakening] = true;
                break;
            case EIndent.Weak:
                battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                battleManager.Player.indent[(int)EIndent.Weak] = true;
                break;
            case EIndent.Consciousness:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                _enemy.indent[(int)EIndent.Consciousness] = true;
                break;
            case EIndent.Frenzy:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                _enemy.indent[(int)EIndent.Frenzy] = true;
                break;
            case EIndent.Strength:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                _enemy.indent[(int)EIndent.Strength] = true;
                break;
        }
    }

    private string GetPatternAmount()
    {
        string result = "";

        switch (_currentPattern.patternData.patternType)
        {
            case EPatternType.Attack:
                result = (_currentPattern.amount + _enemy.CharacterStat.Power).ToString();
                break;
        }

        return result;
    }
}
