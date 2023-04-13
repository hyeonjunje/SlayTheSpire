using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbUI : MonoBehaviour
{
    [SerializeField]
    private float speed1, speed2, speed3, speed4;

    [SerializeField]
    private Transform transform1, transform2, transform3, transform4;

    private void Update()
    {
        transform1.localEulerAngles += Vector3.forward * speed1 * Time.deltaTime;
        transform2.localEulerAngles += Vector3.forward * speed2 * Time.deltaTime;
        transform3.localEulerAngles += Vector3.forward * speed3 * Time.deltaTime;
        transform4.localEulerAngles += Vector3.forward * speed4 * Time.deltaTime;
    }
}
