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
        if (GameManager.Game_Manager_Instance.Game_Stop || GameManager.Game_Manager_Instance.Player_Manager.Player_Die || Time.timeScale == 0) //게임이 진행이 되고 있지 않다면 또는 플레이어가 죽었다면
        {
            return;
        }
        if (!GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving) //자동 움직임이 되는 중이 아닐때만
        {
            //if (GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool) //충돌이나 계단등에 의해 좌우 이동이 아닌 앞 이동이 발생하는 경우가 있어 그것을 제한하는 코루틴이 여러번 호출되는 것을 방지 하기 위함
            //{
            //    Fix_Player_Position_Coroutine = StartCoroutine(GameManager.Game_Manager_Instance.Delay_And_Cool_Func(Fix_Player_Position_To_Dir, 0, 1));
            //    GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool = false;

            //}

            GameManager.Game_Manager_Instance.Player_Manager.Input = Transform_input(); //좌우 방향키를 변형한 값을 input에 저장
            if (GameManager.Game_Manager_Instance.Player_Manager.Can_Move) //움직 일수 있는 상태라면(한칸 단위에 잘 있다면)
            {
                if (GameManager.Game_Manager_Instance.Player_Manager.Input != 0) //상하좌우 방향키 입력이 있다면
                {
                    Moving_Dir = Change_Dir_To_Position(GameManager.Game_Manager_Instance.Player_Manager.Input);
                    if (!GameManager.Game_Manager_Instance.Player_Manager.Holding_Block)
                    {
                        Look_Dir.localPosition = Change_Dir_To_Position(GameManager.Game_Manager_Instance.Player_Manager.Input); //게임 진행 방향과 인풋값을 조합하여 보고있는 방향을 바꾼다. (뒤에 down은 캐릭터 매쉬랑 높이 맞추기 위한 값더하기)
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

        }//자동 움직임이 돌아는 동안
        else
        {
            if (!GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool)
            {
                //if (Fix_Player_Position_Coroutine != null)
                //{
                //    StopCoroutine(Fix_Player_Position_Coroutine);//좌우 방향이 아닌 방향으로 이동되는 것을 제한하는 코루틴을 끈다.(자동이동시에 여러 방향으로 이동하는 경우가 있어서)
                //    GameManager.Game_Manager_Instance.Player_Manager.Fixed_Position_Control_Bool = true;
                //}
            }
        }
        transform.GetChild(0).LookAt(Look_Dir.position + Vector3.down * 0.48f);
        Real_Player_Moving();
    }

    /// <summary>
    /// 게임 진행 방향에 따른 이동 될 위치 변경 함수
    /// </summary>
    /// <returns></returns>
    private Vector3 Change_Dir_To_Position(int input)
    {
        int sum = (int)GameManager.Game_Manager_Instance.Game_Dir + input;
        switch (sum % 4) //카메라가 보는 방향(카메라에서 변경)
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
    /// 플레이어에게 필요한 입력값(애니메이션 또는 보는 방향 등을 설정하기 위한)을 변형하기 위한 함수
    /// </summary>
    /// <returns></returns>
    int Transform_input() //움직임을 관리하는 벡터의 부호를 관리 하기 위한 함수(또한 키 인풋값을 얻어오기 위한 함수)
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
    /// 계단이 있을때 좌표에 따른 값을 반환 -2 내 앞에 계단이 있는데 내려가야된다. -1 내 밑에 계단이 있는데 아래로 움직여야된다. 0 계단이 없다. 1 내 밑에 계단이 있는데 위로 움직여야된다. 2 내 앞에 계단이 있는데 올라가야된다.
    /// </summary>
    /// <returns></returns>
    private int Is_There_Stair()
    { //계단이 있는지 , 2층에 있어서 움직일 수 없는지 ,높이에 관련된 레이캐스트, 높이 조절함수
        float length = GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f;
        if (Razer(transform.position + Moving_Dir, Vector3.down, length)) // 진행 방향쪽에 계단이 있는지 확인(위로 가느냐)
        {
            if (Razer(transform.position, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Block_Size)) // 내 밑에 계단이 있는지 확인
                return 3;
            else
                return 2;
        }


        length = GameManager.Game_Manager_Instance.Player_Manager.Block_Size + GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f;
        if (Razer(transform.position + Moving_Dir, Vector3.down, length)) //진행 방향쪽에 계단이 있는지 확인(아래로 가느냐)
        {
            if (Razer(transform.position, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Block_Size)) // 내 밑에에 계단이 있는지 확인
                return -3;
            else
                return -2;
        }

        if (Razer(transform.position, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Block_Size)) //내 아래로 레이저를 쏜다.(계단이 있는지 파악)
        {
            Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f); // 앞에 내 발까지만 레이를 쏴서(높이 파악)
            if (hit.collider != null)
                return 1;
            else
                return -1;
        }

        return 0;
    }




    /// <summary>
    /// 벽이 있는지 확인 하기 위한 함수
    /// </summary>
    /// <returns> 블럭을 잡고있을때와 블럭이 없을때 쏴야는 지점이 다르기에 return을 도출하기 위한 raycast는 다르다.</returns>
    //총 3개의 경우 블럭을 잡고있을때 없을때 블럭을 잡고있을때 뒤로는 한칸 앞으로는 두칸
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
    /// 다음 이동 지점이 플레이어에게 절벽인지 확인하기 위한 함수
    /// </summary>
    private bool Is_There_Cliff()
    {
        return !Physics.Raycast(this.transform.position + Moving_Dir, Vector3.down, GameManager.Game_Manager_Instance.Player_Manager.Character_Height * 0.5f + 0.6f * GameManager.Game_Manager_Instance.Player_Manager.Block_Size);
    }


    /// <summary>
    /// 계단이 있는지 확인하는 함수
    /// </summary>
    /// <param name="origin">쏘는 지점</param>
    /// <param name="TargetVec"> 쏘는 방향 </param>
    /// <param name="distance">쏘는 거리</param>
    /// <returns></returns>
    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance)
    {
        Physics.Raycast(origin, TargetVec, out hit, distance); //2와3 레이어 마스크를 무시한다(포탈 등 있어도 가는곳),interact을 위한 콜라이더(Look Dir,잡고있는 블럭등)
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
    /// 실제 움직임을 구현하는 함수(Character Controller 움직임을 구현
    /// </summary>
    private void Real_Player_Moving()
    {
        if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f)
        {
            Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y); //Target좌표 높이를 현재 높이로 이동시킨다.(없어지면 밑으로 팅기거나 위로 이동할려고 발악 하는 경우 발생)
            Enabling_Player_Character_Controller_To_Fix_Player_Position(Target_Position); //x,z좌표를 Target지점으로 재 변경하는 함수(Controller.Move 함수는 완벽한 한칸 이동이 구현되지 않기에 이렇게 좌표 고정)
            GameManager.Game_Manager_Instance.Player_Manager.Can_Move = true; //다음칸으로 이동할수 있게 변경
            if (GameManager.Game_Manager_Instance.Player_Manager.Input == 0) //다음 칸으로 바로 이동하는 경우 Idle로 변경되지 않고 Run 상태를 유지
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
    /// Character Controller에 의해 이동되고 좌우 좌표의 차이를 방지하기 위해 이동시는 함수
    /// </summary>
    /// <param name="vec">원하는 지점의 좌표</param>
    private void Enabling_Player_Character_Controller_To_Fix_Player_Position(Vector3 vec)
    {
        Player_Character_Controller.enabled = false;
        this.transform.position = vec;
        Player_Character_Controller.enabled = true;
    }




    /// <summary>
    /// 계단이나 충돌에 의해 이동시에 좌표가 이상하게 변경되는 것을 방지하기 위한 함수
    /// </summary>
    /// <returns></returns>
    void Fix_Player_Position_To_Dir()
    {
        if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) //진행 방향이 앞뒤일때
        {
            Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.forward * (Target_Position.z - this.transform.position.z));
        }
        else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) //진행 방향이 양옆일때
        {
            Enabling_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.right * (Target_Position.x - this.transform.position.x));
        }
    }




    /// <summary> 처음 시작할때 움직임(문을 박차고 나간다 던지)등을 위한 함수 </summary>
    public void Start_Moving()
    {
        if (GameManager.Game_Manager_Instance.Player_Manager.Auto_Moving_Needed == true)
        {
            Target_Position += (Look_Dir.localPosition).normalized * 2; // 수식이 복잡한데 캐릭터를 0,0에 두면 높아지는것때문에 lookdir이 좌표가 개같음...

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