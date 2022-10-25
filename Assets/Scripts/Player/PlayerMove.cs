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
        if (GameManager.Game_Manager_Instance.Game_Stop) //게임이 진행 상태일때
        {
            return;
        }
        if (!GameManager.Game_Manager_Instance.Auto_Moving) //자동 움직임이 되는 중이 아닐때만
        {
            PlayerManager.Player_Manager_Instance.Input = Transform_input(); //좌우 방향키를 변형한 값을 input에 저장
            if (PlayerManager.Player_Manager_Instance.Can_Move) //움직 일수 있는 상태라면(한칸 단위로 잘 이동 되었다면)
            {
                if (PlayerManager.Player_Manager_Instance.Input != 0) //좌우 방향키 입력이 있다면
                {
                    if (!PlayerManager.Player_Manager_Instance.In_Motion) //지금 움직이는 중이 아니라면
                    {
                        Look_Dir.transform.localPosition = PlayerManager.Player_Manager_Instance.Input * Change_Dir_To_Position() + Vector3.down * 0.48f; //게임 진행 방향과 인풋값을 조합하여 보고있는 방향을 바꾼다. (뒤에 down은 캐릭터 매쉬랑 높이 맞추기 위한 값더하기)
                        transform.GetChild(0).LookAt(Look_Dir.transform);
                        if (Is_There_Wall() || Is_There_Cliff())// 절벽이 있는지 벽이 있는지 파악
                        {
                            return;
                        }
                        else
                        {
                            Target_Position += Look_Dir.transform.localPosition;
                            switch (Is_There_Stair()) // -2 내 앞에 계단이 있는데 내려가야된다. -1 내 밑에 계단이 있는데 아래로 움직여야된다. 0 계단이 없다. 1 내 밑에 계단이 있는데 위로 움직여야된다. 2 내 앞에 계단이 있는데 올라가야된다.
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
    private Vector3 Change_Dir_To_Position() //게임 진행 방향에 따른 이동 될 위치 지정 함수
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
    { //계단이 있는지 , 2층에 있어서 움직일 수 없는지 ,높이에 관련된 레이캐스트, 높이 조절함수
        float length = PlayerManager.Player_Manager_Instance.Character_Height * 0.5f;
        if (Razer(transform.position + Look_Dir.transform.localPosition, Vector3.down, length)) // 진행 방향쪽에 계단이 있는지 확인(위로 가느냐)
            return 2;

        length = PlayerManager.Player_Manager_Instance.Block_Size + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f;
        if (Razer(transform.position + Look_Dir.transform.localPosition, Vector3.down, length)) //진행 방향쪽에 계단이 있는지 확인(아래로 가느냐)
            return -2;

        if (Razer(transform.position, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size)) //내 아래로 레이저를 쏜다.(계단이 있는지 파악)
        {
            Physics.Raycast(transform.position + Look_Dir.transform.localPosition, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f); // 앞에 내 발까지만 레이를 쏴서(높이 파악)
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

    int Transform_input() //움직임을 관리하는 벡터의 부호를 관리 하기 위한 함수(또한 키 인풋값을 얻어오기 위한 함수)
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

    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance) //레이를 쐇을때  콜리더가 있는지 없는지 
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

    IEnumerator Fix_Player_Position() // 계단이나 충돌에 의해 좌표가 이상하게 변경되는 것을 방지하기 위한 함수
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) //진행 방향이 앞뒤일때
            {
                Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.forward * (Target_Position.z - this.transform.position.z));
            }
            else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) //진행 방향이 양옆일때
            {
                Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.right * (Target_Position.x - this.transform.position.x));
            }
        }
    }

    /// <summary> 처음 시작할때 움직임(문을 박차고 나간다 던지)등을 위한 함수 </summary>
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

    //교체 내역
    //private void Change_Target_Position(Vector3 Moving_Dir) //이동할 위치 변경 함수
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

