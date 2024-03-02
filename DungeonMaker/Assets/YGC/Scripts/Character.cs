using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform generator;

    public DialogController dialog;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(generator.transform);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collsion Detect");

        if(collision.gameObject.tag == "NPC")
        {
            // 지나갈 수 있음.
            for(int i=4;i<8;i++)
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

    public void ChangeLayer(Transform trans, string name)
    {
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