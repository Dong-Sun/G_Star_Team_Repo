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
    public bool Can_Move = false;
    [HideInInspector] public PlayerAnimatorController playeranimatorcontroller;
    [HideInInspector] public bool Player_Die = false;
    [HideInInspector] public bool Holding_Block;
    [HideInInspector] public int input;
    [HideInInspector] public bool in_motion;

    public static PlayerManager Player_Manager_Instance;
    // Start is called before the first frame update
    void Start()
    {
        playeranimatorcontroller = transform.GetChild(0).GetComponent<PlayerAnimatorController>();
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
        playeranimatorcontroller.Player_Animator_Parameter_Control();
        GameManager.Game_Manager_Instance.Rapping_invoke(3);
        SceneLoadManager.scene_load_manager_instance.CurrentSceneLoad();
    }

}