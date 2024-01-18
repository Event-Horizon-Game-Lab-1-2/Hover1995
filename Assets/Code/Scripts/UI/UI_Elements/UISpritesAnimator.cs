using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpritesAnimator : MonoBehaviour
{
    
    [SerializeField] private float FrameDuration;

    [SerializeField] private Sprite[] sprites;

    private Image image;
    private int index = 0;
    private float timer = 0;

    public bool isPlaying = false;

    void Start()
    {
        image = GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        if (!isPlaying)
            return;

        if ((timer += Time.deltaTime) >= (FrameDuration / sprites.Length))
        {
            timer = 0;
            image.sprite = sprites[index];
            index = (index + 1) % sprites.Length;
        }
    }
}
