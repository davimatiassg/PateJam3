using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private float maxValue = 100f;
    private float initialxSc;
    private float initialxPos;

    void Start()
    {
        initialxSc = transform.localScale.x;
        initialxPos = transform.localPosition.x;
    }

    public void SetMaxValue(float value)
    {
        maxValue = value;
    }

    public void UpdateValue(float value)
    {
        if (value >= 0)
        {
            float r = value/maxValue;
            float xSc = r*(initialxSc);
            transform.localScale = new Vector2 (xSc, transform.localScale.y);
            transform.localPosition = new Vector2 (
                initialxPos - (initialxSc/2)*(1 - r), transform.localPosition.y);
        }
    }
}
