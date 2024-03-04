using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �߰�

public class SceneChanger : MonoBehaviour
{
    public string Scenename;
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �±װ� "Player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            // ���� �˾� ����� ��.
            Time.timeScale = 0f;

            PlayerStats.instance.OpenPopup();

            // "Stage_02" ������ ��ȯ
            // SceneManager.LoadScene(Scenename);
        }
    }
}