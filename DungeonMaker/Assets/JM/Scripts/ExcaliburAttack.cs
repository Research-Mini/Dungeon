using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExcaliburAttack : MonoBehaviour
{
    public Character chara;
    public GameObject boss;

    public void WinGame()
    {
        StartCoroutine(CO_WinGame());
    }

    IEnumerator CO_WinGame()
    {
        GameObject WinPopup = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        WinPopup.SetActive(true);

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainScene");
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� ���̾ "Boss_HP"���� Ȯ��
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
        {
            // ���� ���Ϳ��� �������� �ݴϴ�.
            if (boss != null) // boss�� null�� �ƴ��� Ȯ��
            {
                if (PlayerStats.instance == null)
                    boss.GetComponent<MonsterHealth>().TakeDamage(100);
                else
                {
                    bool isMonsterDead = boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);

                    if (isMonsterDead)
                    {
                        WinGame();
                    }
                }

                Debug.Log("OnCollisionEnter called with " + collision.gameObject.name);
            }
            else
            {
                if (collision.gameObject.transform.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
                {
                    if (PlayerStats.instance == null)
                        monsterHP.TakeDamage(100);
                    else
                    {
                        bool isMonsterDead = monsterHP.TakeDamage(PlayerStats.instance.attackPower);

                        if (isMonsterDead)
                        {
                            WinGame();
                        }
                    }
                }
                // Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
            }
        }
        else if (collision.gameObject.tag == "Enemy") // �̴ϸ����� ���� Layer�� MinimapVisible�� �ٲ� ���
        {
            Debug.Log(collision.gameObject.name);
            // ���� ���Ϳ��� �������� �ݴϴ�.
            if (boss != null) // boss�� null�� �ƴ��� Ȯ��
            {
                if (PlayerStats.instance == null)
                    boss.GetComponent<MonsterHealth>().TakeDamage(100);
                else
                {
                    bool isMonsterDead = boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
                    if (isMonsterDead)
                    {
                        chara.monsterDeathSound.Play();
                    }
                }
                Debug.Log("OnCollisionEnter called with " + collision.gameObject.name);
            }
            else
            {
                // �Ϲ� ����
                if (collision.transform.parent.parent.gameObject.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
                {
                    Debug.Log("Attack Monster");
                    if (PlayerStats.instance == null)
                        monsterHP.TakeDamage(100);
                    else
                    {
                        bool isMonsterDead = monsterHP.TakeDamage(PlayerStats.instance.attackPower);
                        Debug.Log(isMonsterDead);
                        // �ϱ��� ���̾����� ���� �ϱ��� ���� ����.
                        if (isMonsterDead)
                        {
                            if (chara != null)
                            {
                                chara.monsterDeathSound.Play();
                                if (chara.isIllGiTo)
                                {
                                    chara.EndOfIllGiTo();
                                }
                            }
                        }
                    }
                }
                else if(collision.gameObject.TryGetComponent<MonsterHealth>(out MonsterHealth monstersHP))
                {
                    Debug.Log("Attack Monster");
                    if (PlayerStats.instance == null)
                        monstersHP.TakeDamage(100);
                    else
                    {
                        bool isMonsterDead = monstersHP.TakeDamage(PlayerStats.instance.attackPower);

                        // �ϱ��� ���̾����� ���� �ϱ��� ���� ����.
                        if (isMonsterDead)
                        {
                            if (chara != null)
                            {
                                chara.monsterDeathSound.Play();
                                if (chara.isIllGiTo)
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

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //    // �浹�� ��ü�� ���̾ "Boss_HP"���� Ȯ��
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
    //    {
    //        // ���� ���Ϳ��� �������� �ݴϴ�.
    //        if (boss != null) // boss�� null�� �ƴ��� Ȯ��
    //        {
    //            if (PlayerStats.instance == null)
    //                boss.GetComponent<MonsterHealth>().TakeDamage(100);
    //            else
    //                boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
    //            Debug.Log("OnCollisionEnter called with " + other.gameObject.name);
    //        }
    //        else
    //        {
    //            if (other.gameObject.transform.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
    //            {
    //                if (PlayerStats.instance == null)
    //                    monsterHP.TakeDamage(100);
    //                else
    //                    monsterHP.TakeDamage(PlayerStats.instance.attackPower);
    //            }
    //            // Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
    //        }
    //    }
    //    else if (other.gameObject.tag == "Enemy") // �̴ϸ����� ���� Layer�� MinimapVisible�� �ٲ� ���
    //    {
    //        // ���� ���Ϳ��� �������� �ݴϴ�.
    //        if (boss != null) // boss�� null�� �ƴ��� Ȯ��
    //        {
    //            if (PlayerStats.instance == null)
    //                boss.GetComponent<MonsterHealth>().TakeDamage(100);
    //            else
    //                boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
    //            Debug.Log("OnCollisionEnter called with " + other.gameObject.name);
    //        }
    //        else
    //        {
    //            // �Ϲ� ����
    //            if (other.gameObject.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
    //            {
    //                Debug.Log("Attack Monster");
    //                if (PlayerStats.instance == null)
    //                    monsterHP.TakeDamage(100);
    //                else
    //                {
    //                    bool isMonsterDead = monsterHP.TakeDamage(PlayerStats.instance.attackPower);

    //                    // �ϱ��� ���̾����� ���� �ϱ��� ���� ����.
    //                    if(isMonsterDead)
    //                    {
    //                        if(chara != null)
    //                        {
    //                            if(chara.isIllGiTo)
    //                            {
    //                                chara.EndOfIllGiTo();
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //            // Debug.LogError("Boss object is not assigned in ExcaliburAttack script.");
    //        }
    //    }
    //}
}
