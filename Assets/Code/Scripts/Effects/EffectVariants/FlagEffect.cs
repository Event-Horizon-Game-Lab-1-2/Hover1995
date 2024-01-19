using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FlagEffect : Effect
{
    [Tooltip("Score of the flag")]
    [SerializeField] private int ScoreValue = 1000;
    [SerializeField] private LayerMask PlayerlayerMask;
    [SerializeField] private LayerMask EnemylayerMask;
    [SerializeField] private bool RemoveFlagEffect = false;
    public override void ApplyEffect(GameObject gameObject)
    {
        if (gameObject == null)
            return;

        if (1<<gameObject.layer == PlayerlayerMask)
        {
            if (RemoveFlagEffect)
                GameManager.FlagRemoved.Invoke(true, ScoreValue);
            else
                GameManager.FlagTaken.Invoke(true, ScoreValue);
        }
        if(1<<gameObject.layer == EnemylayerMask)
        {
            if (RemoveFlagEffect)
                GameManager.FlagRemoved.Invoke(false, ScoreValue);
            else
                GameManager.FlagRemoved.Invoke(false, ScoreValue);
        }
    }
}