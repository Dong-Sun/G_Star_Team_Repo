using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class RotateLever : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rotateField;        // 돌려줄 오브젝트
    [SerializeField] GameObject stick;              // 레버 스틱
    [SerializeField] float stickSpeed = 3f;         // 스틱 당기는 속도
    [SerializeField] float fieldSpeed = 3f;         // 필드 돌리는 속도
    float stickTimer = 0f;                          // stick 오브젝트 돌리는 시간 체크를 위한 임의의 타이머 변수
    float fieldTimer = 0f;                          // rotateField 오브젝트 돌리는 시간 체크를 위한 임의의 타이머 변수
    bool isRotate = false;                          // 플레이어가 상호작용을 통해서 true로 변환 시키면 필드 회전
    bool rotateSound = true;
    bool oneShot = true;

    void Update() {
        if (rotateField != null) {
            if (isRotate && oneShot) {
                PullStick();
                if (stickTimer >= 1f) {
                    Rotating();
                }
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        stick.transform.rotation = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);
    }
    private void Rotating() {
        if (rotateSound) {
            rotateSound = false;
            AudioManager.instance.OneShotEvent("rotateField");
            AudioManager.instance.OneShotEvent("movingRoad");
        }
        fieldTimer += Time.deltaTime * fieldSpeed;
        rotateField.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), fieldTimer);
        if (fieldTimer >= 1f) {
            oneShot = false;
            arrow.SetActive(false);
            quest.Invoke();
        }
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}