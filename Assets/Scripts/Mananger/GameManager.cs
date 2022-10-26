using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;
using System;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Game_Manager_Instance;
    [HideInInspector] public Dir Game_Dir;
    [HideInInspector] public bool Game_Stop = false;
    [HideInInspector] public bool Get_Stage_Key = false;
    [HideInInspector] public bool Auto_Moving = false;
    public bool Auto_Moving_Needed = true;
    public ChangeCamera Change_Camera;


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
        Game_Stop = false;
        Get_Stage_Key = false;
        PlayerManager.Player_Manager_Instance.Can_Move = true;
        StartCoroutine(Start_Animation_Coroutine());
        //Entrance.Open_Door_Aniamtion();
    }

    public void Initialize_GameData_Coroutine_Rapping(int time)
    {
        StartCoroutine(Initialize_GameData_Coroutine(time));
    }
    /// <summary>
    /// 딜레이 시간과 쿨타임을 가진 함수 호출용 corotine
    /// </summary>
    /// <param name="Func">여러번 호출할 함수</param>
    /// <param name="delay_time">딜레이 타임</param>
    /// <param name="cool_time">쿨 타임</param>
    public IEnumerator Delay_And_Cool_Func(Action Func, int delay_time, int cool_time)
    {
        yield return new WaitForSeconds(delay_time);

        while (true)
        {
            yield return new WaitForSeconds(cool_time);

            Func();
        }
    }

    private IEnumerator Start_Animation_Coroutine()
    {

        PlayerManager.Player_Manager_Instance.Player_Move.Start_Moving();
        yield return new WaitForSeconds(1);
        Change_Camera.ChangeToMain();
    }


}
