using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Portal : MonoBehaviour
{
    bool Portal_Once = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("¹ÙºÎ");
        if (other.CompareTag("Player"))
        {
            if (!Portal_Once)
            {
                transform.parent.GetChild(2).GetComponent<Door>().Close_Door_Animation();
                GameManager.Game_Manager_Instance.Game_Stop = true;
                GameManager.Game_Manager_Instance.Rapping_invoke(3);
                SceneLoadManager.scene_load_manager_instance.NextSceneLoad();
                Portal_Once = true;
            }
        }
    }

}
