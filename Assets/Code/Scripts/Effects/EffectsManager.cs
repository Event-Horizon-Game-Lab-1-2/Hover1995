using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    //usable timers --- float is the deacay time
    public static UnityEvent<float> WallUsed = new UnityEvent<float>();
    public static UnityEvent<float> InvisibilityUsed = new UnityEvent<float>();
    //Effect timers
    public static UnityEvent<float> InvulnerabilityUsed = new UnityEvent<float>();
    public static UnityEvent<float> SpeedEditUsed = new UnityEvent<float>();

    //Timers
    public static float InvulnerabilityTime { get; private set; } = 0f;
    public static float InvulnerabilityStartTime { get; private set; } = 0f;
    public static float WallTime { get; private set; } = 0f;
    public static float WallStartTime { get; private set; } = 0f;
    public static float InvisibilityTime { get; private set; } = 0f;
    public static float InvisibilityStartTime { get; private set; } = 0f;
    public static float SpeedEditTime { get; private set; } = 0f;
    public static float SpeedEditStartTime { get; private set; } = 0f;
    
    private void Awake()
    {
        //create all unity events
        //usable Events
        if(WallUsed == null)
            WallUsed = new UnityEngine.Events.UnityEvent<float>();

        if(InvisibilityUsed == null)
            InvisibilityUsed = new UnityEngine.Events.UnityEvent<float>();

        if(InvulnerabilityUsed == null)
            InvulnerabilityUsed = new UnityEngine.Events.UnityEvent<float>();

        if (SpeedEditUsed == null)
            SpeedEditUsed = new UnityEvent<float>();

        //add all listener
        WallUsed.AddListener(OnWallUsed);
        InvisibilityUsed.AddListener(OnInvisibilityEnabled);
        InvulnerabilityUsed.AddListener(OnVulnerabilityUsed);
        SpeedEditUsed.AddListener(OnSpeedEditUsed);
    }

    private void FixedUpdate()
    {
        //decrease all timers and set to 0 if < 0
        if(InvulnerabilityTime > 0)
        {
            InvulnerabilityTime-=Time.fixedDeltaTime;
            if(InvulnerabilityTime < 0f)
                InvulnerabilityTime = 0f;
        }

        if (WallTime > 0)
        {
            WallTime-=Time.fixedDeltaTime;
            if(WallTime < 0f)
                WallTime = 0f;
        }

        if(InvisibilityTime > 0)
        {
            InvisibilityTime-=Time.fixedDeltaTime;
            if(InvisibilityTime < 0f)
                InvisibilityTime = 0f;
        }

        if(SpeedEditTime > 0)
        {
            SpeedEditTime-=Time.fixedDeltaTime;
            if(SpeedEditTime < 0f)
                SpeedEditTime = 0f;
        }


    }

    private void OnVulnerabilityUsed(float time)
    {
        InvulnerabilityStartTime = time;
        InvulnerabilityTime = time;
    }

    private void OnWallUsed(float time)
    {
        WallStartTime = time;
        WallTime = time;
    }

    private void OnSpeedEditUsed(float time)
    {
        SpeedEditStartTime = time;
        SpeedEditTime = time;
    }

    private void OnInvisibilityEnabled(float time)
    {
        InvisibilityStartTime = time;
        InvisibilityTime = time;
    }


}
