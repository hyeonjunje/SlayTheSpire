using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Character _character;
    private Animator _animator;
    public void Init(Character character)
    {
        this._character = character;
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string anim)
    {
        _animator.SetTrigger(anim);
    }


    public IEnumerator CoAct(bool isRight)
    {
        Vector3 dir = isRight ? Vector3.right : Vector3.left;

        Vector3 origion = transform.position;

        float moveTime = 0.1f;
        float currentTime = 0f;

        while (true)
        {
            currentTime += Time.deltaTime;

            transform.position = Vector3.Lerp(origion, origion + dir, currentTime / moveTime);

            if (currentTime >= moveTime)
            {
                break;
            }

            yield return null;
        }

        currentTime = 0f;
        origion = transform.position;

        while (true)
        {
            currentTime += Time.deltaTime;

            transform.position = Vector3.Lerp(origion, origion - dir, currentTime / moveTime);

            if (currentTime >= moveTime)
            {
                break;
            }

            yield return null;
        }
    }
}
