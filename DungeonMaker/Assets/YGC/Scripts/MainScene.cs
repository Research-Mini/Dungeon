using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject HowToPlayPopup;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Start_Game()
    {
        SceneManager.LoadScene(1);
    }

    public void End_Game()
    {
        Application.Quit();
    }

    public void OpenHowToPlay()
    {
        HowToPlayPopup.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        HowToPlayPopup.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
