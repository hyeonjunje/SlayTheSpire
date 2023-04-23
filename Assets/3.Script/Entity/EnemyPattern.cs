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
    public int secondAmount;
}


public class EnemyPattern : MonoBehaviour
{
    private Enemy _enemy;

    public Pattern alreadyPattern;  // �̹� �ִ� ����
    public Pattern enemyFirstPattern;  // ���� ó�� ����
    public List<Pattern> enemyPatterns;  // �� �ܿ� ����
    public List<Pattern> enemyCyclePatterns; // ��ȯ�ϴ� ����
    public bool isAlreadyPattern = false;
    public bool isFirstPattern = false;  // ���� ó�� ������ �ִ°�
    public bool isCyclePattern = false; // ������ ��ȯ�ϴ°�

    [SerializeField] private Image _patternImage;
    [SerializeField] private Text _patternText;

    private bool _isDecided = false;
    private int _patternTurn = 1;
    private Pattern _currentPattern;

    private bool isActFirst = true;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        isActFirst = true;
        if (isAlreadyPattern)
        {
            _currentPattern = alreadyPattern;
            GetIndent();
        }

        if (_currentPattern == null)
        {
            DecidePattern();
            _isDecided = true;
        }
    }

    public void Act()
    {
        // ù��° �Ͽ� �� ������ ������ ����
        ActPattern();

        _patternTurn++;
    }

    public void DecidePattern()
    {
        // ���� �����ϰ� ui�� �����ֱ�
        // �� �� ���� ��������Ʈ�� �� �Լ��� �־���� ��

        // ù��° ������ �ִ� ���̸� ù��° ������ ����� ��
        // �ƴϸ� ����(�ϴ���...)

        if(_isDecided)
        {
            _isDecided = false;
        }
        else if(_patternTurn == 1 && isFirstPattern && isActFirst)
        {
            _currentPattern = enemyFirstPattern;
            isActFirst = false;
            _patternTurn = 0;
        }
        else if(isCyclePattern)
        {
            _currentPattern = enemyCyclePatterns[(_patternTurn - 1) % enemyCyclePatterns.Count];
        }
        else
        {
            _currentPattern = enemyPatterns[Random.Range(0, enemyPatterns.Count)];
        }

        _patternImage.sprite = _currentPattern.patternData.patternIcon;
        _patternText.text = GetPatternAmount();
    }

    public void DecidePattern(Pattern pattern)
    {
        _currentPattern = pattern;
        _isDecided = true;

        _patternImage.sprite = _currentPattern.patternData.patternIcon;
        _patternText.text = "";
    }

    private void ActPattern()
    {
        switch (_currentPattern.patternData.patternType)
        {
            case EPatternType.Attack:
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power, _enemy);
                break;
            case EPatternType.Defense:
                _enemy.CharacterStat.Shield += _currentPattern.amount + _enemy.CharacterStat.Agility;
                break;
            case EPatternType.Debuff:
            case EPatternType.Buff:
                GetIndent();
                break;
            case EPatternType.DefendBuff:
                // �� 3 ��� 6
                GetIndent();
                _enemy.CharacterStat.Shield += _currentPattern.secondAmount;
                break;
            case EPatternType.AttackDefend:
                // ���� ���
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power, _enemy);
                _enemy.CharacterStat.Shield += _currentPattern.secondAmount + _enemy.CharacterStat.Agility;
                break;
            case EPatternType.AttackDebuff:
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power, _enemy);
                // ������ ī��
                for (int i = 0; i < _currentPattern.secondAmount; i++)
                    battleManager.Player.cardHolder.AddCardTemporary(cardGenerator.GenerateAbnormalStatusCard("����������"));
                break;
        }
    }

    private void GetIndent()
    {

        switch (_currentPattern.indentData.indent)
        {
            case EIndent.Weakening: // ��ȭ
                if(!battleManager.Player.PlayerRelic.GetRelic(ERelic.Ginger))  // �÷��̾ ���������� ��ȭ ������
                    battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.Weak:
                battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.damaged:  // �ջ�
                if (!battleManager.Player.PlayerRelic.GetRelic(ERelic.Turnip))  // �÷��̾ ���������� �ջ� ������
                    battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.Consciousness:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.Frenzy:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.Strength:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                _enemy.CharacterStat.Power += _currentPattern.amount;
                break;
            case EIndent.Roll:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.SporeCloud:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.Split:
                _enemy.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
        }
    }

    private string GetPatternAmount()
    {
        string result = "";

        Debug.Log(_currentPattern.patternData.patternType);

        switch (_currentPattern.patternData.patternType)
        {
            case EPatternType.Attack:
            case EPatternType.AttackDefend:
            case EPatternType.AttackDebuff:
                result = (_currentPattern.amount + _enemy.CharacterStat.Power).ToString();
                break;
        }

        return result;
    }
}
