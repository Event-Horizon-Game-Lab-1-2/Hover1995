using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DirectionVisualizer : MonoBehaviour
{ 
    private RectTransform RectTransform;
    [SerializeField] private Image Image;
    [SerializeField] PlayerManager Player;
    private Rigidbody PlayerRigidbody;

    private void Start()
    {
        RectTransform = Image.GetComponent<RectTransform>();
        PlayerRigidbody = Player.GetComponent<Rigidbody>();
    }

    public void VisualizeDirection()
    {
        Image.fillAmount = Player.GetLinearVelocity();

        RectTransform.rotation = Quaternion.Euler( new Vector3(0.0f, 0.0f, Mathf.Atan2(Movement.AppliedForce.z, Movement.AppliedForce.x)) * Mathf.Rad2Deg);

        Debug.Log(transform.InverseTransformDirection(Movement.AppliedForce));
    }
}