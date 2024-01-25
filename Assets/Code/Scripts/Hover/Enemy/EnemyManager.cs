using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Patrolling Options")]
    [Tooltip("Position manager")]
    [SerializeField] PatrollingPositionManager PatrollingPositions;
    [Tooltip("If distance is less than this it will go towards the next position")]
    [SerializeField] float PatrollingSafeMargin;
    [SerializeField] float StunSecAfterBounce;

    [Header("Vision")]
    [SerializeField][Range(4, 48)] int RaysAmount = 6;
    [SerializeField] float RaysLenght = 5;
    [SerializeField][Range(0f, 2.5f)] float RaysAperture = 0.5f;

    [Header("Collision")]
    [Tooltip("Is the force applied when colliding with a collider")]
    [SerializeField] private float MaxBounceForce = 20f;
    [SerializeField] private float MinBounceForce = 10f;
    [Space]

    [Header("Targetting Options")]
    [SerializeField] LayerMask TargetLayer;
    [SerializeField] float ChasingSpeedMultiplier = 0.5f;
    [SerializeField] float LeaveChaseDistance = 30f;

    private bool IsChasing = false;
    private bool SpeedIncreased = false;
    private bool IsStunned = false;

    GameObject ChasingObject;
    Transform TargetTransform;
    NavMeshAgent Agent;
    Rigidbody Body;
    float StartingSpeed;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TargetTransform = PatrollingPositions.GetStartTransform();
        Body.position = new Vector3(TargetTransform.position.x, Body.position.y, TargetTransform.position.z);
        TargetTransform = PatrollingPositions.GetNextTarget();
        Agent.SetDestination(TargetTransform.position);
        StartingSpeed = Agent.speed;
    }

    private void FixedUpdate()
    {
        if (IsStunned)
            return;

        UpdateRays();

        if(!IsChasing)
        {
            if (Vector3.Distance(transform.position, TargetTransform.position) < PatrollingSafeMargin)
            {
                TargetTransform = PatrollingPositions.GetNextTarget();
                Agent.SetDestination(TargetTransform.position);
            }
        }
        else
        {
            if (ChasingObject == null)
            {
                IsChasing = false;
                Agent.SetDestination(TargetTransform.position);
                return;
            }
            
            if (Vector3.Distance(transform.position, ChasingObject.transform.position) < LeaveChaseDistance)
            {
                Agent.SetDestination(ChasingObject.transform.position);
                //if is chasing player
                if (ChasingObject.GetComponent<PlayerManager>() != null)
                    IsChasing = PlayerManager.VisibleToEnemy;
            }
            else
            {
                ChasingObject = null;
                IsChasing = false;
                Agent.SetDestination(TargetTransform.position);
            }
        }
    }

    private void UpdateRays()
    {
        Gizmos.color = Color.magenta;
        float singleRayAperture = RaysAperture / RaysAmount;

        for (int i = 0; i < RaysAmount / 2; i++)
        {
            Vector3 direction;
            direction = Vector3.Lerp(transform.forward, -transform.right, singleRayAperture * i).normalized;
            direction *= RaysLenght;
            Ray ray = new Ray(transform.position, direction);

            if(Physics.Raycast(ray, out RaycastHit hit, RaysLenght, TargetLayer))
            {
                ChasingObject = hit.collider.gameObject;

                if (ChasingObject.GetComponent<PlayerManager>() == null)
                    IsChasing = true;
                else
                    IsChasing = PlayerManager.VisibleToEnemy;
            }
        }

        for (int i = 0; i < RaysAmount / 2; i++)
        {
            Vector3 direction;
            direction = Vector3.Lerp(transform.forward, transform.right, singleRayAperture * i).normalized;
            direction *= RaysLenght;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, RaysLenght, TargetLayer))
            {
                ChasingObject = hit.collider.gameObject;

                if (ChasingObject.GetComponent<PlayerManager>() == null)
                    IsChasing = true;
                else
                    IsChasing = PlayerManager.VisibleToEnemy;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float oldSpeed = Agent.speed;
        StartCoroutine(Stun(collision));
    }

    IEnumerator Stun(Collision collision)
    {
        IsStunned = true;
        //stop the agent
        Agent.speed = 0f;
        Agent.velocity = Vector3.zero;
        if(Agent.isOnNavMesh)
            Agent.isStopped = true;
        //disable the agent
        Agent.enabled = false;

        
        //bounce
        /*
        Bounce(collision);
        */
        //wait
        yield return new WaitForSeconds(StunSecAfterBounce);
        
        //enable agent
        Agent.velocity = Vector3.zero;
        Agent.enabled = true;
        Agent.isStopped = false;
        Agent.speed = StartingSpeed;

        //recover target pos
        if (IsChasing)
            Agent.SetDestination(ChasingObject.transform.position);
        else
            Agent.SetDestination(TargetTransform.position);
        //no more stun
        IsStunned = false;
    }

    private void Bounce(Collision collision)
    {
        Body.isKinematic = true;
        //Apply Bounce
        if (collision.contacts[0].normal.y == 0)
        {
            //Collision amount
            int collsionAmount = collision.GetContacts(collision.contacts);
            //Collision normal
            Vector3 collisionNormal = Vector3.zero;
            //Averange collision point
            Vector3 collisionBouncePoint = Vector3.zero;

            //Get the averange collision point
            foreach (ContactPoint contacts in collision.contacts)
            {
                collisionNormal += contacts.normal;
                collisionBouncePoint += contacts.point;
            }
            collisionNormal /= collsionAmount;
            collisionBouncePoint /= collsionAmount;

            //Adjust the bounce force
            float forceApplied = MaxBounceForce * Agent.acceleration;
            forceApplied = Mathf.Clamp(forceApplied, MinBounceForce, MaxBounceForce);

            //Add the bounce force
            Body.AddForceAtPosition(new Vector3(forceApplied * collisionNormal.x, 0.0f, forceApplied * collisionNormal.z), collisionBouncePoint, ForceMode.Impulse);
        }
        Body.isKinematic = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        float singleRayAperture = RaysAperture / RaysAmount;

        for (int i = 0; i < RaysAmount / 2; i++)
        {
            Vector3 direction;
            direction = Vector3.Lerp(transform.forward, -transform.right, singleRayAperture * i).normalized;
            direction *= RaysLenght;
            Ray ray = new Ray(transform.position, direction);
            Gizmos.DrawRay(ray.origin, direction);
        }

        for (int i = 0; i < RaysAmount / 2; i++)
        {
            Vector3 direction;
            direction = Vector3.Lerp(transform.forward, transform.right, singleRayAperture * i).normalized;
            direction *= RaysLenght;
            Ray ray = new Ray(transform.position, direction);
            Gizmos.DrawRay(ray.origin, direction);
        }

        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, LeaveChaseDistance);
    }
#endif
}
