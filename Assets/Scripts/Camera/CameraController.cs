using DataStruct;
using System.Collections;
using UnityEngine;

/// <summary>
/// 카메라 조작에 대한 구현이 담겨져 있습니다.
/// </summary>
public class CameraController : Camera {
    private void Start() {
        walls[(int)Dir.ForWard].SetActive(false);           // 정면 벽 비활성화
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
    }

    private void Update() {
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

    void Rotate(Dir dir) {
        Time.timeScale = 0f;                                        // 카메라 동작 중에는 시간이 멈춰있어야 함
        GameManager.Game_Manager_Instance.Game_Dir = direction;     // 방향값을 지정 (GameManager에도 방향지정 함수가 있기에 회의 필요)
        StartCoroutine(Around(dir));                                // 카메라의 회전을 담당하는 코루틴
        StartCoroutine(CoverWall());                                // 벽의 활성화 여부를 담당하는 코루틴
    }

    IEnumerator Around(Dir dirAround) {     // dirAround = 돌아가는 방향
        yield return null;
        float timer = 0f;

        while (timer <= 1f / aroundCycle) {
            timer += 0.01f;
            if (dirAround == Dir.Left)      // 회전 방향으로 구분지어 center를 중심으로 돔
                transform.RotateAround(center.position, Vector3.up, aroundCycle * 0.9f);
            if (dirAround == Dir.Right)
                transform.RotateAround(center.position, Vector3.up, -aroundCycle * 0.9f);
            yield return new WaitForSecondsRealtime(0.01f);     // WaitForSecondsRealtime = 실제 시간으로 작동
        }
        // 다 돌고 난 뒤, 오차 범위로 인한 카메라 밀림(혹은 각도 오차범위)을 잡아주기 위한 초기화
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
        Time.timeScale = 1f;        // 모든 작업이 끝나면 시간 원상복귀
        isRotate = true;            // 다시 돌릴 수 있는 상태로 돌아옴
    }
    IEnumerator CoverWall() {
        yield return null;
        walls[(int)direction].SetActive(false);     // 바라보게 될 벽은 바로 비활성화 처리함

        yield return new WaitForSecondsRealtime(1f / aroundCycle); // 돌아가는 속도에 맞춰서 시간 지연을 시킴

        walls[(int)lastDirection].SetActive(true);  // 돌리고 난 뒤에는, 기존 비활성화 상태였던 벽을 다시 활성화 시켜줌
    }
}