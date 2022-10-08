using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interact {
    public void Work() {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key) {
            Debug.Log("Stage Clear");
        }
    }
}
