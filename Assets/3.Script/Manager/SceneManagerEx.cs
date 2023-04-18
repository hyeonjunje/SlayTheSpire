using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneName
{
    Title,
    Act1,
    Act2,
    Act3,
}

public class SceneManagerEx
{
    // �� �̵��� ������ �Լ���
    public delegate void OnMoveOtherScene();
    public OnMoveOtherScene onMoveOtherScene;

    public void Init()
    {

    }


    public void LoadScene(ESceneName sceneName)
    {
        onMoveOtherScene?.Invoke();
        SceneManager.LoadScene((int)sceneName);

        // �� �̵� �� UI�� ����
        GameManager.UI.ClearUI();
    }
}
