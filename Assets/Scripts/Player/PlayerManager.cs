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
    [HideInInspector] public bool Can_Move = false;
    [HideInInspector] public PlayerAnimatorController playeranimatorcontroller;
    [HideInInspector] public bool Player_Die = false;


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

}