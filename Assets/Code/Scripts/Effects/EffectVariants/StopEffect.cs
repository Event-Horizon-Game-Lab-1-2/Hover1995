using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEffect : Effect
{
    private Movement objectMovement;
    [SerializeField] LayerMask PlayerLayer;

    public override void ApplyEffect(GameObject gameObject)
    {
        objectMovement = gameObject.GetComponent<Movement>();
        //check if id the player
        if (PlayerLayer == 1 << gameObject.layer && PlayerManager.Invulnerability)
            return;
        StartCoroutine(ApplyStop());
    }

    IEnumerator ApplyStop()
    {
        objectMovement.Halt();
        yield return new WaitForSeconds(EffectTime);
        objectMovement.Continue();
    }

}
