using DataStruct;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

    private bool Can_Move;
    private Vector3 Target_Position;
    private Transform Look_Dir;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private int Moving_Speed = 3;
    // Start is called before the first frame update


    private void Start() {
        Can_Move = true;
        Target_Position = this.transform.position;
        Look_Dir = transform.GetChild(1).transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        //StartCoroutine(Staying_Position());
    }

    // Update is called once per frame
    private void Update() {
        Change_Target_Position(Change_Dir_To_Position());//이동할 위치 지정함수와 게임 진행 방향에 따른 방향 변경 설정
        Player_Moving(); //실제 움직임 함수
    }
    private Vector3 Change_Dir_To_Position() //게임 진행 방향에 따른 이동 될 위치 지정 함수
    {
        if (Can_Move == true) {
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
        if (Can_Move == true) {
            if (Input.GetKey(KeyCode.RightArrow)) {
                Look_Dir.localPosition = Moving_Dir+Vector3.down*0.48f;
                if (!Dont_Moving(Moving_Dir)) {
                    Target_Position += Moving_Dir;
                    Can_Move = false;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                Look_Dir.localPosition = -Moving_Dir+Vector3.down * 0.48f;
                if (!Dont_Moving(-Moving_Dir)) {
                    Target_Position -= Moving_Dir;
                    Can_Move = false;
                }
            }
        }
    }

    private void Player_Moving()//실제 움직임 함수
    {
        navMeshAgent.SetDestination(Target_Position);
        transform.GetChild(0).LookAt(Look_Dir);
        Debug.Log(Target_Position);
        if (Can_Move == false)
        {
            if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f)
            {
                Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y);
                this.transform.position = Target_Position;
                Can_Move = true;
            }
        }
    }

    private bool Dont_Moving(Vector3 Moving_Dir) {
        RaycastHit hit;

        Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair") {
                Target_Position += Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f;
                return false;
            }
            else if (hit.collider.tag == "Object") {
                return true;
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
                    Target_Position += Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f;
                    return false;
                }
                else {
                    Target_Position -= Vector3.up * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f;
                    return false;
                }
            }
        }
        return Physics.Raycast(this.transform.position, Moving_Dir, PlayerManager.Player_Manager_Instance.Block_Size, 3) ||
        !Physics.Raycast(this.transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + 0.3f * PlayerManager.Player_Manager_Instance.Block_Size);
    }

    //IEnumerator Staying_Position() {
        
    //        Target_Position = new Vector3(Mathf.Round(Target_Position.x * 4) * 0.25f, Mathf.Round((Target_Position.y - PlayerManager.Player_Manager_Instance.Character_Height * 0.5f) * 4) * 0.25f + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f, Mathf.Round(Target_Position.z * 4) * 0.25f);
    //}
}