using DataStruct;
using System.Collections;
using UnityEngine;

/// <summary>
/// 카메라 조작에 대한 구현이 담겨져 있습니다.
/// </summary>
public class CameraController : Camera {
    [SerializeField] protected Transform center;        // 카메라가 돌아가게 되는 중심
    [SerializeField] protected GameObject[] walls;      // 4방향의 벽을 컨트롤 하기 위해 받음
    [SerializeField] protected float aroundCycle = 1f;  // 한 바퀴 돌리는데 걸리는 시간
    protected float myDeletaTime = 0.004f;              // 카메라 돌릴 때 반복하는 횟수를 조절

    private void Start() {
        walls[(int)Dir.ForWard].SetActive(false);           // 정면 벽 비활성화
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
    }

    private void Update() {
        if (PlayerManager.Player_Manager_Instance.Can_Move == true) {   // 플레이어가 이동 중 일때는 카메라 회전에 제한을 둠
            if (Input.GetKeyDown(KeyCode.A) && isRotate) {      // 카메라 회전가능 상태 + A키 입력
                isRotate = false;                               // 입력을 잠궈줌
                lastDirection = direction;                      // 돌리기 전 방향을 저장해둠
                if (direction < Dir.Right)                      // Enum범위를 벗어나는 것을 방지하기 위한 조건문
                    direction = direction + 1;
                else
                    direction = Dir.ForWard;
                Rotate(Dir.Left);                               // 사전준비 완료 후 Rotate함수 실행
            }
            else if (Input.GetKeyDown(KeyCode.D) && isRotate) { // D키 입력, 조건문 내부 설명은 위와 같음
                isRotate = false;
                lastDirection = direction;
                if (direction > Dir.ForWard)
                    direction = direction - 1;
                else
                    direction = Dir.Right;
                Rotate(Dir.Right);
            }
        }
    }

    void Rotate(Dir dir) {
        Time.timeScale = 0f;                                        // 카메라 동작 중에는 시간이 멈춰있어야 함
        GameManager.Game_Manager_Instance.Game_Dir = direction;     // 방향값을 지정 (GameManager에도 방향지정 함수가 있기에 회의 필요)
        StartCoroutine(RotateCamera(dir));                          // 카메라의 회전과 벽의 활성화 여부를 담당하는 코루틴
    }

    IEnumerator RotateCamera(Dir dirAround) {     // dirAround = 돌아가는 방향
        yield return null;
        walls[(int)direction].SetActive(false);     // 바라보게 될 벽은 바로 비활성화 처리함
        float timer = 0f;

        while (timer <= 1f / aroundCycle) {
            timer += myDeletaTime;
            if (dirAround == Dir.Left)      // 회전 방향으로 구분지어 center를 중심으로 돔
                transform.RotateAround(center.position, Vector3.up, aroundCycle * (90f * myDeletaTime));
            if (dirAround == Dir.Right)
                transform.RotateAround(center.position, Vector3.up, -aroundCycle * (90f * myDeletaTime));
            yield return new WaitForSecondsRealtime(myDeletaTime);     // WaitForSecondsRealtime = 실제 시간으로 작동
        }
        // 다 돌고 난 뒤, 오차 범위로 인한 카메라 밀림(혹은 각도 오차범위)을 잡아주기 위한 초기화
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
        walls[(int)lastDirection].SetActive(true);  // 돌리고 난 뒤에는, 기존 비활성화 상태였던 벽을 다시 활성화 시켜줌
        Time.timeScale = 1f;        // 모든 작업이 끝나면 시간 원상복귀
        isRotate = true;            // 다시 돌릴 수 있는 상태로 돌아옴
    }
}