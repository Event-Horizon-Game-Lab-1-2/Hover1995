using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FlagEffect : Effect
{
    [Tooltip("Score of the flag")]
    [SerializeField] private int ScoreValue = 1000;
    [Tooltip("Define the flag team\nTrue = Player Flag\nFalse = Enemy Flag")]
    [SerializeField] private bool PlayerFlag = false;
    public override void ApplyEffect(GameObject gameObject)
    {
        
    }
}