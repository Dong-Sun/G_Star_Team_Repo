using DataStruct;
using UnityEngine;
using System.Collections;




public class PlayerMove : MonoBehaviour {


    [SerializeField] private bool Start_Moving_Needed = true;

    public Vector3 Target_Position;
    private GameObject Look_Dir;
    private CharacterController Player_Character_Controller;
    private int Moving_Speed = 3;
    static RaycastHit hit;

    private void Start() {

        Look_Dir = transform.GetChild(1).gameObject;
        Target_Position = this.transform.position;
        Player_Character_Controller = GetComponent<CharacterController>();
        PlayerManager.Player_Manager_Instance.Can_Move = false;
        Invoke("Start_Moving", 2f);
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Game_Manager_Instance.Game_Stop == false)
        {
            Change_Target_Position(Change_Dir_To_Position());//이동할 위치 지정함수와 게임 진행 방향에 따른 방향 변경 설정
        }
        Player_Moving(); //실제 움직임 함수
        Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 0.8f);
        //if (PlayerManager.Player_Manager_Instance.Can_Move == true)
        //{
        //    Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 4f);
        //}
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
            PlayerManager.Player_Manager_Instance.input = Look_Dir_Arrow();
            if ((PlayerManager.Player_Manager_Instance.input != 0)) {
                Look_Dir.transform.localPosition = PlayerManager.Player_Manager_Instance.input * Moving_Dir+Vector3.down*0.45f;
                if (!Dont_Moving(PlayerManager.Player_Manager_Instance.input *Moving_Dir)) {
                    Target_Position += PlayerManager.Player_Manager_Instance.input * Moving_Dir;
                    Is_There_Stair(PlayerManager.Player_Manager_Instance.input * Moving_Dir);
                    PlayerManager.Player_Manager_Instance.Can_Move = false;
                }
                else {
                    PlayerManager.Player_Manager_Instance.in_motion = false;
                    PlayerManager.Player_Manager_Instance.playeranimatorcontroller.Player_Animator_Parameter_Control();
                }
            }

        }
    }

    private void Player_Moving()//실제 움직임 함수
    {
        transform.GetChild(0).LookAt(Look_Dir.transform);
        if (PlayerManager.Player_Manager_Instance.Can_Move == false) {
            if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f) {
                Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y);
                Enable_Player_Character_Controller_To_Fix_Player_Position(Target_Position);
                if(GameManager.Game_Manager_Instance.Game_Stop ==false)
                    PlayerManager.Player_Manager_Instance.Can_Move = true;
                if ((PlayerManager.Player_Manager_Instance.input=Look_Dir_Arrow()) == 0) {
                    PlayerManager.Player_Manager_Instance.in_motion = false;
                    PlayerManager.Player_Manager_Instance.playeranimatorcontroller.Player_Animator_Parameter_Control();
                }
            }
            else {
                Player_Character_Controller.Move((Target_Position - this.transform.position).normalized * Time.deltaTime * Moving_Speed);
                PlayerManager.Player_Manager_Instance.in_motion = true;
                PlayerManager.Player_Manager_Instance.playeranimatorcontroller.Player_Animator_Parameter_Control();
            }
        }

    }

    private bool Dont_Moving(Vector3 Moving_Dir) { //계단이 있는지 , 2층에 있어서 움직일 수 없는지 ,높이에 관련된 레이캐스트, 높이 조절함수
        return Physics.Raycast(this.transform.position + Vector3.down * 0.4f, Moving_Dir, PlayerManager.Player_Manager_Instance.Block_Size, 3) ||
        !Physics.Raycast(this.transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + 0.3f * PlayerManager.Player_Manager_Instance.Block_Size);
    }

    void Start_Moving() { //처음 시작할때 움직임(문을 박차고 나간다 던지)등을 위한 함수
        if (Start_Moving_Needed == true) {
            if (Look_Dir.transform.localPosition.x != 0)
                Target_Position += (Vector3.right * Look_Dir.transform.localPosition.x).normalized * 2;
            else if(Look_Dir.transform.localPosition.z != 0)
                Target_Position += (Vector3.forward * Look_Dir.transform.localPosition.z).normalized * 2;
            PlayerManager.Player_Manager_Instance.Can_Move = false;
            StartCoroutine(Fix_Player_Position());
        }
    }

    int Look_Dir_Arrow() //움직임을 관리하는 벡터의 부호를 관리 하기 위한 함수(또한 키 인풋값을 얻어오기 위한 함수)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            return 1;
        else 
            return 0;
    }

    private void Enable_Player_Character_Controller_To_Fix_Player_Position(Vector3 vec) {
        Player_Character_Controller.enabled = false;
        this.transform.position = vec;
        Player_Character_Controller.enabled = true;
    }


    private void Target_Position_Change(Vector3 vec) //움직일 위치 움직이는 함수
    {
        Target_Position += vec;
    }

    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance) //레이를 쐇을때  콜리더가 있는지 없는지 
    {
        Physics.Raycast(origin, TargetVec, out hit, distance);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair") {
                return true;
            }
        }
        return false;
    }

    IEnumerator Fix_Player_Position() // 계단이나 충돌에 의해 좌표가 이상하게 변경되는 것을 방지하기 위한 함수
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) //진행 방향이 앞뒤일때
            {
                Enable_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.forward * (Target_Position.z - this.transform.position.z));
            }
            else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) //진행 방향이 양옆일때
            {
                Enable_Player_Character_Controller_To_Fix_Player_Position(this.transform.position + Vector3.right * (Target_Position.x - this.transform.position.x));
            }
        }
    }

    void Is_There_Stair(Vector3 Moving_Dir) //계단의 위치와 방향에 따라 움직이는 방향 조절
    {
        if (Razer(transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)) // 진행 방향쪽에 계단이 있는지 확인(위로 가느냐)
            Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f)); //이동할 위치를 변경하고 이동값 보내기
        if (Razer(transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)) //진행 방향쪽에 계단이 있는지 확인(아래로 가느냐)
            Target_Position_Change(Vector3.down * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f); //아래로 내려가기
        if (Razer(transform.position, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size)) //내 아래로 레이저를 쏜다.(계단이 있는지 파악)
        {
            Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f); // 앞에 내 발까지만 레이를 쏴서(높이 파악)
            if (hit.collider != null)
                Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y)); // 위로
            else
                Target_Position_Change(Vector3.up * (hit.point.y - Target_Position.y + PlayerManager.Player_Manager_Instance.Block_Size * 0.5f + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)); //아래로
        }
    }
}