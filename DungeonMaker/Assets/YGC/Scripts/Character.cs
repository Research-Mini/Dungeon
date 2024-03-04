using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform generator;

    public DialogController dialog;
    public bool isIllGiTo = false;  // 방에서 몬스터와 일기토 하는지 여부

    Transform beforeRoom;           // 일기토 직전에 있었던 방

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(generator.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Collsion Detect");

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
    }

    private void OnTriggerExit(Collider other)
    {
        // if (!other.transform.parent.name.Contains("Room")) return;
        beforeRoom = other.transform.parent;
        Debug.Log(beforeRoom.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isIllGiTo)
        {
            if(other.transform.parent.name == "Floor")
                TryToIllGiTo(other.transform.parent);
            else if(other.transform.parent.parent.name == "Floor")
                TryToIllGiTo(other.transform.parent.parent);
        }
    }

    // 일기토 - 방의 문 다 막기
    public void TryToIllGiTo(Transform trans)
    {
        Debug.Log(trans.parent.name + " / " + trans.parent.tag);
        if (trans.parent.tag != "EnemyRoom") return;        

        // 지나갈 수 없음.
        for (int i = 0; i < trans.parent.GetChild(2).childCount; i++)
        {
            if (!trans.parent.GetChild(2).GetChild(i).name.Contains("Door")) continue;

            if(trans.parent.GetChild(2).GetChild(i).TryGetComponent<BoxCollider>(out BoxCollider box))
            {
                box.enabled = true;
                box.isTrigger = false;
            }

            for(int j=0;j< trans.parent.GetChild(2).GetChild(i).childCount;j++)
            {
                if(trans.parent.GetChild(2).GetChild(i).GetChild(j).TryGetComponent<BoxCollider>(out BoxCollider boxes))
                {
                    boxes.enabled = true;
                    boxes.isTrigger = false;
                }
            }            
        }

        // if (beforeRoom == null) return;

        // if (!beforeRoom.name.Contains("Room")) return;
        if (beforeRoom.parent.childCount > 1)
        {
            for (int i = 0; i < beforeRoom.parent.GetChild(2).childCount; i++)
            {
                if (!beforeRoom.parent.GetChild(2).GetChild(i).name.Contains("Door")) continue;

                if (beforeRoom.parent.GetChild(2).GetChild(i).TryGetComponent<BoxCollider>(out BoxCollider box))
                {
                    box.enabled = true;
                    box.isTrigger = false;
                }

                for (int j = 0; j < beforeRoom.parent.GetChild(2).GetChild(i).childCount; j++)
                {
                    if (beforeRoom.parent.GetChild(2).GetChild(i).GetChild(j).TryGetComponent<BoxCollider>(out BoxCollider boxes))
                    {
                        boxes.enabled = true;
                        boxes.isTrigger = false;
                    }
                }
            }
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