using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class ResourceMeter : MonoBehaviour
{
    [HideInInspector]
    public Canvas canvas;

    public Image delayedFill = null;
    public Image mainFill = null;

    void OnValidate()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
    }

    // Use this for initialization
    void OnEnable()
    {
        canvas.enabled = true;
    }
    void OnDisable()
    {
        canvas.enabled = false;
    }
}
