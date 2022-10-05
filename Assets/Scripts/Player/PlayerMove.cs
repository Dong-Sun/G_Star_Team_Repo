using DataStruct;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private bool Can_Move;
    private Vector3 Target_Position;
    private Transform Look_Dir;

    [SerializeField] private int Moving_Speed = 3;
    // Start is called before the first frame update


    private void Start() {
        Target_Position = this.transform.position;
        Look_Dir = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    private void Update() {
        Debug.DrawLine(this.transform.position, Look_Dir.position, Color.red);
        Change_Target_Position(Change_Dir_To_Position());//이동할 위치 지정함수와 게임 진행 방향에 따른 방향 변경 설정
        Player_Moving(); //실제 움직임 함수
        //StartCoroutine(Staying_Position());
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
                Look_Dir.localPosition = Moving_Dir;
                if (!Dont_Moving(Moving_Dir)) {
                    Target_Position += Moving_Dir;
                    Can_Move = false;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                Look_Dir.localPosition = -Moving_Dir;
                if (!Dont_Moving(-Moving_Dir)) {
                    Target_Position -= Moving_Dir;
                    Can_Move = false;
                }
            }
        }
    }

    private void Player_Moving()//실제 움직임 함수
    {
        if ((Target_Position - transform.position).magnitude < 0.05f) {
            this.transform.position = Target_Position;
            Can_Move = true;
        }
        else {
            this.transform.position += (Target_Position - transform.position).normalized * Time.deltaTime * Moving_Speed;
            this.transform.GetChild(0).transform.LookAt(Look_Dir);
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

    IEnumerator Staying_Position() {
        StartCoroutine(Staying_Position());
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x * 4) * 0.25f, Mathf.Round(this.transform.position.y * 4) * 0.25f + PlayerManager.Player_Manager_Instance.Character_Height, Mathf.Round(this.transform.position.z * 4) * 0.25f);
        yield return new WaitForSeconds(5f);
    }
}