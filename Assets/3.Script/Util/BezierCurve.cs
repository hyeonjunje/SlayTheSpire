using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BezierCurve : MonoBehaviour
{
    public Transform p0, p1, p2;

    [SerializeField]
    private Image[] bezierCureveImage;
   
    [SerializeField]
    private float minSize, maxSize;
    [SerializeField]
    private float arrowSize = 3f;

    [SerializeField]
    private Color highlightColor, originColor;


    private void Awake()
    {
        for (int i = 0; i < bezierCureveImage.Length - 1; i++)
        {
            bezierCureveImage[i].transform.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, (float)i / (bezierCureveImage.Length - 1));
        }
        bezierCureveImage[bezierCureveImage.Length - 1].transform.localScale = Vector3.one * arrowSize;
    }

    public Vector3 Bezier(float t)
    {
        float oneMinusT = 1 - t;
        float oneMinusTPower = Mathf.Pow(oneMinusT, 2);
        float tPower = Mathf.Pow(t, 2);

        Vector3 result = oneMinusTPower * p0.position + 2 * t * oneMinusT * p1.position + tPower * p2.position;

        return result;
    }

    public void Highlight(bool flag)
    {
        for(int i = 0; i < bezierCureveImage.Length; i++)
        {
            if(flag)
                bezierCureveImage[i].color = highlightColor;
            else
                bezierCureveImage[i].color = originColor;
        }
    }

    private void Update()
    {
        for (int i = 0; i < bezierCureveImage.Length; i++)
        {
            Vector3 pos = Bezier((float)i / (bezierCureveImage.Length - 1));
            bezierCureveImage[i].transform.position = pos;
            
            if(i != 0)
            {
                Vector3 dir = (bezierCureveImage[i].transform.position - bezierCureveImage[i - 1].transform.position).normalized;
                float theta = 0;

                if (dir.x != 0)
                {
                    if (dir.x > 0)
                    {
                        theta = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg - 90;
                    }
                    else
                    {
                        theta = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg + 90;
                    }
                }

                bezierCureveImage[i].transform.localEulerAngles = new Vector3(0f, 0f, theta);
            }
        }
    }
}
