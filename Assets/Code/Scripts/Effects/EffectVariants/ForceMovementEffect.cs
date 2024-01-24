using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ForceMovementEffect : Effect
{
    [SerializeField] float RotationSpeed = 30f;
    [SerializeField] float ForceSpeedDuration = 5f;
    private Transform Ref;
    private Transform StartRef;

    private bool Rotated = false;
    private float TimeCount = 0f;

    public override void ApplyEffect(GameObject gameObject)
    {
        Rotated = false;
        Ref = gameObject.transform;
        StartRef = Ref;
        TimeCount = 0f;
        StartCoroutine(ForceMovement());
    }

    IEnumerator ForceMovement()
    {
        PlayerManager.ForceRotation.Invoke();
        while (!Rotated)
        {
            Ref.rotation = Quaternion.Lerp(StartRef.rotation, transform.rotation, TimeCount * RotationSpeed);
            TimeCount += Time.deltaTime;
            if (TimeCount >= 1f)
                Rotated = true;
            yield return null;
        }
        PlayerManager.ForceMovement.Invoke();
        yield return new WaitForSeconds(ForceSpeedDuration);
        PlayerManager.FreeMovement.Invoke();
        Ref = null;
    }
}
