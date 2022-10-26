using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RotateLever : MonoBehaviour, Interact {
    [SerializeField] GameObject rotateField;        // 돌려줄 오브젝트
    [SerializeField] GameObject stick;              // 레버 스틱
    [SerializeField] float stickSpeed = 3f;         // 스틱 당기는 속도
    [SerializeField] float fieldSpeed = 3f;         // 필드 돌리는 속도
    float stickTimer = 0f;                          // stick 오브젝트 돌리는 시간 체크를 위한 임의의 타이머 변수
    float fieldTimer = 0;                           // rotateField 오브젝트 돌리는 시간 체크를 위한 임의의 타이머 변수
    public bool switching = false;                  // 상호작용 시, 레버를 당기는 것과 미는 것을 구분지어주는 변수
    bool isRotate = false;                          // 플레이어가 상호작용을 통해서 true로 변환 시키면 필드 회전

    public bool Switching {
        get { return switching; }
    }

    void Update() {
        if (rotateField != null) {
            if (isRotate) {
                PullStick();
                if (stickTimer >= 1f) {
                    Rotating();
                }
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        if (!switching)
            stick.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);
        else
            stick.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), 1f - stickTimer);
    }
    private void Rotating() {
        fieldTimer += Time.deltaTime * fieldSpeed;
        if (!switching)
            rotateField.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), fieldTimer);
        else
            rotateField.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), 1 - fieldTimer);
        if (fieldTimer >= 1f) {
            isRotate = false;
            fieldTimer = 0f;
            stickTimer = 0f;
            switching = !switching;
        }
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}