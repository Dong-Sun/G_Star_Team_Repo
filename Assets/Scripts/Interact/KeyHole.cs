using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour,Interact
{
    public GameObject KeyObj;
    [SerializeField] GameObject arrow;
    bool oneTake = true;

    private void Update() {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key && oneTake) {
            arrow.SetActive(true);
            oneTake = false;
        }
    }

    public void Work()
    {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key)
        {
            arrow.SetActive(false);
            KeyObj.SetActive(true);
            StartCoroutine(GameManager.Game_Manager_Instance.End_Animation_Coroutine());
            GameManager.Game_Manager_Instance.Get_Stage_Key = false;
        }
    }
}
