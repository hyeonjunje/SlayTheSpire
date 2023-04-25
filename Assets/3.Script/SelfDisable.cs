using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    VFXGenerator vfxGenerator => ServiceLocator.Instance.GetService<VFXGenerator>();

    // �ִϸ��̼� �̺�Ʈ�� �־��
    private void Disable()
    {
        vfxGenerator.ReturnVFX(gameObject);
    }
}
