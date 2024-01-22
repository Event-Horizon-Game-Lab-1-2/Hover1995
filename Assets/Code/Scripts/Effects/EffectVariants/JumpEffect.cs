using UnityEngine;

[System.Serializable]
public class JumpPlatForm : Effect
{
    [SerializeField] float JumpHeigh = 20.0f;
    [SerializeField] float GravityScale = 1.0f;

    public override void ApplyEffect(GameObject gameObject)
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        Movement playerMovement = body.GetComponent<Movement>();
        if(playerMovement != null)
        {
            //check if the player is on the ground
            if(playerMovement.OnGround)
                body.AddForce(0.0f, Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeigh * GravityScale), 0.0f, ForceMode.Impulse);
        }
    }

    public override void ApplyEffect(UsableManager usableManager)
    {
        Rigidbody body = usableManager.GetComponent<Rigidbody>();
        Movement playerMovement = body.GetComponent<Movement>();
        if (playerMovement != null)
        {
            //check if the player is on the ground
            if (playerMovement.OnGround)
                body.AddForce(0.0f, Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeigh * GravityScale), 0.0f, ForceMode.Impulse);
        }
    }
}
