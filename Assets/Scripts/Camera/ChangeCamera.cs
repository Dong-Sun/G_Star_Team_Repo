using UnityEngine;
using DataStruct;

public class ChangeCamera : MonoBehaviour {
    [SerializeField] GameObject main;   // 플레이 중인 카메라
    [SerializeField] GameObject start;  // 시작할 때 나오는 문을 보여주는 카메라
    [SerializeField] GameObject[] end;    // 끝날 때 문에 들어가면서 들어가는 장면을 보여주는 카메라

    private void Awake()
    {
        GameManager.Game_Manager_Instance.Change_Camera = this;
    }

    private void Start() {
        ChangeToStart();    // 처음 시작할 대는 start 카메라를 켜주고

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
