using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DataStruct;

[RequireComponent(typeof(PlayerMove))]

public class PlayerManager : MonoBehaviour
{

    public float Character_Height = 1;
    public int Block_Size = 1;
    public float Floor_Height = 0.5f;
    public PlayerAnimatorController Player_Animator_Controller;
    public PlayerMove Player_Move;
    public PlayerInteraction Player_Interation;
    public bool Player_Die = false;
    public bool Can_Move = false; //한칸 움직임이 다 되어야 Can_Move가 활성되면서 다음 움직임을 구현
    public bool Holding_Block = false;
    public int Input;
    public bool In_Motion = false; //플레이어 애니메이션중 Run과 Idle을 구분짓기 위한 변수
    public bool Fixed_Position_Control_Bool = false;
    public bool Auto_Moving = false;
    public bool Auto_Moving_Needed = true;

    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {
        GameManager.Game_Manager_Instance.Player_Manager = this;
        if (GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving_Needed == true)
        {
            GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving = true;
        }
        Player_Animator_Controller = transform.GetChild(0).GetComponent<PlayerAnimatorController>();
        Player_Move = GetComponent<PlayerMove>();
        transform.GetChild(1).TryGetComponent<PlayerInteraction>(out Player_Interation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Player_Dying()
    {
        Player_Die = true;
        AudioManager.instance.OneShotEvent("playerDie");
        GameManager.Game_Manager_Instance.Game_Stop = true;
        Player_Animator_Controller.Player_Animator_Parameter_Control();
        SceneLoadManager.scene_load_manager_instance.CurrentSceneLoad(2);
    }

}