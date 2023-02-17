using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaunchText : MonoBehaviour
{
    public AnimationCurve curve;
    float time = 0;
    float lastKeyTime;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Keyframe lastKey = curve[curve.length - 1];
        lastKeyTime = lastKey.time;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > lastKeyTime)
        {
            time = 0;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, curve.Evaluate((time / lastKeyTime) * lastKeyTime));
    }
}
