using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscureMap : Effect
{
    public override void ApplyEffect(GameObject gameObject)
    {
        if(!PlayerManager.Invulnerability)
            UIManager.ObscureMap.Invoke(EffectTime);
    }
}
