using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Door : MonoBehaviour
{
    Animator DoorAniamtor;
    private void Start()
    {
        DoorAniamtor=GetComponent<Animator>();
        DoorAniamtor.SetInteger("animator_parameter", 0);
    }

    public void Closed_Door_Animation()
    {
        DoorAniamtor.SetInteger("animator_parameter", 0);
    }

    public void Open_Door_Aniamtion()
    {
        DoorAniamtor.SetInteger("animator_parameter", 1);
    }

    public void Close_Door_Animation()
    {
        DoorAniamtor.SetInteger("animator_parameter", 2);
        Invoke("Closed_Door_Animation", 1);
    }

}
