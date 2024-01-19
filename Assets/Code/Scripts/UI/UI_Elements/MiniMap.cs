using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] RenderTexture MiniMapView;
    [SerializeField] Image MinimapObscurer;
    [Header("Easter Egg")]
    [SerializeField] Image MinimapObscurer_EasterEgg;
    [SerializeField][Range(0f, 1f)] private float easterEggProbability = 0.1f;

    private void Awake()
    {
        MinimapObscurer.enabled = false;
        MinimapObscurer_EasterEgg.enabled = false;
    }

    public void Obscure()
    {
        float easterEgg = UnityEngine.Random.Range(0f, 1f);

        if (easterEgg <= easterEggProbability)
            MinimapObscurer_EasterEgg.enabled = true;
        
        MinimapObscurer.enabled = true;
    }

    public void Clear()
    {
        MinimapObscurer.enabled = false;
        MinimapObscurer_EasterEgg.enabled = false;
    }
}
