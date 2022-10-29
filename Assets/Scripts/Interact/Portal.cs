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
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad(2);
            GameManager.Game_Manager_Instance.Exit.Close_Door_Animation();
        }
    }

}
