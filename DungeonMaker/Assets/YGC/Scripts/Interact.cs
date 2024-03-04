using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public LayerMask oppositeLayer;     // 플레이어를 공격하는 레이어 (상대방의 레이어)    

    private void OnTriggerEnter(Collider other)
    {
        // Layermask 값은 2의 제곱수로 주어짐. Layer가 9면, Layermask.value = 2^9 = 512
        if (Mathf.Pow(2, other.gameObject.layer) == oppositeLayer.value)
        {
            Debug.Log("Attacked BY " + other.gameObject);
            // 받는 피해 양은, collision.GetComponent<적의 공격 수치 담은 스크립트>(). ... 해서 가져와야 함.

            // 일단 50 피해 받는다고 가정.
            PlayerStats.instance.TakeDamage(50);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Attacked BY " + other.gameObject);
            // 받는 피해 양은, collision.GetComponent<적의 공격 수치 담은 스크립트>(). ... 해서 가져와야 함.

            // 일단 50 피해 받는다고 가정.
            PlayerStats.instance.TakeDamage(50);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{                
    //    // Layermask 값은 2의 제곱수로 주어짐. Layer가 9면, Layermask.value = 2^9 = 512
    //    if (Mathf.Pow(2, collision.gameObject.layer) == oppositeLayer.value)
    //    {
    //        Debug.Log("Attacked BY " + collision.gameObject);
    //        // 받는 피해 양은, collision.GetComponent<적의 공격 수치 담은 스크립트>(). ... 해서 가져와야 함.

    //        // 일단 50 피해 받는다고 가정.
    //        PlayerStats.instance.TakeDamage(50);
    //    }
    //}
}
