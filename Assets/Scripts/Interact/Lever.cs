using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 상호작용 시, 할당받은 오브젝트 필드를 돌려서 길을 만들어 줍니다.
/// </summary>
public class Lever : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject stick;              // 레버 스틱
    [SerializeField] float stickSpeed = 3f;         // 스틱 당기는 속도
    float stickTimer = 0f;                          // stick 오브젝트 돌리는 시간 체크를 위한 임의의 타이머 변수
    public bool switching = false;                  // 상호작용 시, 레버를 당기는 것과 미는 것을 구분지어주는 변수
    bool isRotate = false;                          // 플레이어가 상호작용을 통해서 true로 변환 시키면 필드 회전
    bool oneShot = true;

    public bool Switching {
        get { return switching; }
    }

    void Update() {
        if (oneShot) {
            if (isRotate) {
                PullStick();
            }
            if (stickTimer >= 1f) {
                arrow.SetActive(false);
                AudioManager.instance.OneShotEvent("switchingLever");
                stickTimer = 0f;
                switching = true;
                oneShot = false;
                quest.Invoke();
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        stick.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);

    }
    public void Work() {
        isRotate = true;
    }
}