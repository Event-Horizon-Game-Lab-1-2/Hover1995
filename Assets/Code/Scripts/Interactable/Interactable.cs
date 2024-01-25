using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] LayerMask CollisionLayer;
    [SerializeField] Effect EffectOnTrigger;
    [SerializeField] bool RemoveOnTrigger = true;
    [SerializeField] bool SendRespawnRequest = false;

    private void OnTriggerEnter(Collider other)
    {
        if(CollisionLayer == (CollisionLayer | (1<< other.gameObject.layer) ))
        {
            if(EffectOnTrigger != null)
            {
                EffectOnTrigger.ApplyEffect(other.gameObject);
            }
            else
                Debug.LogWarning("EFFECT NOT SET " + this);
            
            if(RemoveOnTrigger)
            {
                if(SendRespawnRequest)
                    InteractableSpawnerTrigger.InteractableTriggred.Invoke();

                Destroy(this.gameObject);
            }
        }
    }

    public void Trigger(UsableManager usableManager)
    {
        EffectOnTrigger.ApplyEffect(usableManager);
    }
}
