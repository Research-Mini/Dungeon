using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxHealth = 100;
    public int currentHealth = 100;
    public int attackPower = 10;
    public int defensePower = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(DecreaseHealthOverTime());
        HealthUI.instance.SetMaxHealth(maxHealth); 
    }

    IEnumerator DecreaseHealthOverTime()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(1); 
            TakeDamage(10); 
        }
    }

    public void TakeDamage(int damage)
    {
        int damageTaken = damage - defensePower;
        currentHealth -= damageTaken > 0 ? damageTaken : 0;
        if (currentHealth <= 0)
        {
            Die();
        }
       
        Debug.Log("Updating health UI.");
        if (HealthUI.instance != null)
        {
            HealthUI.instance.UpdateHealth(currentHealth);
        }
        else
        {
            Debug.LogError("HealthUI.instance is null!");
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {        
        if (HealthUI.instance != null)
        {
            // 씬 넘어갔을 때 갑자기 체력바 달라지는 이슈 수정
            currentHealth = maxHealth;

            // 아래의 이 함수가 작동을 안 함.
            // slider를 못찾는 것으로 보여서 찾는 구문 추가.
            HealthUI.instance.SetMaxHealth(maxHealth);

            HealthUI.instance.UpdateHealth(currentHealth);
        }
    }
}