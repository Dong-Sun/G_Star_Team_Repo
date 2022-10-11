using DataStruct;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private bool Start_Coroutine;
    public Vector3 Target_Position;
    private Transform Look_Dir;
    private CharacterController Player_Character_Controller;
    private Vector3 Start_Moving_Target;

    [SerializeField] private int Moving_Speed = 3;
    // Start is called before the first frame update


    private void Start() {
        Start_Coroutine = true;
        Look_Dir = transform.GetChild(1).transform;
        Target_Position = this.transform.position;
        Player_Character_Controller = GetComponent<CharacterController>();
        Invoke("Start_Moving", 3f);
        //StartCoroutine(Staying_Position());

    }

    // Update is called once per frame
    private void Update() {
        if(GameManager.Game_Manager_Instance.Game_Stop==false)
        Change_Target_Position(Change_Dir_To_Position());//이동할 위치 지정함수와 게임 진행 방향에 따른 방향 변경 설정
        Player_Moving(); //실제 움직임 함수
        if (PlayerManager.Player_Manager_Instance.Can_Move == true)
        {
            Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 16f);
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
            if (Input.GetKey(KeyCode.RightArrow)) {
                Look_Dir.localPosition = Moving_Dir+Vector3.down*0.48f;
                if (!Dont_Moving(Moving_Dir)) {
                    Target_Position += Moving_Dir;
                    PlayerManager.Player_Manager_Instance.Can_Move = false;
                }
                else
                {
                    PlayerManager.Player_Manager_Instance.player_animator_parameter = Player_Animator_Parameter.Idle;
                    PlayerManager.Player_Manager_Instance.Player_animator.SetInteger("animator", (int)PlayerManager.Player_Manager_Instance.player_animator_parameter);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                Look_Dir.localPosition = -Moving_Dir+Vector3.down * 0.48f;
                if (!Dont_Moving(-Moving_Dir)) {
                    Target_Position -= Moving_Dir;
                    PlayerManager.Player_Manager_Instance.Can_Move = false;
                }
                else
                {
                    PlayerManager.Player_Manager_Instance.player_animator_parameter = Player_Animator_Parameter.Idle;
                    PlayerManager.Player_Manager_Instance.Player_animator.SetInteger("animator", (int)PlayerManager.Player_Manager_Instance.player_animator_parameter);
                }
            }
        }
    }

    private void Player_Moving()//실제 움직임 함수
    {
        transform.GetChild(0).LookAt(Look_Dir);
        if (PlayerManager.Player_Manager_Instance.Can_Move == false)
        {
            if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard)
            {
                Player_Character_Controller.enabled = false;
                this.transform.position += Vector3.forward * (Target_Position.z - this.transform.position.z);
                Player_Character_Controller.enabled = true;
            }
            else if(GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left)
            {
                Player_Character_Controller.enabled = false;
                this.transform.position += Vector3.right * (Target_Position.x - this.transform.position.x);
                Player_Character_Controller.enabled = true;
            }

            if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f)
            {
                Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y);
                Player_Character_Controller.enabled = false;
                this.transform.position = Target_Position;
                Player_Character_Controller.enabled = true;
                PlayerManager.Player_Manager_Instance.Can_Move = true;
                if(!(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.RightArrow)))
                {
                    PlayerManager.Player_Manager_Instance.player_animator_parameter = Player_Animator_Parameter.Idle;
                    PlayerManager.Player_Manager_Instance.Player_animator.SetInteger("animator", (int)PlayerManager.Player_Manager_Instance.player_animator_parameter);
                }
            }
            else
            {
                if (Start_Coroutine == false)
                {
                    StopCoroutine(Staying_Position());
                    Start_Coroutine = true;
                }
                Player_Character_Controller.Move((Target_Position - this.transform.position).normalized * Time.deltaTime*Moving_Speed);
                PlayerManager.Player_Manager_Instance.player_animator_parameter = Player_Animator_Parameter.Run;
                PlayerManager.Player_Manager_Instance.Player_animator.SetInteger("animator", (int)PlayerManager.Player_Manager_Instance.player_animator_parameter);
            }
        }
        else
        {
            if (Start_Coroutine == true)
            {
                StartCoroutine(Staying_Position());
            }
        }
    }

    private bool Dont_Moving(Vector3 Moving_Dir) {
        RaycastHit hit;

        Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair")
            {
                Target_Position += Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y);
                return false;
            }
        }
        Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Block_Size + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair") {
                Target_Position -= Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f;
                return false;
            }
        }
        Physics.Raycast(transform.position, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Block_Size);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair") {
                Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f);
                if (hit.collider != null) {
                    Target_Position += Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height*0.5f+hit.point.y-Target_Position.y);
                    return false;
                }
                else {
                    Target_Position += Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y);
                    return false;
                }
            }
        }
        return Physics.Raycast(this.transform.position+Vector3.down*0.2f, Moving_Dir, PlayerManager.Player_Manager_Instance.Block_Size, 3) ||
        !Physics.Raycast(this.transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + 0.3f * PlayerManager.Player_Manager_Instance.Block_Size);
    }

    void Start_Moving()
    {
        Target_Position += Vector3.right*2;
        PlayerManager.Player_Manager_Instance.Can_Move = false;
    }


    IEnumerator Staying_Position() {

        while (true)
        {
            Target_Position = new Vector3(Mathf.Round(Target_Position.x * 4) * 0.25f, Mathf.Round((Target_Position.y - PlayerManager.Player_Manager_Instance.Character_Height * 0.5f) * 4) * 0.25f + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f, Mathf.Round(Target_Position.z * 4) * 0.25f);
            Start_Coroutine = false;
            yield return new WaitForSeconds(1f);
        }
    }
    

}