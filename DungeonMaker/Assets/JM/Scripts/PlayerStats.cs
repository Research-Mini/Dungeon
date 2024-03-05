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

    public int sceneNum = 0;        // �� �Ѿ �� ���.
    private string[] sceneArr = { "Stage_02", "Stage_03", "DeadMoon" }; // �Ѿ ����.

    private GameObject statPopup;
    private GameObject selectPopup;

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
        //StartCoroutine(DecreaseHealthOverTime());
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
        Debug.Log("Playerdamaged");
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
        HealthUI.instance.UpdateHealth(0);
        Debug.Log("Player Died");

        StartCoroutine(OpenDiePopup());
    }

    IEnumerator OpenDiePopup()
    {
        GameObject Player = GameObject.FindWithTag("Player");

        Player.GetComponent<jm.PlayerController>().speed = 0;
        Player.GetComponent<jm.PlayerController>().isDie = true;

        Animator chara_Ani = GameObject.FindWithTag("Player").GetComponent<Animator>();
        chara_Ani.Play("Die");        

        GameObject DiePopup = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
        DiePopup.SetActive(true);

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("MainScene");
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
        // ���� �˾� ����� ��.
        Time.timeScale = 1f;

        if (HealthUI.instance != null)
        {
            // ���� ������ ������ ��� �ݿ�
            if (HealthUI.instance.isSelectHPUP)
                maxHealth += 50;
            else
                attackPower += 10;

            // �� �Ѿ�� �� ���ڱ� ü�¹� �޶����� �̽� ����
            currentHealth = maxHealth;

            // �Ʒ��� �� �Լ��� �۵��� �� ��.
            // slider�� ��ã�� ������ ������ ã�� ���� �߰�.
            HealthUI.instance.SetMaxHealth(maxHealth);

            HealthUI.instance.UpdateHealth(currentHealth);
        }

        selectPopup = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        selectPopup.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SelectButton(0));
        selectPopup.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SelectButton(1));

        // ���� ���� �ƴ϶��, ������ ������ ������ �Ѿ�;� ��.
        if (!SceneManager.GetActiveScene().name.Contains("01"))
        {            
            statPopup = GameObject.Find("Canvas").transform.GetChild(3).gameObject;

            if (HealthUI.instance.isSelectHPUP) statPopup.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "ü�� 50 ����!";
            else statPopup.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "���ݷ� 10 ����!";

            jm.PlayerController player = GameObject.Find("DogPolyart").transform.GetComponent<jm.PlayerController>();
            StartCoroutine(ShowPopup(player));                                            
        }
    }

    public void OpenPopup()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        selectPopup.SetActive(true);
    }

    public void SelectButton(int index)
    {
        if (index == 0)
            SelectHP();
        else if (index == 1)
            SelectAttack();

        // "Stage_02" ������ ��ȯ
        Debug.Log("Selectbutton");
        SceneManager.LoadScene(sceneArr[sceneNum++]);
    }

    public void SelectHP()
    {
        HealthUI.instance.isSelectHPUP = true;
    }

    public void SelectAttack()
    {
        HealthUI.instance.isSelectHPUP = false;
    }

    IEnumerator ShowPopup(jm.PlayerController player)
    {
        player.speed = 0;
        player.rotationSpeed = 0;

        statPopup.SetActive(true);
        yield return new WaitForSeconds(3f);
        statPopup.SetActive(false);

        player.speed = 5;
        player.rotationSpeed = 400;
    }
}