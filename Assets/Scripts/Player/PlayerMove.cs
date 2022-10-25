using DataStruct;
using UnityEngine;
using System.Collections;




public class PlayerMove : MonoBehaviour
{



    public Vector3 Target_Position;
    private GameObject Look_Dir;
    private CharacterController Player_Character_Controller;
    private bool Running_Coroutine = false;
    private int Moving_Speed = 3;
    static RaycastHit hit;


    private void Start()
    {
        Look_Dir = transform.GetChild(1).gameObject;
        Target_Position = this.transform.position;
        Player_Character_Controller = GetComponent<CharacterController>();
        Invoke("Start_Moving", 2f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Game_Manager_Instance.Game_Stop) //������ ���� �����϶�
        {
            return;
        }
        if (!GameManager.Game_Manager_Instance.Auto_Moving) //�ڵ� �������� �Ǵ� ���� �ƴҶ���
        {
            PlayerManager.Player_Manager_Instance.Input = Transform_input(); //�¿� ����Ű�� ������ ���� input�� ����
            if (PlayerManager.Player_Manager_Instance.Can_Move) //���� �ϼ� �ִ� ���¶��(��ĭ ������ �� �̵� �Ǿ��ٸ�)
            {
                if (PlayerManager.Player_Manager_Instance.Input != 0) //�¿� ����Ű �Է��� �ִٸ�
                {
                    if (!PlayerManager.Player_Manager_Instance.In_Motion) //���� �����̴� ���� �ƴ϶��
                    {
                        Look_Dir.transform.localPosition = PlayerManager.Player_Manager_Instance.Input * Change_Dir_To_Position() + Vector3.down * 0.48f; //���� ���� ����� ��ǲ���� �����Ͽ� �����ִ� ������ �ٲ۴�. (�ڿ� down�� ĳ���� �Ž��� ���� ���߱� ���� �����ϱ�)
                        transform.GetChild(0).LookAt(Look_Dir.transform);
                        if (Is_There_Wall() || Is_There_Cliff())// ������ �ִ��� ���� �ִ��� �ľ�
                        {
                            return;
                        }
                        else
                        {
                            Target_Position += Look_Dir.transform.localPosition;
                            switch (Is_There_Stair()) // -2 �� �տ� ����� �ִµ� �������ߵȴ�. -1 �� �ؿ� ����� �ִµ� �Ʒ��� �������ߵȴ�. 0 ����� ����. 1 �� �ؿ� ����� �ִµ� ���� �������ߵȴ�. 2 �� �տ� ����� �ִµ� �ö󰡾ߵȴ�.
                            {
                                case -2:
                                    Target_Position -= Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.3f;
                                    break;
                                case -1:
                                    Target_Position -= Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.7f;
                                    break;
                                case 0:
                                    break;
                                case 1:
                                    Target_Position += Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.7f;
                                    break;
                                case 2:
                                    Target_Position += Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.3f;
                                    break;
                            }
                        }
                    }
                    else //
                    {
                        PlayerManager.Player_Manager_Instance.Can_Move = false;
                    }
                }//
                else
                {
                    if (!PlayerManager.Player_Manager_Instance.In_Motion)
                        PlayerManager.Player_Manager_Instance.Can_Move = true;
                    else
                        PlayerManager.Player_Manager_Instance.Can_Move = false;
                }
            }
            else
            {
                PlayerManager.Player_Manager_Instance.In_Motion = true;
            }
        }
        if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f)
        {
            Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y);
            Enabling_Player_Character_Controller_To_Fix_Player_Position(Target_Position);
            if (PlayerManager.Player_Manager_Instance.Input == 0)
            {
                PlayerManager.Player_Manager_Instance.In_Motion = false;
                PlayerManager.Player_Manager_Instance.Player_Animator_Controller.Player_Animator_Parameter_Control();
            }
        }
        else
        {
            Player_Character_Controller.Move((Target_Position - this.transform.position).normalized * Time.deltaTime * Moving_Speed);
            PlayerManager.Player_Manager_Instance.In_Motion = true;
            PlayerManager.Player_Manager_Instance.Player_Animator_Controller.Player_Animator_Parameter_Control();
        }
        Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 0.8f);
    }
    private Vector3 Change_Dir_To_Position() //���� ���� ���⿡ ���� �̵� �� ��ġ ���� �Լ�
    {
        if (PlayerManager.Player_Manager_Instance.Can_Move == true)
        {
            switch (GameManager.Game_Manager_Instance.Game_Dir)
            {
                case Dir.ForWard:
                    return Vector3.right;
                case Dir.BackWard:
                    return Vector3.left;
                case Dir.Right:
                    return Vector3.forward;
                case Dir.Left:
                    return Vector3.back;
                default:
                    return Vector3.zero;
            }
        }
        else
            return Vector3.zero;
    }

    private int Is_There_Stair()
    { //����� �ִ��� , 2���� �־ ������ �� ������ ,���̿� ���õ� ����ĳ��Ʈ, ���� �����Լ�
        float length = PlayerManager.Player_Manager_Instance.Character_Height * 0.5f;
        if (Razer(transform.position + Look_Dir.transform.localPosition, Vector3.down, length)) // ���� �����ʿ� ����� �ִ��� Ȯ��(���� ������)
            return 2;

        length = PlayerManager.Player_Manager_Instance.Block_Size + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f;
        if (Razer(transform.position + Look_Dir.transform.localPosition, Vector3.down, length)) //���� �����ʿ� ����� �ִ��� Ȯ��(�Ʒ��� ������)
            return -2;

        if (Razer(transform.position, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size)) //�� �Ʒ��� �������� ���.(����� �ִ��� �ľ�)
        {
            Physics.Raycast(transform.position + Look_Dir.transform.localPosition, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f); // �տ� �� �߱����� ���̸� ����(���� �ľ�)
            if (hit.collider != null)
                return 1;
            else
                return -1;
        }
        return 0;
    }

    private bool Is_There_Wall()
    {
        if (PlayerManager.Player_Manager_Instance.Holding_Block)
            return Physics.Raycast(this.transform.position + Vector3.down * 0.4f + Look_Dir.transform.localPosition, Look_Dir.transform.localPosition, PlayerManager.Player_Manager_Instance.Block_Size, 3 | 2);
        else
            return Physics.Raycast(this.transform.position + Vector3.down * 0.4f, Look_Dir.transform.localPosition, PlayerManager.Player_Manager_Instance.Block_Size, 3 | 2);
    }

    private bool Is_There_Cliff()
    {
        return !Physics.Raycast(this.transform.position + Look_Dir.transform.localPosition, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + 0.3f * PlayerManager.Player_Manager_Instance.Block_Size);
    }

    int Transform_input() //�������� �����ϴ� ������ ��ȣ�� ���� �ϱ� ���� �Լ�(���� Ű ��ǲ���� ������ ���� �Լ�)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            return 1;
        else
            return 0;
    }

    private void Enabling_Player_Character_Controller_To_Fix_Player_Position(Vector3 vec)
    {
        Player_Character_Controller.enabled = false;
        this.transform.position = vec;
        Player_Character_Controller.enabled = true;
    }

    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance) //���̸� �i����  �ݸ����� �ִ��� ������ 
    {
        Physics.Raycast(origin, TargetVec, out hit, distance);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Stair")
            {
                return true;
            }

        }
        return false;
    }

    IEnumerator Fix_Player_Position() // ����̳� �浹�� ���� ��ǥ�� �̻��ϰ� ����Ǵ� ���� �����ϱ� ���� �Լ�
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) //���� ������ �յ��϶�
            {
                Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.forward * (Target_Position.z - this.transform.position.z));
            }
            else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) //���� ������ �翷�϶�
            {
                Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.right * (Target_Position.x - this.transform.position.x));
            }
        }
    }

    /// <summary> ó�� �����Ҷ� ������(���� ������ ������ ����)���� ���� �Լ� </summary>
    void Start_Moving()
    { 
        if (PlayerManager.Player_Manager_Instance.Auto_Moving_Needed == true)
        {
            Target_Position += Look_Dir.transform.localPosition.normalized * 2;
            PlayerManager.Player_Manager_Instance.In_Motion = false;
            GameManager.Game_Manager_Instance.Auto_Moving = false;
            StartCoroutine(Fix_Player_Position());
        }
    }

    //��ü ����
    //private void Change_Target_Position(Vector3 Moving_Dir) //�̵��� ��ġ ���� �Լ�
    //{
    //    if (PlayerManager.Player_Manager_Instance.Can_Move == true)
    //    {
    //        {
    //            Target_Position += PlayerManager.Player_Manager_Instance.input * Moving_Dir;
    //            PlayerManager.Player_Manager_Instance.Can_Move = false;
    //        }
    //            else
    //        {
    //            PlayerManager.Player_Manager_Instance.in_motion = false;
    //            PlayerManager.Player_Manager_Instance.Player_Animator_Controller.Player_Animator_Parameter_Control();
    //        }
    //    }

    //}
}

