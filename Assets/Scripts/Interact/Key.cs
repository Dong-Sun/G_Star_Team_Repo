using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, Interact {
    bool getKey = false;
    private void Update() {
        transform.Rotate(Vector3.up * 60f * Time.deltaTime);
    }
    public bool GetKey {
        get {
            return getKey;

           
        }
    }

    
    public void Work() {
        if (!GameManager.Game_Manager_Instance.Get_Stage_Key) {
            GameManager.Game_Manager_Instance.Get_Stage_Key = true;
            getKey = true;
            this.gameObject.transform.position += Vector3.down * 2;
            Destroy(this.gameObject, 3);
        }
    }
}
