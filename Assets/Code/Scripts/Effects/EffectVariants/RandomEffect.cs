using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEffect : Effect
{
    [SerializeField] Effect[] effects;

    public override void ApplyEffect(GameObject gameObject)
    {
        effects[Random.Range(0, effects.Length)].ApplyEffect(gameObject);
    }

}
