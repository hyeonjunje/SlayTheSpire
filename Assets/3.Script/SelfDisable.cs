using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    VFXGenerator vfxGenerator => ServiceLocator.Instance.GetService<VFXGenerator>();

    // 애니메이션 이벤트로 넣어둠
    private void Disable()
    {
        vfxGenerator.ReturnVFX(gameObject);
    }
}
