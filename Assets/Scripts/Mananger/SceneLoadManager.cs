using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager :MonoBehaviour
{
    public static SceneLoadManager scene_load_manager_instance;
    public Fade Fade_UI_Control;

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
        Fade_UI_Control.Fade_in=true;
        if (! (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount-1))
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1,3));
    }
    public void CurrentSceneLoad()
    {
        Fade_UI_Control.Fade_in = true;
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex,3));
    }

    IEnumerator LoadScene(int buildIndex,int Time)
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(buildIndex);

    }
}
