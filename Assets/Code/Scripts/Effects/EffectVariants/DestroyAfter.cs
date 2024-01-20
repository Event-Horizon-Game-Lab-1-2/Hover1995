using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public void SelfKill(float time)
    {
        //remove after lifeTime
        Destroy(this.gameObject, time);
    }
}
