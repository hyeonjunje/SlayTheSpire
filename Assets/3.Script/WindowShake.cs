using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class WindowShake : MonoBehaviour
{
    private Vector3 _origin;

    private void Awake()
    {
        _origin = transform.position;
    }


    public void ShakeWindow()
    {
        // UI�� ��鸮��
        // ī�޶� ��鸮��
    }
}
