using UnityEngine;

[System.Serializable]
public class JumpPlatForm : Effect
{
    [SerializeField] float JumpHeigh = 20.0f;
    [SerializeField] float GravityScale = 1.0f;
    
    public override void ApplyEffect(GameObject gameObject)
    {
        Rigidbody Body  = gameObject.GetComponent<Rigidbody>();
        Body.AddForce(0.0f, Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeigh * GravityScale), 0.0f, ForceMode.Impulse);
    }
}
