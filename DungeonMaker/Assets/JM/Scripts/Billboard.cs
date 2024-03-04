using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform camTransform;

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.forward);
    }
}