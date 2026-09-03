using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public Color CurrentColor => _meshRenderer.material.color;

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

    public void SetOpaqueMode()
    {
        _meshRenderer.material.ToOpaqueMode();
    }

    public void SetFadeMode()
    {
        _meshRenderer.material.ToFadeMode();
    }

    public void SetColorSmoothly(Color color, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothColorChange(color, duration));
    }

    private IEnumerator SmoothColorChange(Color color, float duration)
    {
        float time = 0;
        Color baseColor = CurrentColor;

        while (time <= duration)
        {
            SetColor(Color.Lerp(baseColor, color, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
    }
}