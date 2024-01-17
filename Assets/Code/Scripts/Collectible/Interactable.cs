using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] LayerMask CollisionLayer;
    [SerializeField] Effect EffectOnTrigger;
    [SerializeField] bool RemoveOnTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == CollisionLayer)
        {
            EffectOnTrigger.ApplyEffect(other.gameObject);
            if(RemoveOnTrigger)
                Destroy(gameObject);
        }
    }
}
