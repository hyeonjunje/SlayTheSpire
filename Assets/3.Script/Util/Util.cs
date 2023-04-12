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
}
