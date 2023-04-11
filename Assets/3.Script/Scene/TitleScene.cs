using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : BaseScene
{
    [SerializeField]
    private GameObject _menuGameObject;

    [SerializeField]
    private GameObject[] selectedCharacters;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private float _vibrationForce = 3f;

    private Coroutine _covibration = null;

    public override void Init()
    {
        base.Init();

        GameManager.Sound.PlayBGM(EBGM.Menu);

        ShowUI(_menuGameObject);

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => ExitUI());
    }

    // 버튼에 추가
    public void ShowSelectCharacter(int index)
    {
        if(_covibration != null)
        {
            StopCoroutine(_covibration);
        }
        _covibration = StartCoroutine(CoVibration(index));

        
        for(int i = 0; i < selectedCharacters.Length; i++)
        {
            if(i == index)
            {
                selectedCharacters[i].SetActive(true);
            }
            else
            {
                selectedCharacters[i].SetActive(false);
            }
        }

        startButton.SetActive(true);

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => ExitSelectCharacter());
    }

    // 출정 버튼에 추가
    public void MoveAct1()
    {
        GameManager.Scene.LoadScene(ESceneName.Act1);
    }

    private void ExitSelectCharacter()
    {
        for (int i = 0; i < selectedCharacters.Length; i++)
        {
            selectedCharacters[i].SetActive(false);
        }

        startButton.SetActive(false);

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => ExitUI());
    }

    private IEnumerator CoVibration(int index)
    {
        // 진동
        // 소리

        Vector3 origin = selectedCharacters[index].transform.position;

        for (int i = 0; i < 5; i++)
        {
            selectedCharacters[index].transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * _vibrationForce;
            yield return new WaitForSeconds(0.03f);
            selectedCharacters[index].transform.position = origin;
        }
    }
}
