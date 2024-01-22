using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibilityEffect : Effect
{
    public override void ApplyEffect(UsableManager usableManager)
    {
        StartCoroutine(effect());
    }

    IEnumerator effect()
    {
        PlayerManager.VisibleToEnemy = false;
        EffectsManager.InvisibilityUsed.Invoke(EffectTime);
        yield return new WaitForSeconds(EffectTime);
        PlayerManager.VisibleToEnemy = true;
    }
}
