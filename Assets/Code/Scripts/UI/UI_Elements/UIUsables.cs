using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUsables : MonoBehaviour
{
    [Header("Power Up Quantity")]
    [SerializeField] TMP_Text PowerUp1;
    [SerializeField] TMP_Text PowerUp2;
    [SerializeField] TMP_Text PowerUp3;

    public void SetUsableAmount(int p1, int p2, int p3)
    {
        PowerUp1.SetText(p1.ToString());
        PowerUp1.SetText(p2.ToString());
        PowerUp1.SetText(p3.ToString());
    }
}
