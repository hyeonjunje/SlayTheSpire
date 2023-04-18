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
    // 씬 이동시 실행할 함수들
    public delegate void OnMoveOtherScene();
    public OnMoveOtherScene onMoveOtherScene;

    public void Init()
    {

    }


    public void LoadScene(ESceneName sceneName)
    {
        onMoveOtherScene?.Invoke();
        SceneManager.LoadScene((int)sceneName);

        // 씬 이동 시 UI들 제거
        GameManager.UI.ClearUI();
    }
}
