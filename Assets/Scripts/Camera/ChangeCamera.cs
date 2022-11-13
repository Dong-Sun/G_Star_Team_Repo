using UnityEngine;
using DataStruct;

public class ChangeCamera : MonoBehaviour {
    [SerializeField] GameObject main;   // �÷��� ���� ī�޶�
    [SerializeField] GameObject start;  // ������ �� ������ ���� �����ִ� ī�޶�
    [SerializeField] GameObject[] end;    // ���� �� ���� ���鼭 ���� ����� �����ִ� ī�޶�

    private void Awake()
    {
        GameManager.Game_Manager_Instance.Change_Camera = this;
    }

    private void Start() {
        ChangeToStart();    // ó�� ������ ��� start ī�޶� ���ְ�

    }
    private void Update() {
    }

    public void ChangeToMain() {
        main.SetActive(true);
        start.SetActive(false);
        EndCameraOff();
    }

    public void ChangeToStart() {
        main.SetActive(false);
        start.SetActive(true);
        EndCameraOff();
    }

    public void ChangeToEnd() {
        main.SetActive(false);
        start.SetActive(false);
        EndCameraOn();
    }

    private void EndCameraOff() {
        foreach (GameObject g in end)
            g.SetActive(false);
    }
    private void EndCameraOn() {
        switch (GameManager.Game_Manager_Instance.Game_Dir) {
            case Dir.ForWard:
                end[0].SetActive(true);
                break;
            case Dir.Left:
                end[1].SetActive(true);
                break;
            case Dir.BackWard:
                end[2].SetActive(true);
                break;
            case Dir.Right:
                end[3].SetActive(true);
                break;
            default:
                break;
        }
        
    }
}
