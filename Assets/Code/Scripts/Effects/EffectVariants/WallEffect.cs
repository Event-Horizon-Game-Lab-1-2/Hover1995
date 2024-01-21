using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallEffect : Effect
{
    [Header("Parameters")]
    [SerializeField] float WallLifeTime = 9f;
    [SerializeField] float SpawnOffset_X = 2f;
    [SerializeField] GameObject WallPrefab;

    public override void ApplyEffect(UsableManager gameObject)
    {
        Transform transform = gameObject.gameObject.transform;
        //send event
        EffectsManager.WallUsed.Invoke(WallLifeTime);
        //add wall at transform  position and rotation
        GameObject newObject = Instantiate(WallPrefab, transform.localPosition - (transform.forward*SpawnOffset_X), transform.rotation);
        newObject.GetComponent<DestroyAfter>().SelfKill(WallLifeTime);
    }
}
