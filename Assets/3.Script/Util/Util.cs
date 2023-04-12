using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// �Ǽ�-������ ���� �˰���
    /// ���޹��� ����Ʈ�� �������� �����ݴϴ�.
    /// </summary>
    /// <typeparam name="T">����Ʈ�� ���׸�</typeparam>
    /// <param name="list">���� ����Ʈ</param>
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
