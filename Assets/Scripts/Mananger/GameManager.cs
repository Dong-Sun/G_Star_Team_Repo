using DataStruct;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Game_Manager_Instance;
    public PlayerManager Player_Manager;
    public Dir Game_Dir;
    public bool Game_Stop = false;
    public bool Get_Stage_Key = false;
    public ChangeCamera Change_Camera;
    public Door Entrance;
    public Door Exit;


    private void Awake()
    {
        SingleTon();
    }

    private void Start()
    {
        Cursor.visible = false;
        if (SceneLoadManager.scene_load_manager_instance.CurrentSceneIndex() < 2)
        {
            Game_Dir = Dir.ForWard;
            Game_Stop = false;
        }
        else
        {
            Initialize_GameData();  
        }

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
    /// <summary>
    /// 게임이 재시작 또는 다음 씬이 로드 될때 호출되는 함수
    /// </summary>

    public void Initialize_GameData()
    {
        Game_Dir = Dir.ForWard;
        Game_Stop = false;
        Get_Stage_Key = false;
        StopAllCoroutines();
        StartCoroutine(Start_Animation_Coroutine());

    }


    private IEnumerator Start_Animation_Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (Entrance != null)
        {
            Entrance.Open_Door_Aniamtion();
        }
        yield return new WaitForSeconds(0.5f);
        if (Player_Manager != null)
        {
            Player_Manager.Player_Move.Start_Moving();
        }
        yield return new WaitForSeconds(1);
        if (Entrance != null)
        {
            Entrance.Close_Door_Animation();
        }
        if (Change_Camera != null)
            Change_Camera.ChangeToMain();
        yield return new WaitForSeconds(1.5f);
        SceneLoadManager.scene_load_manager_instance.SceneChanging = false;
        yield return new WaitForSeconds(0.25f);
        if (Player_Manager != null)
        {
            Player_Manager.Auto_Moving = false;
        }
    }
    public IEnumerator End_Animation_Coroutine()
    {
        Player_Manager.Auto_Moving = true;
        if (Change_Camera != null)
            Change_Camera.ChangeToEnd();
        yield return new WaitForSeconds(1.5f);
        Exit.Open_Door_Aniamtion();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Player_Manager.Player_Move.End_Moving());

    }
}
