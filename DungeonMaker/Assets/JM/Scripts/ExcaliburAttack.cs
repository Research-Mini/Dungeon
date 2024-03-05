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
        // 충돌한 객체의 레이어가 "Boss_HP"인지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
        {
            // 보스 몬스터에게 데미지를 줍니다.
            if (boss != null) // boss가 null이 아닌지 확인
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
        else if (collision.gameObject.tag == "Enemy") // 미니맵으로 인해 Layer가 MinimapVisible로 바뀐 경우
        {
            Debug.Log(collision.gameObject.name);
            // 보스 몬스터에게 데미지를 줍니다.
            if (boss != null) // boss가 null이 아닌지 확인
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
                // 일반 몬스터
                if (collision.transform.parent.parent.gameObject.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
                {
                    Debug.Log("Attack Monster");
                    if (PlayerStats.instance == null)
                        monsterHP.TakeDamage(100);
                    else
                    {
                        bool isMonsterDead = monsterHP.TakeDamage(PlayerStats.instance.attackPower);
                        Debug.Log(isMonsterDead);
                        // 일기토 중이었으면 방의 일기토 설정 해제.
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

                        // 일기토 중이었으면 방의 일기토 설정 해제.
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
    //    // 충돌한 객체의 레이어가 "Boss_HP"인지 확인
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
    //    {
    //        // 보스 몬스터에게 데미지를 줍니다.
    //        if (boss != null) // boss가 null이 아닌지 확인
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
    //    else if (other.gameObject.tag == "Enemy") // 미니맵으로 인해 Layer가 MinimapVisible로 바뀐 경우
    //    {
    //        // 보스 몬스터에게 데미지를 줍니다.
    //        if (boss != null) // boss가 null이 아닌지 확인
    //        {
    //            if (PlayerStats.instance == null)
    //                boss.GetComponent<MonsterHealth>().TakeDamage(100);
    //            else
    //                boss.GetComponent<MonsterHealth>().TakeDamage(PlayerStats.instance.attackPower);
    //            Debug.Log("OnCollisionEnter called with " + other.gameObject.name);
    //        }
    //        else
    //        {
    //            // 일반 몬스터
    //            if (other.gameObject.TryGetComponent<MonsterHealth>(out MonsterHealth monsterHP))
    //            {
    //                Debug.Log("Attack Monster");
    //                if (PlayerStats.instance == null)
    //                    monsterHP.TakeDamage(100);
    //                else
    //                {
    //                    bool isMonsterDead = monsterHP.TakeDamage(PlayerStats.instance.attackPower);

    //                    // 일기토 중이었으면 방의 일기토 설정 해제.
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
