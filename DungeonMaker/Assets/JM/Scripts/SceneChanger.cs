using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class SceneChanger : MonoBehaviour
{
    public string Scenename;
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // "Stage_02" 씬으로 전환
            SceneManager.LoadScene(Scenename);
        }
    }
}