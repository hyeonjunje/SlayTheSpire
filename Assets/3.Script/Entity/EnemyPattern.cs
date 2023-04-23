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

    public Pattern alreadyPattern;  // 이미 있는 패턴
    public Pattern enemyFirstPattern;  // 제일 처음 패턴
    public List<Pattern> enemyPatterns;  // 그 외에 패턴
    public List<Pattern> enemyCyclePatterns; // 순환하는 패턴
    public bool isAlreadyPattern = false;
    public bool isFirstPattern = false;  // 제일 처음 패턴이 있는가
    public bool isCyclePattern = false; // 패턴이 순환하는가

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
        // 첫번째 턴에 할 패턴이 있으면 실행
        ActPattern();

        _patternTurn++;
    }

    public void DecidePattern()
    {
        // 패턴 결정하고 ui로 보여주기
        // 내 턴 시작 델리게이트에 이 함수를 넣어줘야 함

        // 첫번째 패턴이 있는 적이면 첫번째 패턴을 해줘야 함
        // 아니면 랜덤(일단은...)

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
                // 힘 3 방어 6
                GetIndent();
                _enemy.CharacterStat.Shield += _currentPattern.secondAmount;
                break;
            case EPatternType.AttackDefend:
                // 공격 방어
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power, _enemy);
                _enemy.CharacterStat.Shield += _currentPattern.secondAmount + _enemy.CharacterStat.Agility;
                break;
            case EPatternType.AttackDebuff:
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power, _enemy);
                // 슬라임 카드
                for (int i = 0; i < _currentPattern.secondAmount; i++)
                    battleManager.Player.cardHolder.AddCardTemporary(cardGenerator.GenerateAbnormalStatusCard("점액투성이"));
                break;
        }
    }

    private void GetIndent()
    {

        switch (_currentPattern.indentData.indent)
        {
            case EIndent.Weakening: // 약화
                if(!battleManager.Player.PlayerRelic.GetRelic(ERelic.Ginger))  // 플레이어가 생강있으면 약화 안입음
                    battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.Weak:
                battleManager.Player.CharacterIndent.AddIndent(_currentPattern.indentData, _currentPattern.amount);
                break;
            case EIndent.damaged:  // 손상
                if (!battleManager.Player.PlayerRelic.GetRelic(ERelic.Turnip))  // 플레이어가 순무있으면 손상 안입음
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
