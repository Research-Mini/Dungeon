using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class DialogController : MonoBehaviour
{
    public TMP_Text text;

    public List<string> npc_text;

    private int dialogNum = 0;
    private bool isDialogStarted = false;
    private jm.PlayerController playerController;
    private Tweener talkTween;
    private GameObject dialog_Ani;

    public void End_Dialog()
    {
        playerController.speed = 5;
        playerController.rotationSpeed = 400;
        text.transform.parent.gameObject.SetActive(false);

        isDialogStarted = false;
    }

    public void Next_Dialog()
    {
        if (dialogNum == npc_text.Count)
        {
            End_Dialog();
            return;
        }

        dialog_Ani.SetActive(false);

        // 넘어가는 효과음 필요

        text.text = npc_text[dialogNum++];

        talkTween = DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, 2f)
            .OnComplete(() =>
            {
                dialog_Ani.SetActive(true);
                //Debug.Log("Complete Dialog");
            });
    }

    public void Start_Dialog(jm.PlayerController player)
    {
        playerController = player;
        text.transform.parent.gameObject.SetActive(true);
        isDialogStarted = true;
        dialog_Ani = text.transform.parent.GetChild(1).gameObject;

        Next_Dialog();
    }

    public void Update()
    {
        if(isDialogStarted)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Next_Dialog();
            }
        }
    }
}
