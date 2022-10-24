using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Player")
        {
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad();
        }
    }
}
