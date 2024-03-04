using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public HealthBar healthBar; // 체력바 UI를 위한 참조

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // 체력바의 최대값 설정
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("mosterdamaged");
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력이 0 이하로 떨어지지 않도록 함
        healthBar.SetHealth(currentHealth); // 체력바 업데이트

        if (currentHealth <= 0)
        {
            // 몬스터가 죽었을 때의 로직 처리
            Debug.Log("Monster is dead!");
        }
    }
}
