using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSound : MonoBehaviour
{
    public AudioSource shieldSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Axe")
        {
            shieldSound.Play();
        }
    }
}
