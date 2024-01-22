using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Movement PlayerMovement;
    private StepController StepController;
    private UsableManager UsableManager;

    [SerializeField] KeyCode[] UsableKeys = new KeyCode[3]{KeyCode.A, KeyCode.S, KeyCode.D};

    [HideInInspector] public static bool VisibleToEnemy = true;
    [HideInInspector] public static bool Invulnerability;
    private void Awake()
    {
        PlayerMovement = GetComponent<Movement>();
        StepController = GetComponent<StepController>();
        UsableManager = GetComponent<UsableManager>();
        Invulnerability = false;
    }

    private void Update()
    {
        //Use the usable
        for (int i = 0; i < UsableKeys.Length; i++)
            if (Input.GetKeyDown(UsableKeys[i]))
                UsableManager.useUsable(i);
    }

    private void FixedUpdate()
    {
        //Move
        PlayerMovement.Move(Input.GetAxisRaw("Vertical"));
        PlayerMovement.Rotate(Input.GetAxisRaw("Horizontal"));
        //Check if a stair is close enought to jump onto
        StepController.CheckStep(PlayerMovement.ClampedVelocity);
        //check if invulnerable
        if (Invulnerability)
            if (PlayerMovement.SpeedLimiter < PlayerMovement.StartSpeedLimiter)
            {
                EffectsManager.ResetSpeedEdit();
                PlayerMovement.SpeedLimiter = PlayerMovement.StartSpeedLimiter;
            }
    }

    public float GetLinearVelocity()
    {
        return PlayerMovement.ClampedVelocity;
    }
}
