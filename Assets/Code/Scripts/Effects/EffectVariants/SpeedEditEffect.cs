using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedEditEffect : Effect
{
    [SerializeField][Range(0f, 1f)] float NewSpeedLimiter = 0f;
    private Movement objectMovement;

    public override void ApplyEffect(GameObject gameObject)
    {
        if (Malus && PlayerManager.Invulnerability)
            return;

        EffectsManager.SpeedEditUsed.Invoke(EffectTime, Malus);
        objectMovement = gameObject.GetComponent<Movement>();
        if(objectMovement != null)
            StartCoroutine(ApplyStop());
    }

    IEnumerator ApplyStop()
    {
        float oldLimiter = objectMovement.SpeedLimiter;
        objectMovement.SpeedLimiter = NewSpeedLimiter;
        yield return new WaitForSeconds(EffectTime);  
        objectMovement.SpeedLimiter = oldLimiter;
    }

}
