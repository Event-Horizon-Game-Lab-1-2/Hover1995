using System;
using UnityEngine;

[System.Serializable]
public class Effect : MonoBehaviour
{

    [SerializeField] protected bool Malus = false;
    [SerializeField] protected float EffectTime = 0f;

    public virtual void ApplyEffect(GameObject gameObject)
    {

    }

    public virtual void ApplyEffect(UsableManager usableManager)
    {

    }

    public virtual void ApplyEffect(Transform transform)
    {

    }

    public virtual void ApplyEffect()
    {

    }
}
