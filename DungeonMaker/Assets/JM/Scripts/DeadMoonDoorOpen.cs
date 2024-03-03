using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMoonDoorOpen : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;
    public float rotateAmount = 90f;
    public float duration = 1.5f;

    private bool isDoorOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            StartCoroutine(RotateDoor(doorLeft, -rotateAmount)); 
            StartCoroutine(RotateDoor(doorRight, rotateAmount)); 
            isDoorOpen = true; 
        }
    }

 
    IEnumerator RotateDoor(GameObject door, float angle)
    {
        Quaternion startRotation = door.transform.rotation;
        Quaternion endRotation = door.transform.rotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            door.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.rotation = endRotation;
    
}
}
