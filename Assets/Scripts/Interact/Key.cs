using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, Interact {
    public void Work() {
        if (!GameManager.Game_Manager_Instance.Get_Stage_Key) {
            GameManager.Game_Manager_Instance.Get_Stage_Key = true;
            gameObject.SetActive(false);
        }
    }
}
