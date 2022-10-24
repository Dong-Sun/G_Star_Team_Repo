using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Game_Manager_Instance.Game_Stop = true;
            GameManager.Game_Manager_Instance.Rapping_invoke(3);
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad();
        }
    }

}
