using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void FlagObtained();
    public static event FlagObtained OnFlagObtained;

    public static GameManager Instance { get; private set; }

    [SerializeField] UIManager UI;
    [SerializeField] PlayerManager Player;

    public int BlueFlag;
    public int RedFlag;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        UI.SetUISpeed(Player.GetLinearVelocity());
    }
}
