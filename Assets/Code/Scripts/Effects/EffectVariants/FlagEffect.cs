using UnityEngine;

[System.Serializable]
public class FlagEffect : Effect
{
    [SerializeField] private int ScoreValue = 1000;
    public override void ApplyEffect(GameObject gameObject)
    {
        UIManager ui = gameObject.GetComponent<UIManager>();
        if (ui != null)
            ui.PickUpFlag(ScoreValue);
    }
}