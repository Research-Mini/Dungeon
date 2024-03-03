using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int attackPower = 10;
    public int defensePower = 5;

    public void TakeDamage(int damage)
    {
        int damageTaken = damage - defensePower;
        currentHealth -= damageTaken > 0 ? damageTaken : 0;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Á×¾úÀ»¶§
        Debug.Log("Player Died");
    }
}