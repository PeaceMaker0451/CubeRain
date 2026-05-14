using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void RandomizeColor()
    {
        SetColor(Random.ColorHSV());
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}