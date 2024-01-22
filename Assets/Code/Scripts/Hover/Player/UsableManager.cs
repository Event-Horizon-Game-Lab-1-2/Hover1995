using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsableManager : MonoBehaviour
{
    public static UnityEvent<int, int> ObtainUsable = new UnityEvent<int, int>();

    [Header("Usables")]
    [SerializeField] Effect[] UsableEffect = new Effect[3];
    [SerializeField] int MaxusableAmount = 9;
    [HideInInspector] public int[] ObtainedUsableAmount = new int[3] {0,0,0};

    private void Awake()
    {
        ObtainUsable.AddListener(OnObtainUsable);
    }

    public void useUsable(int usableIndex)
    {

        if (ObtainedUsableAmount[usableIndex] <= 0)
            return;

        if (UsableEffect[usableIndex] != null)
        {
            Effect usableInteractable = UsableEffect[usableIndex];
            if(usableInteractable != null)
            {
                usableInteractable.ApplyEffect(this);
                ObtainedUsableAmount[usableIndex]--;
            }
            else
                Debug.LogWarning("Error while obtaining usable Interactable");
        }
    }

    private void OnObtainUsable(int index, int quantity)
    {
        if (ObtainedUsableAmount[index] < MaxusableAmount)
            ObtainedUsableAmount[index] += quantity;
    }
}