using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburAttack : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� ���̾ "Boss_HP"���� Ȯ��
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
        {
            // ���� ���Ϳ��� �������� �ݴϴ�.
            if (boss != null) // boss�� null�� �ƴ��� Ȯ��
            {
                boss.GetComponent<MonsterHealth>().TakeDamage(100);
                Debug.Log("OnCollisionEnter called with " + collision.gameObject.name);
            }
            else
            {
                Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
            }
        }
    }
}
