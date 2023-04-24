using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class WindowShake : MonoBehaviour
{
    public static WindowShake Instance;

    private Vector3 _origin;

    [SerializeField]
    private float _vibrationObjectForce = 0.1f;
    [SerializeField]
    private float _vibrationUIForce = 6f;

    private Coroutine _coVibration;
    private BaseUI CurrentUI => GameManager.UI.CurrentUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _origin = transform.position;
    }

    public void ShakeWindow()
    {
        // UI도 흔들리고
        // 카메라도 흔들리고
        if (_coVibration != null)
            StopCoroutine(_coVibration);
        _coVibration = StartCoroutine(CoVibration());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ShakeWindow();
    }

    private IEnumerator CoVibration()
    {
        transform.position = _origin;
        if(CurrentUI != null)
            CurrentUI.transform.localPosition = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {
            Vector3 randomVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

            transform.position += randomVec * _vibrationObjectForce;
            if(CurrentUI != null)
            {
                CurrentUI.transform.localPosition += randomVec * _vibrationUIForce;
            }

            yield return new WaitForSeconds(0.03f);

            transform.position = _origin;
            if (CurrentUI != null)
            {
                CurrentUI.transform.localPosition = Vector3.zero;
            }
        }
    }
}
