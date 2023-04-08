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
    public void Init()
    {

    }


    public void LoadScene(ESceneName sceneName)
    {
        SceneManager.LoadScene((int)sceneName);
    }
}
