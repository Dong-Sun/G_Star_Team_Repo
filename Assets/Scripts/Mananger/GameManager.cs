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
    /// ������ ����� �Ǵ� ���� ���� �ε� �ɶ� ȣ��Ǵ� �Լ�
    /// </summary>

    public void Initialize_GameData()
    {


        Change_Camera = GameObject.FindObjectOfType<ChangeCamera>();
        Game_Dir = Dir.ForWard;
        Game_Stop = false;
        Get_Stage_Key = false;
        StopAllCoroutines();
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        if (g != null && Player_Manager == null)
        {
            Player_Manager = g.GetComponent<PlayerManager>();
        }
        if (Player_Manager != null && Player_Manager.Auto_Moving_Needed == true)
        {
            Player_Manager.Auto_Moving = true;
        }
        g = GameObject.FindWithTag("Entrance");
        if (g != null)
            g.TryGetComponent<Door>(out Entrance);
        g = GameObject.FindGameObjectWithTag("Exit");
        if (g != null)
            g.TryGetComponent<Door>(out Exit);
        StartCoroutine(Start_Animation_Coroutine());

    }



    /// <summary>
    /// ������ �ð��� ��Ÿ���� ���� �Լ� ȣ��� corotine
    /// </summary>
    /// <param name="Func">������ ȣ���� �Լ�</param>
    /// <param name="delay_time">������ Ÿ��</param>
    /// <param name="cool_time">�� Ÿ��</param>
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
        if (Player_Manager != null)
        {
            Player_Manager.Auto_Moving = false;
        }
        SceneLoadManager.scene_load_manager_instance.SceneChanging = false;
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
