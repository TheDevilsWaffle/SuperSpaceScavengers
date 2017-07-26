using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class AnimateGraphicColor : AnimateColor
{
    public Graphic graphic;

    // Use this for initialization
    protected override void Start()
    {
        if(graphic == null)
            graphic = GetComponent<Graphic>();
        base.Start();
    }

    protected override void SetStartValue()
    {
        startColor = graphic.color;
    }

    protected override void SetColor(Color _newColor)
    {
        graphic.color = _newColor;
    }
}
