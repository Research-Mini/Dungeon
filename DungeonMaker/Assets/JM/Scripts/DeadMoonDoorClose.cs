using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMoonDoorClose : MonoBehaviour
{
    public AudioSource OpenSound;
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject frontPart;
    public float rotateAmount = 90f;
    public float duration = 1.5f;

    private bool isDoorOpen = false; 
    private bool leftDoorClosed = false; 
    private bool rightDoorClosed = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            OpenSound.Play();
            StartCoroutine(RotateDoor(doorLeft, rotateAmount, () => leftDoorClosed = true)); 
            StartCoroutine(RotateDoor(doorRight, -rotateAmount, () => rightDoorClosed = true));
            isDoorOpen = true; 
        }
    }

    IEnumerator RotateDoor(GameObject door, float angle, System.Action onCompleted)
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

       
        onCompleted?.Invoke();

        
        if (leftDoorClosed && rightDoorClosed)
        {
            frontPart.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
