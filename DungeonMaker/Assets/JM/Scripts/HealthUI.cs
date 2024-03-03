using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;
    public Slider healthSlider; 

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
         healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

        UpdateHealth(PlayerStats.instance.currentHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        // 못 찾았다면 다시 찾도록!
        if(healthSlider == null)
            healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        if (healthSlider != null)
        {
            Debug.Log(currentHealth);
            healthSlider.value = currentHealth;
        }
    }

}
