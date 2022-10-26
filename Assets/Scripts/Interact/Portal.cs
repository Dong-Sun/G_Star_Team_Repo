using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {

                transform.parent.GetChild(2).GetComponent<Door>().Close_Door_Animation();
                GameManager.Game_Manager_Instance.Game_Stop = true;
                GameManager.Game_Manager_Instance.Initialize_GameData_Coroutine_Rapping(3);
                SceneLoadManager.scene_load_manager_instance.NextSceneLoad();
        }
    }

}
