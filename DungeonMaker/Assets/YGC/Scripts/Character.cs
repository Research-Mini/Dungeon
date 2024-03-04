using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform generator;

    public DialogController dialog;
    public bool isIllGiTo = false;  // 방에서 몬스터와 일기토 하는지 여부

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
            // 지나갈 수 있음.
            for (int i = 4; i < 8; i++)
                collision.gameObject.transform.parent.GetChild(2).GetChild(i).GetComponent<BoxCollider>().isTrigger = true;

            // 일단 멈추기 (대화 끝나고 움직이도록)
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

    // 일기토 - 방의 문 다 막기
    public void TryToIllGiTo(Transform trans)
    {
        // 지나갈 수 없음.
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