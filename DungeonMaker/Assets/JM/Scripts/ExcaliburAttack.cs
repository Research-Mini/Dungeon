using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburAttack : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 레이어가 "Boss_HP"인지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss_HP"))
        {
            // 보스 몬스터에게 데미지를 줍니다.
            if (boss != null) // boss가 null이 아닌지 확인
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
