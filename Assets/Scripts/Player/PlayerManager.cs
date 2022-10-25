using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DataStruct;

[RequireComponent(typeof(PlayerMove))]

public class PlayerManager : MonoBehaviour
{

    [HideInInspector] public float Character_Height = 1;
    [HideInInspector] public int Block_Size = 1;
    [HideInInspector] public float Floor_Height = 0.5f;
    [HideInInspector] public PlayerAnimatorController Player_Animator_Controller;
    [HideInInspector] public bool Player_Die = false;
    [HideInInspector] public bool Can_Move = false;
    [HideInInspector] public bool Holding_Block = false;
    [HideInInspector] public int Input;
    [HideInInspector] public bool In_Motion = false;
    public bool Auto_Moving_Needed = true;

    public static PlayerManager Player_Manager_Instance;
    // Start is called before the first frame update
    void Start()
    {
        Player_Animator_Controller = transform.GetChild(0).GetComponent<PlayerAnimatorController>();
        SingleTon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SingleTon()
    {
        if (Player_Manager_Instance == null)
        {
            Player_Manager_Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Player_Dying()
    {
        Player_Die = true;
        GameManager.Game_Manager_Instance.Game_Stop = true;
        Player_Animator_Controller.Player_Animator_Parameter_Control();
        GameManager.Game_Manager_Instance.Initialize_GameData_Coroutine_Rapping(3);
        SceneLoadManager.scene_load_manager_instance.CurrentSceneLoad();
    }

}