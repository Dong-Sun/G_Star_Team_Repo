using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour,Interact
{
    public GameObject KeyObj;


    public void Work()
    {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key)
        {
            KeyObj.SetActive(true);
            StartCoroutine(GameManager.Game_Manager_Instance.End_Animation_Coroutine());
        }
    }
}
