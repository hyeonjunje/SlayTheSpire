using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// 피셔-예이츠 셔플 알고리즘
    /// 전달받은 리스트를 무작위로 섞어줍니다.
    /// </summary>
    /// <typeparam name="T">리스트의 제네릭</typeparam>
    /// <param name="list">섞을 리스트</param>
    public static void ShuffleList<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// 자식 오브젝트 다 지웁니다.
    /// </summary>
    /// <param name="parent">지울 오브젝트들의 부모</param>
    public static void DestroyAllChild(this Transform parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child != parent.transform)
                GameObject.Destroy(child.gameObject);
    }


    public static IEnumerator ShakeCamera(float force = 6f)
    {
        Transform cameraPosition = Camera.main.transform;
        Vector3 origin = cameraPosition.position;


        for (int i = 0; i < 5; i++)
        {
            cameraPosition.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * force;
            yield return new WaitForSeconds(0.03f);
            cameraPosition.position = origin;
        }
    }
}
