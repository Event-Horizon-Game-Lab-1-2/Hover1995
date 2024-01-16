using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class StepController : MonoBehaviour
{
    #region Steps Parameters
    [Header("Steps Parameters")]
    [Tooltip("Start Y position of the detector")]
    [SerializeField] private float StartY = 0f;
    //[Tooltip("End position of the detectors")]
    //[SerializeField] private Vector3 EndDetectorPosition = Vector3.zero;
    [Tooltip("Height of the step")]
    [SerializeField] private float StepHeight = 1.0f;
    [Tooltip("Max Legth of the detector")]
    [SerializeField] private float StepDetectorLenght = 0.5f;
    [Tooltip("Min Legth of the detector")]
    [SerializeField] private float StepDetectorStartLenght = 0.2f;

    [Tooltip("Layer that collide with Raycasts")]
    [SerializeField] LayerMask StepLayerMask;
    [Tooltip("Color of the detector")]
    [SerializeField] private Color StepDetectorColor = Color.yellow;
    #endregion

    private Rigidbody Body;

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    public void CheckStep(float linearSpeed)
    {
        bool step = false;
        float rayLenght = Mathf.Clamp(StepDetectorLenght * linearSpeed, StepDetectorStartLenght, StepDetectorLenght);
        Vector3 rayDir = new Vector3(Body.velocity.normalized.x, 0.0f, Body.velocity.normalized.z);
        //Create rays var
        Ray lowerRay = new Ray(new Vector3(transform.position.x, transform.position.y + StartY, transform.position.z), rayDir);
        Ray upperRay = new Ray(new Vector3(transform.position.x, transform.position.y + StartY + StepHeight, transform.position.z), rayDir);

        if (Physics.Raycast(lowerRay, out RaycastHit lowerHit, rayLenght, StepLayerMask))
            step = true;
        if (Physics.Raycast(upperRay, out RaycastHit upperHit, rayLenght, StepLayerMask))
            step = false;

        if (step)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + StepHeight, transform.position.z);
        }

        //DEBUG Ray
        Debug.DrawRay(lowerRay.origin, lowerRay.direction * rayLenght, Color.red, Time.deltaTime);
        Debug.DrawRay(upperRay.origin, upperRay.direction * rayLenght, Color.red, Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        #region Draw Step Detection
        Gizmos.color = StepDetectorColor;

        //Ray direction
        Vector3 direction = Vector3.forward * StepDetectorLenght;
        //All ray start in 0.0f xf 0.0f locally
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + StartY, transform.position.z);
        //Draw Lower Ray
        Gizmos.DrawRay(rayOrigin, direction);
        //Draw Upper Ray
        rayOrigin.y += StepHeight;
        Gizmos.DrawRay(rayOrigin, direction);
        #endregion
    }
}