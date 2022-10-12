using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager :MonoBehaviour
{
    public static SceneLoadManager scene_load_manager_instance;

    private void Start()
    {
        if(scene_load_manager_instance == null)
        {
            scene_load_manager_instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void NextSceneLoad()
    {
        if(! (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount-1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PriviousSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
