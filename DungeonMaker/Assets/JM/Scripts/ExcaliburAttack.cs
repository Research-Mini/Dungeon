using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburAttack : MonoBehaviour
{
    public Character chara;
    public GameObject boss;
    
    //// Start is called before the first frame update
    //void OnCollisionEnter(Collision collision)
    //{
    //    // �浹�� ��ü�� ���̾ "Boss_HP"���� Ȯ��
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
    //    {
    //        // ���� ���Ϳ��� �������� �ݴϴ�.
    //        if (boss != null) // boss�� null�� �ƴ��� Ȯ��
    //        {
    //            if(PlayerStats.instance == null)
    //                boss.GetComponent<MonsterHealth>().TakeDamage(100);
    //            else
    //                boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
    //            Debug.Log("OnCollisionEnter called with " + collision.gameObject.name);
    //        }
    //        else
    //        {
    //            if(collision.gameObject.transform.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
    //            {
    //                if (PlayerStats.instance == null)
    //                    monsterHP.TakeDamage(100);
    //                else
    //                    monsterHP.TakeDamage(PlayerStats.instance.attackPower);
    //            }
    //            // Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        // �浹�� ��ü�� ���̾ "Boss_HP"���� Ȯ��
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
        {
            // ���� ���Ϳ��� �������� �ݴϴ�.
            if (boss != null) // boss�� null�� �ƴ��� Ȯ��
            {
                if (PlayerStats.instance == null)
                    boss.GetComponent<MonsterHealth>().TakeDamage(100);
                else
                    boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
                Debug.Log("OnCollisionEnter called with " + other.gameObject.name);
            }
            else
            {
                if (other.gameObject.transform.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
                {
                    if (PlayerStats.instance == null)
                        monsterHP.TakeDamage(100);
                    else
                        monsterHP.TakeDamage(PlayerStats.instance.attackPower);
                }
                // Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
            }
        }
        else if (other.gameObject.tag == "Enemy") // �̴ϸ����� ���� Layer�� MinimapVisible�� �ٲ� ���
        {
            // ���� ���Ϳ��� �������� �ݴϴ�.
            if (boss != null) // boss�� null�� �ƴ��� Ȯ��
            {
                if (PlayerStats.instance == null)
                    boss.GetComponent<MonsterHealth>().TakeDamage(100);
                else
                    boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
                Debug.Log("OnCollisionEnter called with " + other.gameObject.name);
            }
            else
            {
                // �Ϲ� ����
                if (other.gameObject.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
                {
                    Debug.Log("Attack Monster");
                    if (PlayerStats.instance == null)
                        monsterHP.TakeDamage(100);
                    else
                    {
                        bool isMonsterDead = monsterHP.TakeDamage(PlayerStats.instance.attackPower);

                        // �ϱ��� ���̾����� ���� �ϱ��� ���� ����.
                        if(isMonsterDead)
                        {
                            if(chara != null)
                            {
                                if(chara.isIllGiTo)
                                {
                                    chara.EndOfIllGiTo();
                                }
                            }
                        }
                    }
                }
                // Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
            }
        }
    }
}
