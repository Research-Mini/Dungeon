using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform generator;

    public DialogController dialog;
    public bool isIllGiTo = false;  // �濡�� ���Ϳ� �ϱ��� �ϴ��� ����

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(generator.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collsion Detect");

        if (collision.gameObject.tag == "NPC")
        {
            // ������ �� ����.
            for (int i = 4; i < 8; i++)
                collision.gameObject.transform.parent.GetChild(2).GetChild(i).GetComponent<BoxCollider>().isTrigger = true;

            // �ϴ� ���߱� (��ȭ ������ �����̵���)
            jm.PlayerController player = transform.GetComponent<jm.PlayerController>();
            player.rotationSpeed = 0;
            player.speed = 0;

            dialog.Start_Dialog(player);
        }
        else
            ChangeLayer(collision.collider.transform.parent.parent, "MinimapVisible");

        if (isIllGiTo)
        {
            if (collision.collider.transform.parent.name == "Entrances")
                TryToIllGiTo(collision.collider.transform.parent);
        }
    }

    // �ϱ��� - ���� �� �� ����
    public void TryToIllGiTo(Transform trans)
    {
        // ������ �� ����.
        for (int i = 4; i < 8; i++)
        {
            trans.GetChild(2).GetChild(i).GetChild(0).GetComponent<BoxCollider>().enabled = true;
            trans.GetChild(2).GetChild(i).GetChild(0).GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    public void ChangeLayer(Transform trans, string name)
    {
        if (trans.parent != generator)
            trans = trans.parent;
        ChangeLayersRecursively(trans, name);
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}