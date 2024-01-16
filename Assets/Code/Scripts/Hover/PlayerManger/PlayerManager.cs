using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Movement PlayerMovement;
    private StepController StepController;

    private void Awake()
    {
        PlayerMovement = GetComponent<Movement>();
        StepController = GetComponent<StepController>();
    }

    private void FixedUpdate()
    {
        //Move
        PlayerMovement.Move(Input.GetAxisRaw("Vertical"));
        PlayerMovement.Rotate(Input.GetAxisRaw("Horizontal"));
        //Check if a stair is close enought to jump onto
        StepController.CheckStep(PlayerMovement.ClampedVelocity);
    }

    public float GetLinearVelocity()
    {
        return PlayerMovement.ClampedVelocity;
    }
}
