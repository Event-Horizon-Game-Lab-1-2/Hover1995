using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [Tooltip("Aperture of the rays")]
    [SerializeField] Vector3 StepRayAperture = new Vector3(0.5f, 0.0f, 0.0f);
    [Tooltip("Layer that collide with Raycasts")]
    [SerializeField] LayerMask StepLayerMask;
    [Tooltip("Color of the preview")]
    [SerializeField] private Color StepDetectorColor = Color.yellow;
    [Tooltip("Color of the rays")]
    [SerializeField] private Color RayColor = Color.magenta;
    #endregion

    private Rigidbody Body;

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    public void CheckStep(float linearSpeed)
    {
        if (Body.velocity == Vector3.zero)
            return;

        bool step = false;
        float rayLenght = Mathf.Clamp(StepDetectorLenght * linearSpeed, StepDetectorStartLenght, StepDetectorLenght);
        Vector3 rayDir = new Vector3(Body.velocity.normalized.x, 0.0f, Body.velocity.normalized.z);
        //Create rays var
        Ray[] rays = new Ray[4];
        //Lower Rays
        rays[0] = new Ray(new Vector3(transform.position.x, transform.position.y + StartY, transform.position.z), rayDir + StepRayAperture);
        rays[1] = new Ray(new Vector3(transform.position.x, transform.position.y + StartY, transform.position.z), rayDir - StepRayAperture);
        //Upper Rays
        rays[2] = new Ray(new Vector3(transform.position.x, transform.position.y + StartY + StepHeight, transform.position.z), rayDir + StepRayAperture);
        rays[3] = new Ray(new Vector3(transform.position.x, transform.position.y + StartY + StepHeight, transform.position.z), rayDir - StepRayAperture);

        step = IsAStep(rays, rayLenght);

        if (step)
            transform.position = new Vector3(transform.position.x, transform.position.y + StepHeight, transform.position.z);

        //DEBUG Ray
        for(int i = 0; i < rays.Length; i++)
            Debug.DrawRay(rays[i].origin, rays[i].direction * rayLenght, RayColor, Time.deltaTime);
    }

    private bool IsAStep(Ray[] rays, float rayLenght)
    {
        bool result = false;
        //upperrays
        for (int i = rays.Length / 2; i < rays.Length; i++)
            if (Physics.Raycast(rays[i], out RaycastHit hit, rayLenght, StepLayerMask))
                return false;

        //lower rays
        for (int i = 0; i < rays.Length/2; i++)
            if (Physics.Raycast(rays[i], out RaycastHit hit, rayLenght, StepLayerMask))
                result = true;

        return result;
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        #region Draw Step Detection preview
        Gizmos.color = StepDetectorColor;
        if (!EditorApplication.isPlaying)
        {
            //Ray direction
            Vector3 direction = Vector3.forward * StepDetectorLenght;
            //All ray start in 0.0f xf 0.0f locally
            Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + StartY, transform.position.z);
            //Draw Lower Rays
            Gizmos.DrawRay(rayOrigin, direction + StepRayAperture);
            Gizmos.DrawRay(rayOrigin, direction - StepRayAperture);
            //Draw Upper Ray
            rayOrigin.y += StepHeight;
            Gizmos.DrawRay(rayOrigin, direction + StepRayAperture);
            Gizmos.DrawRay(rayOrigin, direction - StepRayAperture);
        }
        #endregion
        #endif
    }
}