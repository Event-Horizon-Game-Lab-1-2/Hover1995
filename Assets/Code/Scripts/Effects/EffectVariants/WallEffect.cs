using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : Effect
{
    [Header("Parameters")]
    [SerializeField] float WallLifeTime = 9f;
    [SerializeField] float SpawnOffset_X = 2f;

    public void Awake()
    {
        ApplyEffect();
    }

    public override void ApplyEffect(UsableManager gameObject)
    {
        Transform transform = gameObject.gameObject.transform;
        //send event
        EffectsManager.WallUsed.Invoke(WallLifeTime);
        //add wall at transform  position and rotation
        Instantiate(this.gameObject, transform.localPosition - (transform.forward*SpawnOffset_X), transform.rotation);
        
        //remove after lifeTime
        if(this.gameObject.activeSelf)
            StartCoroutine(Effect());
        
    }
    
    IEnumerator Effect()
    {
        yield return new WaitForSeconds(WallLifeTime);
        Destroy(this.gameObject);
    }

}
