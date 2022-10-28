using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager :MonoBehaviour
{
    public static SceneLoadManager scene_load_manager_instance;
    public Fade Fade_UI_Control;

    [SerializeField] AudioSource testAudio;

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


    public void NextSceneLoad(int time)
    {
        Fade_UI_Control.Fade_in=true;
        if (!(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1))
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1, time));

        }

    }
    public void CurrentSceneLoad(int time)
    {
        Fade_UI_Control.Fade_in = true;
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex,time));
    }

    IEnumerator LoadScene(int buildIndex,float Time)
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(buildIndex);
        GameManager.Game_Manager_Instance.Initialize_GameData();

    }


}
