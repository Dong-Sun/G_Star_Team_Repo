using DataStruct;
using UnityEngine;




public class PlayerMove : MonoBehaviour {


    [SerializeField] private bool Start_Moving_Needed = true;

    public Vector3 Target_Position;
    private Vector3 Look_Dir;
    private CharacterController Player_Character_Controller;
    private int Moving_Speed = 3;
    private int input;
    static RaycastHit hit;

    private void Start() {
        Look_Dir = transform.GetChild(1).transform.position;
        Target_Position = this.transform.position;
        Player_Character_Controller = GetComponent<CharacterController>();
        Invoke("Start_Moving", 3f);
    }

    // Update is called once per frame
    private void Update() {
        if (GameManager.Game_Manager_Instance.Game_Stop == false)
            Change_Target_Position(Change_Dir_To_Position());//이동할 위치 지정함수와 게임 진행 방향에 따른 방향 변경 설정
        Player_Moving(); //실제 움직임 함수
        if (PlayerManager.Player_Manager_Instance.Can_Move == true) {
            Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 4f);
        }
    }
    private Vector3 Change_Dir_To_Position() //게임 진행 방향에 따른 이동 될 위치 지정 함수
    {
        if (PlayerManager.Player_Manager_Instance.Can_Move == true) {
            switch (GameManager.Game_Manager_Instance.Game_Dir) {
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


    private void Change_Target_Position(Vector3 Moving_Dir) //이동할 위치 변경 함수
    {
        if (PlayerManager.Player_Manager_Instance.Can_Move == true) {
            input = Look_Dir_Arrow();
            if ((input != 0)) {
                Look_Dir = input * Moving_Dir + Vector3.down * 0.48f;
                if (!Dont_Moving(Moving_Dir)) {
                    Target_Position += input * Moving_Dir;
                    PlayerManager.Player_Manager_Instance.Can_Move = false;
                }
                else {
                    PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Idle);
                }
            }

        }
    }

    private void Player_Moving()//실제 움직임 함수
    {
        transform.GetChild(0).LookAt(Look_Dir);
        if (PlayerManager.Player_Manager_Instance.Can_Move == false) {
            if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) {
                Fixed_Player_Position(Vector3.forward * (Target_Position.z - this.transform.position.z));
            }
            else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) {
                Fixed_Player_Position(Vector3.right * (Target_Position.x - this.transform.position.x));
            }
            if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f) {
                Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y);
                Fixed_Player_Position(Target_Position);
                PlayerManager.Player_Manager_Instance.Can_Move = true;
                if (!(Look_Dir_Arrow() != 0)) {
                    PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Idle);
                }
            }
            else {
                Player_Character_Controller.Move((Target_Position - this.transform.position).normalized * Time.deltaTime * Moving_Speed);
                PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Run);
            }
        }

    }

    private bool Dont_Moving(Vector3 Moving_Dir) { //계단이 있는지 , 2층에 있어서 움직일 수 없는지 ,
        if (Razer(transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)) // 진행 방향쪽에 계단이 있는지 확인
            return Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y)); //이동할 위치를 변경하고 이동값 보내느
        if (Razer(transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)) //
            return Target_Position_Change(Vector3.down * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f);
        if (Razer(transform.position, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size)) {
            Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f);
            if (hit.collider != null) {
                return Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y));
            }
            else {
                return Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y));
            }
        }
        return Physics.Raycast(this.transform.position + Vector3.down * 0.4f, Moving_Dir, PlayerManager.Player_Manager_Instance.Block_Size, 3) ||
        !Physics.Raycast(this.transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + 0.3f * PlayerManager.Player_Manager_Instance.Block_Size);
    }

    void Start_Moving() {
        if (Start_Moving_Needed == true) {
            Target_Position += Vector3.right * 2;
            PlayerManager.Player_Manager_Instance.Can_Move = false;
        }
    }

    int Look_Dir_Arrow() //움직임을 관리하는 벡터의 부호를 관리 하기 위한 함수
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            return -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            return 1;
        }
        else {
            return 0;
        }
    }

    private void Fixed_Player_Position(Vector3 vec) {
        Player_Character_Controller.enabled = false;
        this.transform.position += vec;
        Player_Character_Controller.enabled = true;
    }


    private bool Target_Position_Change(Vector3 vec) //움직일 위치 움직이는 함수
    {

        Target_Position += vec * 2;
        return false;
    }

    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance) //레이를 쐇을때  콜리더가 있는지 없는지 
    {
        Physics.Raycast(origin, TargetVec, out hit, distance);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair") {
                return true;
            }
            if (hit.collider.tag == "Lava") {
                return false;
            }
        }
        return false;
    }
}