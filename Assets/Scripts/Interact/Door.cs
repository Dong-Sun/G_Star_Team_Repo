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

            //��ũ��Ʈ �и�
            //ui ����������
            // ���������� �ѱ��
        }
    }

    private void Update()
    {
    }
}
