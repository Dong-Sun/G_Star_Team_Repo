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
    public Door Entrance;//입구
    public Door Exit;//출구



    private void Awake()
    {
        SingleTon();
    }

    private void Start()
    {
        Rapping_invoke(0);
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

    public void Rapping_invoke(int time)
    {
        StartCoroutine(invoke_Initialize_GameData(time));
    }

    private IEnumerator invoke_Initialize_GameData(int time)
    {
        yield return new WaitForSeconds(time);
        
        Game_Dir = Dir.ForWard;
        Game_Stop = false;
        Get_Stage_Key = false;
        PlayerManager.Player_Manager_Instance.Can_Move = true;
        Entrance.Open_Door_Aniamtion();
        
    }



   
}
