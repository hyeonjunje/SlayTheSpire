using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler
{
    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;
    private Coroutine _coMove;

    [SerializeField]
    private float _moveTime = 0.2f;


    // ���콺 Ŭ�� ��
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("�̰� ��? OnPointerClick " + gameObject.name);
    }

    // ���콺 ���� ��
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("�̰� ��? OnPointerDown " + gameObject.name);
    }

    // �ش� �׸��� ���콺�� �� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("�̰� ��? OnPointerEnter " + gameObject.name);
    }

    // ���콺 �� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("�̰� ��? OnPointerUp " + gameObject.name);
    }


    // ī�带 ���� ��
    public void MoveCard(Vector3 targetPos, Vector3 targetRot, Vector3 targetScl)
    {
        _targetPos = targetPos;
        _targetRot = targetRot;
        _targetScl = targetScl;

        if(_coMove != null)
        {
            StopCoroutine(_coMove);
        }
        _coMove = StartCoroutine(CoMove());
    }

    private IEnumerator CoMove()
    {
        float currentTime = 0f;

        Vector3 originPos = transform.localPosition;
        Vector3 originRot = transform.localEulerAngles;
        Vector3 originScl = transform.localScale;

        while (true)
        {
            currentTime += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(originPos, _targetPos, currentTime / _moveTime);

            transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(originRot.z, _targetRot.z, currentTime / _moveTime));
            // transform.localEulerAngles = Vector3.Lerp(originRot, _targetRot, currentTime / _moveTime);

            transform.localScale = Vector3.Lerp(originScl, _targetScl, currentTime / _moveTime);

            if (currentTime >= _moveTime)
                break;

            yield return null;
        }
    }
}
