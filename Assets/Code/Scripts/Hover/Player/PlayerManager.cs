using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Movement PlayerMovement;
    private StepController StepController;
    private UsableManager UsableManager;

    public static UnityEvent ForceMovement;
    public static UnityEvent ForceRotation;
    public static UnityEvent FreeMovement;

    [SerializeField] KeyCode[] UsableKeys = new KeyCode[3]{KeyCode.A, KeyCode.S, KeyCode.D};

    [HideInInspector] public static bool VisibleToEnemy = true;
    [HideInInspector] public static bool Invulnerability;

    private bool MovementForced = false;
    private bool RotationForced = false;
    private float SpeedLimiterBeforeForced;

    private void Awake()
    {
        PlayerMovement = GetComponent<Movement>();
        StepController = GetComponent<StepController>();
        UsableManager = GetComponent<UsableManager>();
        Invulnerability = false;

        if(ForceMovement == null)
            ForceMovement = new UnityEvent();
        if (FreeMovement == null)
            FreeMovement = new UnityEvent();
        if(ForceRotation == null)
            ForceRotation = new UnityEvent();

        ForceMovement.AddListener(OnForceMovement);
        ForceRotation.AddListener(OnForceRotation);
        FreeMovement.AddListener(OnFreeMovement);
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
        if (!MovementForced)
        {
            //Move
            PlayerMovement.Move(Input.GetAxisRaw("Vertical"));
            //Check if a stair is close enought to jump onto
            StepController.CheckStep(PlayerMovement.ClampedVelocity);
        }
        if(MovementForced)
            PlayerMovement.Move(1);

        //rotate
        if(!RotationForced)
            PlayerMovement.Rotate(Input.GetAxisRaw("Horizontal"));

        //check if invulnerable to remove speed edit effect
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

    private void OnForceMovement()
    {
        MovementForced = true;
        PlayerMovement.SpeedLimiter = 1f;
        PlayerMovement.Continue();
    }

    private void OnForceRotation()
    {
        RotationForced = true;
        MovementForced = true;
        PlayerMovement.Halt();
    }

    private void OnFreeMovement()
    {
        PlayerMovement.SpeedLimiter = PlayerMovement.StartSpeedLimiter;
        MovementForced = false;
        RotationForced = false;
    }
}
