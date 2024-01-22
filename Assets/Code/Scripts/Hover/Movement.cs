using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region PARAMETERS
    #region Acceleration
    [Header("Acceleration parameters")]
    [Tooltip("Acceleration curve of the movement")]
    [SerializeField] private AnimationCurve AccelerationCurve;
    [Tooltip("How fast the acceleration is, Acceleration = Acceleration curve * Acceleration Speed * time + Initial Acceleration")]
    [SerializeField][Range(0.1f, 5.0f)] private float AccelerationSpeed = 1;
    [Tooltip("Start Value of the acceleration, Acceleration = Acceleration curve * Acceleration Speed * time + Initial Acceleration")]
    [SerializeField][Range(0.0f, 1.0f)] private float InitialAcceleration = 0.1f;
    [Space]
    //used in fixed update to get the acceleration
    private float AccelerationTimer;
    #endregion
    #region Speeds
    [Header("Speed parameters")]
    [Tooltip("Max speed going forward")]
    [SerializeField][Range(0.1f, 50.0f)] public float MaxSpeed = 30.0f;
    [Tooltip("Is the force applied when going forward")]
    [SerializeField] private float PositiveForce = 20f;
    [Tooltip("Is the force applied when going backward")]
    [SerializeField] private float RotationSpeed = 1f;
    [Tooltip("Rapresent the max speed reachable by the player\nSpeed = Max Speed * Speed Limiter")]
    [SerializeField] public float StartSpeedLimiter = 0.75f;
    [Space]
    [HideInInspector] public float SpeedLimiter;
    [HideInInspector] public float ClampedVelocity;
    [HideInInspector] private float LinearVelocity = 0f;
    [HideInInspector] private bool CanMove = true;
    #endregion
    #region Ground Check
    [Header("Ground Controller")]
    [Tooltip("Layer to check ")]
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Vector3 TriggerOffset = Vector3.zero;
    [SerializeField] private float TriggerSize = 0.2f;
    [SerializeField] private Color GroundCheckColor = Color.cyan;
    [Space]
    public bool OnGround = false;
    #endregion
    #region Collision
    [Header("Collision")]
    [Tooltip("Is the force applied when colliding with a collider")]
    [SerializeField] private float MaxBounceForce = 20f;
    [SerializeField] private float MinBounceForce = 10f;
    [SerializeField] private Color CollisionLine = Color.cyan;
    [Space]
    //physic based
    private Rigidbody Body;
    #endregion
    #endregion

    void Awake()
    {
        Body = GetComponent<Rigidbody>();
        Body.velocity = Vector3.zero;

        SpeedLimiter = StartSpeedLimiter;
    }

    public void Move(float direction)
    {
        if(!CanMove)
            return;

        #region Acceleration
        //acceleration (AccelerationTimer * accelerationCurve)
        AccelerationTimer += Time.deltaTime * AccelerationSpeed;
        float acceleration = AccelerationCurve.Evaluate(AccelerationTimer) + InitialAcceleration;
        Mathf.Clamp01(AccelerationTimer);
        Mathf.Clamp01(acceleration);

        //Adding Force
        if (direction != 0 && LinearVelocity < MaxSpeed * SpeedLimiter)
            Body.AddForce(transform.forward * PositiveForce * acceleration * direction);
        else
            AccelerationTimer = 0.0f;
        #endregion

        LinearVelocity = Vector3.Distance(Vector3.zero, new Vector3(Body.velocity.x, 0.0f, Body.velocity.z));
        ClampedVelocity = LinearVelocity / MaxSpeed;

        OnGround = IsGrounded();
    }

    public void Rotate(float rotation)
    {
        #region Rotation
        //Rotate the Y axis
        if (Input.GetAxisRaw("Horizontal") != 0)
            transform.Rotate(new Vector3(0, 1, 0) * RotationSpeed * rotation, Space.World);
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Bounce
        if (!OnGround)
            return;
        if (collision.contacts[0].normal.y == 0)
        {
            //Collision amount
            int collsionAmount = collision.GetContacts(collision.contacts);
            //Collision normal
            Vector3 collisionNormal = Vector3.zero;
            //Averange collision point
            Vector3 collisionBouncePoint = Vector3.zero;

            //Get the averange collision point
            foreach(ContactPoint contacts in collision.contacts)
            {
                collisionNormal += contacts.normal;
                collisionBouncePoint += contacts.point;
            }
            collisionNormal /= collsionAmount;
            collisionBouncePoint /= collsionAmount;

            //reset the rigid body velocity
            Body.velocity = Vector3.zero;
            //Adjust the bounce force
            float forceApplied = MaxBounceForce * (LinearVelocity / MaxSpeed);
            if(forceApplied < MinBounceForce)
                forceApplied = MinBounceForce;

            //Add the bounce force
            Body.AddForceAtPosition(new Vector3(forceApplied * collisionNormal.x, 0.0f, forceApplied * collisionNormal.z), collisionBouncePoint, ForceMode.Impulse);

            //Debug ray for collision visualization
            Debug.DrawRay(collisionBouncePoint, collisionNormal * forceApplied, CollisionLine, 10f);
        }
        #endregion
    }

    private void OnDrawGizmos()
    {
        //GROUND CHECK
        Gizmos.color = GroundCheckColor;
        Gizmos.DrawWireSphere(transform.position + TriggerOffset, TriggerSize);
    }
    
    private bool IsGrounded() {
        return Physics.CheckSphere(transform.position + TriggerOffset, TriggerSize, GroundLayer);
    }

    public void Halt()
    {
        CanMove = false;
        Body.velocity = Vector3.zero;
        LinearVelocity = 0f;
    }

    public void Continue()
    {
        CanMove = true;
    }

}

