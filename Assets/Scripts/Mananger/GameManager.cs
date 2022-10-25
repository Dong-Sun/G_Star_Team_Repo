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
    [HideInInspector] public bool Auto_Moving = false;



    private void Awake()
    {
        SingleTon();
    }

    private void Start()
    {
        Initialize_GameData_Coroutine_Rapping(0);
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


    private IEnumerator Initialize_GameData_Coroutine(int time)
    {
        yield return new WaitForSeconds(time);
        
        Game_Dir = Dir.ForWard;
        Game_Stop = true;
        Get_Stage_Key = false;
        PlayerManager.Player_Manager_Instance.Can_Move = true;
        //Entrance.Open_Door_Aniamtion();
    }

    public void Initialize_GameData_Coroutine_Rapping(int time)
    {
        StartCoroutine(Initialize_GameData_Coroutine(time));
    }


}
