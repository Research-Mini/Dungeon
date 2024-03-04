using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public LayerMask oppositeLayer;     // �÷��̾ �����ϴ� ���̾� (������ ���̾�)    

    private void OnTriggerEnter(Collider other)
    {
        // Layermask ���� 2�� �������� �־���. Layer�� 9��, Layermask.value = 2^9 = 512
        if (Mathf.Pow(2, other.gameObject.layer) == oppositeLayer.value)
        {
            Debug.Log("Attacked BY " + other.gameObject);
            // �޴� ���� ����, collision.GetComponent<���� ���� ��ġ ���� ��ũ��Ʈ>(). ... �ؼ� �����;� ��.

            // �ϴ� 50 ���� �޴´ٰ� ����.
            PlayerStats.instance.TakeDamage(50);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Attacked BY " + other.gameObject);
            // �޴� ���� ����, collision.GetComponent<���� ���� ��ġ ���� ��ũ��Ʈ>(). ... �ؼ� �����;� ��.

            // �ϴ� 50 ���� �޴´ٰ� ����.
            PlayerStats.instance.TakeDamage(50);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{                
    //    // Layermask ���� 2�� �������� �־���. Layer�� 9��, Layermask.value = 2^9 = 512
    //    if (Mathf.Pow(2, collision.gameObject.layer) == oppositeLayer.value)
    //    {
    //        Debug.Log("Attacked BY " + collision.gameObject);
    //        // �޴� ���� ����, collision.GetComponent<���� ���� ��ġ ���� ��ũ��Ʈ>(). ... �ؼ� �����;� ��.

    //        // �ϴ� 50 ���� �޴´ٰ� ����.
    //        PlayerStats.instance.TakeDamage(50);
    //    }
    //}
}
