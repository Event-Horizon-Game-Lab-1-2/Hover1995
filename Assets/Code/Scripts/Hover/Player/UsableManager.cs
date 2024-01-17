using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableManager : MonoBehaviour
{
    [Header("Usables")]
    [SerializeField] SpawnerData[] UsableSpawnerData = new SpawnerData[3];
    public int[] ObtainedUsableAmount = new int[3];

    private void Awake()
    {

    }

    public void useUsable(int usableIndex)
    {
        Interactable usableInteractable = UsableSpawnerData[usableIndex].GetComponent<Interactable>();
        if(usableInteractable != null)
        {
            if(ObtainedUsableAmount[usableIndex]>0)
                usableInteractable.Trigger(this, usableIndex);
        }
        else
        {
            Debug.LogWarning("Error while obtaining usable Interactable");
        }
    }

}
