using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEffect : Effect
{
    [SerializeField] private float EffectTime = 0f;
    private Movement objectMovement;

    public override void ApplyEffect(GameObject gameObject)
    {
        objectMovement = gameObject.GetComponent<Movement>();
        StartCoroutine(ApplyStop());
    }

    IEnumerator ApplyStop()
    {
        objectMovement.Halt();
        yield return new WaitForSeconds(EffectTime);
        objectMovement.Continue();
    }

}
