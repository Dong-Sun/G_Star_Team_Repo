using DataStruct;
using UnityEngine;




public class PlayerMove : MonoBehaviour {


    [SerializeField] private bool Start_Moving_Needed = true;

    public Vector3 Target_Position;
    private GameObject Look_Dir;
    private CharacterController Player_Character_Controller;
    private int Moving_Speed = 3;
    private int input;
    static RaycastHit hit;

    private void Start() {

        Look_Dir = transform.GetChild(1).gameObject;
        Target_Position = this.transform.position;
        Player_Character_Controller = GetComponent<CharacterController>();
        //Invoke("Start_Moving", 3f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Game_Manager_Instance.Game_Stop == false)
        {
            Change_Target_Position(Change_Dir_To_Position());//�̵��� ��ġ �����Լ��� ���� ���� ���⿡ ���� ���� ���� ����
        }
        Player_Moving(); //���� ������ �Լ�
        Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 0.8f);
        //if (PlayerManager.Player_Manager_Instance.Can_Move == true)
        //{
        //    Player_Character_Controller.Move(Vector3.down * Time.deltaTime * 4f);
        //}
    }
    private Vector3 Change_Dir_To_Position() //���� ���� ���⿡ ���� �̵� �� ��ġ ���� �Լ�
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


    private void Change_Target_Position(Vector3 Moving_Dir) //�̵��� ��ġ ���� �Լ�
    {
        if (PlayerManager.Player_Manager_Instance.Can_Move == true) {
            input = Look_Dir_Arrow();
            if ((input != 0)) {
                Look_Dir.transform.localPosition = input * Moving_Dir+Vector3.down*0.45f;
                if (!Dont_Moving(input*Moving_Dir)) {
                    Target_Position += input * Moving_Dir;
                    PlayerManager.Player_Manager_Instance.Can_Move = false;
                }
                else {
                    if (PlayerManager.Player_Manager_Instance.Player_Die != true)
                        PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Idle);
                }
            }

        }
    }

    private void Player_Moving()//���� ������ �Լ�
    {
        transform.GetChild(0).LookAt(Look_Dir.transform);
        if (PlayerManager.Player_Manager_Instance.Can_Move == false) {
            if (GameManager.Game_Manager_Instance.Game_Dir == Dir.BackWard || GameManager.Game_Manager_Instance.Game_Dir == Dir.ForWard) {
                Fixed_Player_Position(this.transform.position + Vector3.forward * (Target_Position.z - this.transform.position.z));
            }
            else if (GameManager.Game_Manager_Instance.Game_Dir == Dir.Right || GameManager.Game_Manager_Instance.Game_Dir == Dir.Left) {
                Fixed_Player_Position(this.transform.position + Vector3.right * (Target_Position.x - this.transform.position.x));
            }
            if (Mathf.Sqrt(Mathf.Pow(Target_Position.x - this.transform.position.x, 2) + Mathf.Pow(Target_Position.z - this.transform.position.z, 2)) < 0.05f) {
                Target_Position = Target_Position - Vector3.up * (Target_Position.y - this.transform.position.y);
                Fixed_Player_Position(Target_Position);
                PlayerManager.Player_Manager_Instance.Can_Move = true;
                if (!(Look_Dir_Arrow() != 0)) {
                    if(PlayerManager.Player_Manager_Instance.Player_Die!=true)
                        PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Idle);
                }
            }
            else {
                Player_Character_Controller.Move((Target_Position - this.transform.position).normalized * Time.deltaTime * Moving_Speed);
                if (PlayerManager.Player_Manager_Instance.Player_Die != true)
                    PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Run);
            }
        }

    }

    private bool Dont_Moving(Vector3 Moving_Dir) { //����� �ִ��� , 2���� �־ ������ �� ������ ,���̿� ���õ� ������, ���� �����Լ�
        if (Razer(transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)) // ���� �����ʿ� ����� �ִ��� Ȯ��(���� ������)
            return Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Floor_Height*0.5f)); //�̵��� ��ġ�� �����ϰ� �̵��� ������
        if (Razer(transform.position + Moving_Dir, Vector3.down, PlayerManager.Player_Manager_Instance.Block_Size + PlayerManager.Player_Manager_Instance.Character_Height * 0.5f)) //���� �����ʿ� ����� �ִ��� Ȯ��(�Ʒ��� ������)
            return Target_Position_Change(Vector3.down * PlayerManager.Player_Manager_Instance.Floor_Height * 0.5f); //�Ʒ��� ��������
        if (Razer(transform.position, Vector3.down,PlayerManager.Player_Manager_Instance.Block_Size)) //�� �Ʒ��� �������� ���.(���,���,�� ���� �ִ��� �ľ�)
        {
            Physics.Raycast(transform.position + Moving_Dir, Vector3.down, out hit, PlayerManager.Player_Manager_Instance.Character_Height*0.5f); // �տ� �� �߱����� ���̸� ����(���� �ľ�)
            if (hit.collider != null)
            {
                return Target_Position_Change(Vector3.up * (PlayerManager.Player_Manager_Instance.Character_Height * 0.5f + hit.point.y - Target_Position.y));
            }
            else
            {
                return Target_Position_Change(Vector3.up * (hit.point.y-Target_Position.y+ PlayerManager.Player_Manager_Instance.Block_Size*0.5f+ PlayerManager.Player_Manager_Instance.Character_Height * 0.5f));
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

    int Look_Dir_Arrow() //�������� �����ϴ� ������ ��ȣ�� ���� �ϱ� ���� �Լ�
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
        this.transform.position = vec;
        Player_Character_Controller.enabled = true;
    }


    private bool Target_Position_Change(Vector3 vec) //������ ��ġ �����̴� �Լ�
    {
        Target_Position += vec;
        return false;
    }

    private bool Razer(Vector3 origin, Vector3 TargetVec, float distance) //���̸� �i����  �ݸ����� �ִ��� ������ 
    {
        Physics.Raycast(origin, TargetVec, out hit, distance);
        if (hit.collider != null) {
            if (hit.collider.tag == "Stair") {
                return true;
            }
            if (hit.collider.tag == "Lava")
            {
                GameManager.Game_Manager_Instance.Game_Stop=true;
                PlayerManager.Player_Manager_Instance.Player_Die = true;
                PlayerManager.Player_Manager_Instance.playeranimatorcontroller.SetAndPlayAnimation(Player_Animator_Parameter.Die);
                return true;
            }
        }
        return false;
    }
}