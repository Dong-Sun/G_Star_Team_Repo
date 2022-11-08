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
     public bool Player_Die = false;
     public bool Can_Move = false; //��ĭ �������� �� �Ǿ�� Can_Move�� Ȱ���Ǹ鼭 ���� �������� ����
     public bool Holding_Block = false;
     public int Input;
     public bool In_Motion = false; //�÷��̾� �ִϸ��̼��� Run�� Idle�� �������� ���� ����
     public bool Fixed_Position_Control_Bool = false;
     public bool Auto_Moving = false;
    public bool Auto_Moving_Needed = true;

  // Start is called before the first frame update
    private void Awake()
    {
        SingleTon();
    }

    void Start()
    {
        GameManager.Game_Manager_Instance.Player_Manager = this;
        Player_Animator_Controller = transform.GetChild(0).GetComponent<PlayerAnimatorController>();
        Player_Move = GetComponent<PlayerMove>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SingleTon()
    {
    }

    public void Player_Dying()
    {
        Player_Die = true;
        GameManager.Game_Manager_Instance.Game_Stop = true;
        Player_Animator_Controller.Player_Animator_Parameter_Control();
        SceneLoadManager.scene_load_manager_instance.CurrentSceneLoad(2);
    }

}