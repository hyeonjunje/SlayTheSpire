using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene : BaseScene
{
    [SerializeField] private CanvasGroup _actInfo;

    [SerializeField] private Player _player;
    [SerializeField] private CardHolder _cardHolder;

    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _myCard;

    [SerializeField] private Neow _neow;

    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    private MapGenerator mapGenerator => ServiceLocator.Instance.GetService<MapGenerator>();

    public override void Init()
    {
        base.Init();

        _neow.gameObject.SetActive(true);

        // 씬 bgm 실행
        GameManager.Sound.PlayBGM(EBGM.Level1);

        // 시작 시 맵 만들기 (보여주는 건 x)
        // 맵 생성 && 맵 데이터 넘겨주기
        GameManager.Game.SetMapArray(mapGenerator.GenerateMap());

        // 1막 태초 코루틴으로 보여주기
        StartCoroutine(CoAppearActInfo());

        // 카드도 생성해야 하지
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(1));   // 타격
        _player.AddCard(cardGenerator.GenerateCard(33));  // 수비
        _player.AddCard(cardGenerator.GenerateCard(33));
        _player.AddCard(cardGenerator.GenerateCard(33));
        _player.AddCard(cardGenerator.GenerateCard(33));
        _player.AddCard(cardGenerator.GenerateCard(33));

        // 캐릭터도 만들어야하지
    }


    public void PopUI()
    {
        GameManager.UI.PopUI();
    }

    public void ShowUI(BaseUI ui)
    {
        GameManager.UI.ShowUI(ui);
    }



    private IEnumerator CoAppearActInfo()
    {
        _actInfo.gameObject.SetActive(true);

        // 1.5초간 등장
        float timeElapsed = 0;
        while (true)
        {
            _actInfo.alpha = Mathf.Lerp(0, 1, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 1.5f)
                break;
            yield return null;
        }

        // 1초간 유지
        yield return new WaitForSeconds(1f);

        // 1.5초간 사라짐
        timeElapsed = 0;
        while (true)
        {
            _actInfo.alpha = Mathf.Lerp(1, 0, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 1.5f)
                break;
            yield return null;
        }

        _actInfo.gameObject.SetActive(false);
    }
}
