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

    /// <summary>
    /// �ڽ� ������Ʈ �� ����ϴ�.
    /// </summary>
    /// <param name="parent">���� ������Ʈ���� �θ�</param>
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
