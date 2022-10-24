using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour,Interact
{
    public Door door;
    public GameObject KeyObj;

    public void Work()
    {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key)
        {
            door.Open_Door_Aniamtion();
            KeyObj.SetActive(true);
        }
    }
}
