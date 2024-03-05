using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform generator;

    public DialogController dialog;
    public bool isIllGiTo = false;  // 방에서 몬스터와 일기토 하는지 여부
    private List<BoxCollider> blockedEntrances = new List<BoxCollider>(); // 일기토로 인해 막힌 입구들.
    private List<Transform> blockedRooms = new List<Transform>();

    Transform beforeRoom;           // 일기토 직전에 있었던 방

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(generator.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Collsion Detect");

        // Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Finish") return;

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
        if (other.transform.parent.name.Contains("Room"))
            beforeRoom = other.transform.parent;
        else if (other.transform.parent.parent.name.Contains("Room"))
            beforeRoom = other.transform.parent.parent;
        // Debug.Log(beforeRoom.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isIllGiTo)
        {
            // 적은 일기토 방문 닫기에 관여해서는 안 됨.
            if (other.name.Contains("Enemy")) return;

            // Debug.Log(other.transform.parent.name + " / " + other.transform.parent.tag);
            if (other.transform.name.Contains("Room"))
            {                
                TryToIllGiTo(other.transform);
            }
            /*else if (other.transform.name.Contains("Room"))
            {
                // Debug.Log(other.name);
                TryToIllGiTo(other.transform.parent.parent);
            }
            */
        }
    }

    // 일기토 - 방의 문 다 막기
    public void TryToIllGiTo(Transform trans)
    {
        // Debug.Log(trans.name + " / " + trans.tag);
        if (trans.tag != "EnemyRoom") return;
        Debug.Log("illgi");
        // 지나갈 수 없음.
        for (int i = 0; i < trans.GetChild(2).childCount; i++)
        {
            if (!trans.GetChild(2).GetChild(i).name.Contains("Door")) continue;

            blockedRooms.Add(trans);

            if (trans.GetChild(2).GetChild(i).TryGetComponent<BoxCollider>(out BoxCollider box))
            {                
                blockedEntrances.Add(box);
                box.enabled = true;
                box.isTrigger = false;
            }

            for(int j=0;j< trans.GetChild(2).GetChild(i).childCount;j++)
            {
                if(trans.GetChild(2).GetChild(i).GetChild(j).TryGetComponent<BoxCollider>(out BoxCollider boxes))
                {
                    blockedEntrances.Add(boxes);
                    boxes.enabled = true;
                    boxes.isTrigger = false;
                }
            }            
        }

        if (beforeRoom == null) return;
        // Debug.Log(beforeRoom.parent.name + " / " + beforeRoom.parent.tag);
        if (!beforeRoom.parent.name.Contains("Room")) return;
        if (beforeRoom.parent.childCount > 1)
        {
            for (int i = 0; i < beforeRoom.parent.GetChild(2).childCount; i++)
            {
                if (!beforeRoom.parent.GetChild(2).GetChild(i).name.Contains("Door")) continue;

                blockedRooms.Add(beforeRoom.parent);

                if (beforeRoom.parent.GetChild(2).GetChild(i).TryGetComponent<BoxCollider>(out BoxCollider box))
                {                    
                    blockedEntrances.Add(box);
                    box.enabled = true;
                    box.isTrigger = false;
                }

                for (int j = 0; j < beforeRoom.parent.GetChild(2).GetChild(i).childCount; j++)
                {
                    if (beforeRoom.parent.GetChild(2).GetChild(i).GetChild(j).TryGetComponent<BoxCollider>(out BoxCollider boxes))
                    {
                        blockedEntrances.Add(boxes);
                        boxes.enabled = true;
                        boxes.isTrigger = false;
                    }
                }
                
                
            }
        }        
    }

    public void EndOfIllGiTo()
    {
        Debug.Log("EndOfIllGiTo");
        for (int i = 0; i < blockedEntrances.Count; i++)
        {
            blockedEntrances[i].isTrigger = true;
        }

        blockedEntrances.Clear();

        for (int i = 0; i < blockedRooms.Count; i++)
        {
            blockedRooms[i].tag = "Untagged";
        }

        blockedRooms.Clear();
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