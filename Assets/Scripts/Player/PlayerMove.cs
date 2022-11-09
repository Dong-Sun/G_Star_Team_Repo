using DataStruct;
using UnityEngine;
using System.Collections;
using System;

public class PlayerMove : MonoBehaviour
{

    private Vector3 Target_Position;
    public Transform Look_Dir;
    private CharacterController Player_Character_Controller;
    private int Moving_Speed = 2;
    static RaycastHit hit;
    Coroutine Fix_Player_Position_Coroutine;
    public Vector3 Moving_Dir;


    private void Start()
    {
        Look_Dir = transform.GetChild(1).gameObject.transform;
        Target_Position = this.transform.position;
        Player_Character_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Game_Manager_Instance.Game_Stop || GameManager.Game_Manager_Instance.Player_Manager.Player_Die || Time.timeScale == 0) //������ ������ �ǰ� ���� �ʴٸ� �Ǵ� �÷��̾ �׾��ٸ�
        {
            return;
        }
        if (!GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving) //�ڵ� �������� �Ǵ� ���� �ƴҶ���
        {
            //if (GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool) //�浹�̳� ��ܵ ���� �¿� �̵��� �ƴ� �� �̵��� �߻��ϴ� ��찡 �־� �װ��� �����ϴ� �ڷ�ƾ�� ������ ȣ��Ǵ� ���� ���� �ϱ� ����
            //{
            //    Fix_Player_Position_Coroutine = StartCoroutine(GameManager.Game_Manager_Instance.Delay_And_Cool_Func(Fix_Player_Position_To_Dir, 0, 1));
            //    GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool = false;

            //}

            GameManager.Game_Manager_Instance.Player_Manager.Input = Transform_input(); //�¿� ����Ű�� ������ ���� input�� ����
            if (GameManager.Game_Manager_Instance.Player_Manager.Can_Move) //���� �ϼ� �ִ� ���¶��(��ĭ ������ �� �ִٸ�)
            {
                if (GameManager.Game_Manager_Instance.Player_Manager.Input != 0) //�����¿� ����Ű �Է��� �ִٸ�
                {
                    Moving_Dir = Change_Dir_To_Position(GameManager.Game_Manager_Instance.Player_Manager.Input);
                    if (!GameManager.Game_Manager_Instance.Player_Manager.Holding_Block)
                    {
                        Look_Dir.localPosition = Change_Dir_To_Position(GameManager.Game_Manager_Instance.Player_Manager.Input); //���� ���� ����� ��ǲ���� �����Ͽ� �����ִ� ������ �ٲ۴�. (�ڿ� down�� ĳ���� �Ž��� ���� ���߱� ���� �����ϱ�)
                    }
                    else
                    {
                        if (!(Moving_Dir == -Look_Dir.localPosition || Moving_Dir == Look_Dir.localPosition))
                            return;
                    }
                    if (Is_There_Wall() || Is_There_Cliff())
                    {
                        Audio_Control();
                        GameManager.Game_Manager_Instance.Player_Manager.In_Motion = false;
                        GameManager.Game_Manager_Instance.Player_Manager.Player_Animator_Controller.Player_Animator_Parameter_Control();
                        return;
                    }
                    else
                    {
                        Target_Position += Moving_Dir;
                        switch (Is_There_Stair())
                        {
                            case -3:
                                Target_Position -= Vector3.up * GameManager.Game_Manager_Instance.Player_Manager.Floor_Height * 1;
                                break;
                            case -2:
                                Target_Position -= Vector3.up * GameManager.Game_Manager_Instance.Player_Manager.Floor_Height * 0.5f;
                                break;
                            case -1:
                                Target_Position -= Vector3.up * GameManager.Game_Manager_Instance.Player_Manager.Floor_Height * 0.5f;
                                break;
                            case 0:
                                break;
                            case 1:
                                Target_Position += Vector3.up * GameManager.Game_Manager_Instance.Player_Manager.Floor_Height * 0.5f;
                                break;
                            case 2:
                                Target_Position += Vector3.up * GameManager.Game_Manager_Instance.Player_Manager.Floor_Height * 0.5f;
                                break;
                            case 3:
                                Target_Position += Vector3.up * GameManager.Game_Manager_Instance.Player_Manager.Floor_Height * 1;
                                break;
                        }
                    }
                }
            }

        }//�ڵ� �������� ���ƴ� ����
        else
        {
            if (!GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool)
            {
                //if (Fix_Player_Position_Coroutine != null)
                //{
                //    StopCoroutine(Fix_Player_Position_Coroutine);//�¿� ������ �ƴ� �������� �̵��Ǵ� ���� �����ϴ� �ڷ�ƾ�� ����.(�ڵ��̵��ÿ� ���� �������� �̵��ϴ� ��찡 �־)
                //    GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool = true;
                //}
            }
        }
        transform.GetChild(0).LookAt(Look_Dir.position + Vector3.down * 0.48f);
        Real_Player_Moving();
    }

    /// <summary>
    /// ���� ���� ���⿡ ���� �̵� �� ��ġ ���� �Լ�
    /// </summary>
    /// <returns></returns>
    private Vector3 Change_Dir_To_Position(int input)
    {
        int sum = (int)GameManager.Game_Manager_Instance.Game_Dir + input;
        switch (sum % 4) //ī�޶� ���� ����(ī�޶󿡼� ����)
        {
            case 1:
                return Vector3.back;
            case 2:
                return Vector3.left;
            case 3:
                return Vector3.forward;
            case 0:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }

    /// <summary>
    /// �÷��̾�� �ʿ��� �Է°�(�ִϸ��̼� �Ǵ� ���� ���� ���� �����ϱ� ����)�� �����ϱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    int Transform_input() //�������� �����ϴ� ������ ��ȣ�� ���� �ϱ� ���� �Լ�(���� Ű ��ǲ���� ������ ���� �Լ�)
    {
        if (Input.GetKey(KeyCode.DownArrow))
            return 1;
        else if (Input.GetKey(KeyCode.UpArrow))
            return 3;
        else if (Input.GetKey(KeyCode.LeftArrow))
            return 2;

        else if (Input.GetKey(KeyCode.RightArrow))
            return 4;
        else
            return 0;
    }




    /// <summary>
    /// ����� ������ ��ǥ�� ���� ���� ��ȯ -2 �� �տ� ����� �ִµ� �������ߵȴ�. -1 �� �ؿ� ����� �ִµ� �Ʒ��� �������ߵȴ�. 0 ����� ����. 1 �� �ؿ� ����� �ִµ� ���� �������ߵȴ�. 2 �� �տ� ����� �ִµ� �ö󰡾ߵȴ�.
    /// </summary>
    /// <returns></returns>
    private int Is_There_Stair()
    { //����� �ִ��� , 2���� �־ ������ �� ������ ,���̿� ���õ� ����ĳ��Ʈ, ���� �����Լ�
        float length = GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f;
        if (Razer(transform.position + Moving_Dir, Vector3.down, length)) // ���� �����ʿ� ����� �ִ��� Ȯ��(���� ������)
        {
            if (Razer(transform.position, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Block_Size)) // �� �ؿ� ����� �ִ��� Ȯ��
                return 3;
            else
                return 2;
        }


        length = GameManager.Game_Manager_Instance.Player_Manager.Block_Size + GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f;
        if (Razer(transform.position + Moving_Dir, Vector3.down, length)) //���� �����ʿ� ����� �ִ��� Ȯ��(�Ʒ��� ������)
        {
            if (Razer(transform.position, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Block_Size)) // �� �ؿ��� ����� �ִ��� Ȯ��
                return -3;
            else
                return -2;
        }

        if (Razer(transform.position, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Block_Size)) //�� �Ʒ��� �������� ���.(����� �ִ��� �ľ�)
        {
            Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f); // �տ� �� �߱����� ���̸� ����(���� �ľ�)
            if (hit.collider != null)
                return 1;
            else
                return -1;
        }

        return 0;
    }




    /// <summary>
    /// ���� �ִ��� Ȯ�� �ϱ� ���� �Լ�
    /// </summary>
    /// <returns> ���� ����������� ���� ������ ���ߴ� ������ �ٸ��⿡ return�� �����ϱ� ���� raycast�� �ٸ���.</returns>
    //�� 3���� ��� ���� ��������� ������ ���� ��������� �ڷδ� ��ĭ �����δ� ��ĭ
    private bool Is_There_Wall()
    {
        
        if (!GameManager.Game_Manager_Instance.Player_Manager.Holding_Block)
            return Physics.Raycast(this.transform.position + Vector3.down * 0.3f, Moving_Dir, GameManager.Game_Manager_Instance.Player_Manager.Block_Size, 2 | 3 | 6);
        else if (Moving_Dir == Look_Dir.localPosition)
            return Physics.Raycast(this.transform.position + Vector3.down * 0.4f + Moving_Dir, Look_Dir.localPosition, GameManager.Game_Manager_Instance.Player_Manager.Block_Size, 2 | 3 );
        else 
            return Physics.Raycast(this.transform.position + Vector3.down * 0.4f, Moving_Dir, GameManager.Game_Manager_Instance.Player_Manager.Block_Size, 2 | 3);
    }




    /// <summary>
    /// ���� �̵� ������ �÷��̾�� �������� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    private bool Is_There_Cliff()
    {
        return !Physics.Raycast(this.transform.position + Moving_Dir, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f + 0.6f * GameManager.Game_Manager_Instance.Player_Manager.Block_Size);
    }


    /// <summary>
    /// ����� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="origin">��� ����</param>
    /// <param name="TargetVec"> ��� ���� </param>
    /// <param name="distance">��� �Ÿ�</param>
    /// <returns></returns>
    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance)
    {
        Physics.Raycast(origin, TargetVec, out hit, distance); //2��3 ���̾� ����ũ�� �����Ѵ�(��Ż �� �־ ���°�),interact�� ���� �ݶ��̴�(Look Dir,����ִ� ����)
        if (hit.collider != null)
        {
            
            if (hit.collider.tag == "Stair")
            {
                return true;
            }

        }
        return false;
    }





    /// <summary>
    /// ���� �������� �����ϴ� �Լ�(Character Controller �������� ����
    /// </summary>
    private void Real_Player_Moving()
    {
        if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f)
        {
            Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y); //Target��ǥ ���̸� ���� ���̷� �̵���Ų��.(�������� ������ �ñ�ų� ���� �̵��ҷ��� �߾� �ϴ� ��� �߻�)
            Enabling_Player_Character_Controller_To_Fix_Player_Position(Target_Position); //x,z��ǥ�� Target�������� �� �����ϴ� �Լ�(Controller.Move �Լ��� �Ϻ��� ��ĭ �̵��� �������� �ʱ⿡ �̷��� ��ǥ ����)
            GameManager.Game_Manager_Instance.Player_Manager.Can_Move = true; //����ĭ���� �̵��Ҽ� �ְ� ����
            if (GameManager.Game_Manager_Instance.Player_Manager.Input == 0) //���� ĭ���� �ٷ� �̵��ϴ� ��� Idle�� ������� �ʰ� Run ���¸� ����
            {
                Audio_Control();
                GameManager.Game_Manager_Instance.Player_Manager.In_Motion = false;
                GameManager.Game_Manager_Instance.Player_Manager.Player_Animator_Controller.Player_Animator_Parameter_Control();

            }
        }
        else
        {
            Audio_Control();
            Player_Character_Controller.Move((Target_Position - this.transform.position).normalized * Time.deltaTime * Moving_Speed);
            GameManager.Game_Manager_Instance.Player_Manager.Can_Move = false;
            GameManager.Game_Manager_Instance.Player_Manager.In_Motion = true;
            GameManager.Game_Manager_Instance.Player_Manager.Player_Animator_Controller.Player_Animator_Parameter_Control();

        }
        Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 1f);
    }





    /// <summary>
    /// Character Controller�� ���� �̵��ǰ� �¿� ��ǥ�� ���̸� �����ϱ� ���� �̵��ô� �Լ�
    /// </summary>
    /// <param name="vec">���ϴ� ������ ��ǥ</param>
    private void Enabling_Player_Character_Controller_To_Fix_Player_Position(Vector3 vec)
    {
        Player_Character_Controller.enabled = false;
        this.transform.position = vec;
        Player_Character_Controller.enabled = true;
    }




    /// <summary>
    /// ����̳� �浹�� ���� �̵��ÿ� ��ǥ�� �̻��ϰ� ����Ǵ� ���� �����ϱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    void Fix_Player_Position_To_Dir()
    {
        if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) //���� ������ �յ��϶�
        {
            Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.forward * (Target_Position.z - this.transform.position.z));
        }
        else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) //���� ������ �翷�϶�
        {
            Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.right * (Target_Position.x - this.transform.position.x));
        }
    }




    /// <summary> ó�� �����Ҷ� ������(���� ������ ������ ����)���� ���� �Լ� </summary>
    public void Start_Moving()
    {
        if (GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving_Needed == true)
        {
            Target_Position += (Look_Dir.localPosition).normalized * 2; // ������ �����ѵ� ĳ���͸� 0,0�� �θ� �������°Ͷ����� lookdir�� ��ǥ�� ������...

            //Fix_Player_Position_Coroutine = StartCoroutine(GameManager.Game_Manager_Instance.Delay_And_Cool_Func(Fix_Player_Position_To_Dir, 0, 3));

        }
    }

    public IEnumerator End_Moving()
    {
        Look_Dir.localPosition = Quaternion.Euler(0, -90, 0) * Look_Dir.localPosition;
        Target_Position += Look_Dir.localPosition;
        yield return new WaitForSeconds(0.5f);
        Look_Dir.localPosition = Quaternion.Euler(0, 90, 0) * Look_Dir.localPosition;
        Target_Position += Look_Dir.localPosition * 2;
        yield return new WaitForSeconds(1f);
        GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving = false;
    }
    private void Audio_Control()
    {
        if (GameManager.Game_Manager_Instance.Player_Manager.In_Motion == true && GameManager.Game_Manager_Instance.Player_Manager.Can_Move == true)
        {
            if (GameManager.Game_Manager_Instance.Player_Manager.Holding_Block)
                AudioManager.instance.OneShotEvent("dragRock");
            else
                AudioManager.instance.OneShotEvent("walk");
        }
    }
}