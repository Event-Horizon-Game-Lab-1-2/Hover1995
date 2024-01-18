using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscureMap : Effect
{
    [SerializeField][Range(0f, 60f)] float effectTime = 5.0f;

    public override void ApplyEffect(GameObject gameObject)
    {
        UIManager.ObscureMap.Invoke(effectTime);
    }

}
