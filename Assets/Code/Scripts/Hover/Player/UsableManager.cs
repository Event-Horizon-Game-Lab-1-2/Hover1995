using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableManager : MonoBehaviour
{
    [Header("Usables")]
    [SerializeField] SpawnerData[] UsableSpawnerData = new SpawnerData[3];
    public int[] ObtainedUsableAmount = new int[3];

    public void useUsable(int usableIndex)
    {

        if (ObtainedUsableAmount[usableIndex] <= 0)
            return;

        if (UsableSpawnerData[usableIndex] != null)
        {
            Interactable usableInteractable = UsableSpawnerData[usableIndex].GetComponent<Interactable>();
            if(usableInteractable != null)
            {
                usableInteractable.Trigger(this);
                ObtainedUsableAmount[usableIndex]--;
            }
            else
                Debug.LogWarning("Error while obtaining usable Interactable");
        }
    }

}
