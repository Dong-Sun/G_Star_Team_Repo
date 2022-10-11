using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Door : MonoBehaviour, Interact 
{
    public void Work()
    {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key)
        {
            this.GetComponent<Animator>().SetBool("Open_The_Door", true);
            Debug.Log("Stage Clear");

            //스크립트 분리
            //ui 검은색으로
            // 다음신으로 넘기기
        }
    }

    private void Update()
    {
    }
}
