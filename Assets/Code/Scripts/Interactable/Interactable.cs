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
            if(EffectOnTrigger != null)
            {
                EffectOnTrigger.ApplyEffect(other.gameObject);
            }
            else
                Debug.LogWarning("EFFECT NOT SET " + this);
            
            if(RemoveOnTrigger)
            {
                SpawnerManager.InteractableTriggred.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    public void Trigger(UsableManager usableManager)
    {
        EffectOnTrigger.ApplyEffect(usableManager);
    }
}
