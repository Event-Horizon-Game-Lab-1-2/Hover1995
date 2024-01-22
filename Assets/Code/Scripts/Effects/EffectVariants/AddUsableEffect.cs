using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUsableEffect : Effect
{
    [Tooltip("0->Spring\n1->Wall\n2->Invisibility")]
    [SerializeField][Range(0, 2)] int PowerUpGiven=0;
    public override void ApplyEffect(GameObject gameObject)
    {
        UsableManager.ObtainUsable.Invoke(PowerUpGiven, 1);
    }
}
