using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public HealthBar healthBar; // ü�¹� UI�� ���� ����

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // ü�¹��� �ִ밪 ����
    }

    public bool TakeDamage(int damage)
    {
        bool isMonsterDead = false;

        currentHealth -= damage;
        Debug.Log("mosterdamaged");
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ü���� 0 ���Ϸ� �������� �ʵ��� ��
        healthBar.SetHealth(currentHealth); // ü�¹� ������Ʈ

        if (currentHealth <= 0)
        {
            // ���Ͱ� �׾��� ���� ���� ó��
            gameObject.SetActive(false);
            Debug.Log("Monster is dead!");

            isMonsterDead = true;
        }

        return isMonsterDead;
    }
}