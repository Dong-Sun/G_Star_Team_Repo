using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Game_Manager_Instance;
    [HideInInspector] public Dir Game_Dir;
    [HideInInspector] public bool Game_Stop = false;
    [HideInInspector] public bool Get_Stage_Key = false;



    private void Awake()
    {
        SingleTon();
    }

    private void Start()
    { 
        Initialize_GameData();
    }


    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SingleTon()
    {
        if (Game_Manager_Instance == null)
        {
            Game_Manager_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize_GameData()
    {
        Game_Dir = Dir.ForWard;
        SceneLoadManager.scene_load_manager_instance.SceneStart();
    }

    public void Player_Die()
    {
        
        PlayerManager.Player_Manager_Instance.Player_Die = true;
        PlayerManager.Player_Manager_Instance.playeranimatorcontroller.Player_Animator_Parameter_Control();
        Game_Stop = true;
        SceneLoadManager.scene_load_manager_instance.CurrentSceneLoad();
    }


   
}
