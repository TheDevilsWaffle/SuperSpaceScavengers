using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AnimateMeshColor : AnimateColor
{
    private MeshRenderer meshRenderer;

    public bool useEmissive = false;

    // Use this for initialization
    protected override void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        base.Start();
    }
    protected override void SetStartValue()
    {
        if(!useEmissive)
            startColor = meshRenderer.material.color;
        else
            startColor = meshRenderer.material.GetColor("_EmissionColor");
    }
    protected override void SetColor(Color _newColor)
    {
        if (!useEmissive)
            meshRenderer.material.color = _newColor;
        else
            meshRenderer.material.SetColor("_EmissionColor", _newColor);
    }
}
