using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsableManager : MonoBehaviour
{
    public static UnityEvent<int, int> ObtainUsable;

    [Header("Usables")]
    [SerializeField] Effect[] UsableEffect = new Effect[3];
    [SerializeField] public int[] ObtainedUsableAmount = new int[3];

    private void Awake()
    {
        if (ObtainUsable != null)
            ObtainUsable = new UnityEvent<int, int>();
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
}