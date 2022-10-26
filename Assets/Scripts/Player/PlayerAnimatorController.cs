using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;

public class PlayerAnimatorController : MonoBehaviour
{

    private Animator Player_animator;
    private Player_Animator_Parameter player_animator_parameter;


    private void Start()
    {
        Player_animator = GetComponent<Animator>();
        player_animator_parameter = Player_Animator_Parameter.Idle;
    }

    private void Set_And_Play_Animation(Player_Animator_Parameter _player_animator_parameter)
    {
        player_animator_parameter = _player_animator_parameter;
        Player_animator.SetInteger("animator",(int)player_animator_parameter);
    }

    public void Player_Animator_Parameter_Control()
    {
        if (PlayerManager.Player_Manager_Instance.Player_Die == true)
        {
            Set_And_Play_Animation(Player_Animator_Parameter.Die);
            return;
        }
        else if (PlayerManager.Player_Manager_Instance.Holding_Block == true)
        {
            if (PlayerManager.Player_Manager_Instance.In_Motion)
            {
                switch (PlayerManager.Player_Manager_Instance.Input)
                {
                    case -1:
                        Set_And_Play_Animation(Player_Animator_Parameter.Pull);
                        return;
                    case 0:

                        return;
                    case 1:
                        Set_And_Play_Animation(Player_Animator_Parameter.Push);
                        return;
                }
            }
            else
            {
                SceneLoadManager.scene_load_manager_instance.NextSceneLoad();
                Set_And_Play_Animation(Player_Animator_Parameter.Block);
                return;
            }
        }
        else if (PlayerManager.Player_Manager_Instance.In_Motion)
            Set_And_Play_Animation(Player_Animator_Parameter.Run);
        else
            Set_And_Play_Animation(Player_Animator_Parameter.Idle);
    }
   
}
