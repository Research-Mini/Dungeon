using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �߰�

public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �±װ� "Player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            // "Stage_02" ������ ��ȯ
            SceneManager.LoadScene("Stage_02");
        }
    }
}