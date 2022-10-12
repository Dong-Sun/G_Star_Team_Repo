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

    public void SetAndPlayAnimation(Player_Animator_Parameter _player_animator_parameter)
    {
        player_animator_parameter = _player_animator_parameter;
        Player_animator.SetInteger("animator",(int)player_animator_parameter);
    }
        
   
}
