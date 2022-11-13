using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Spike들의 활성화 여부를 제어하는 객체입니다.
/// </summary>
public class Switch : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    [SerializeField] Spike[] spikes;            // 제어할 Spike들을 담는 배열
    [SerializeField] GameObject button;         // 버튼을 누르고 떼는 연출을 해주기 위해 담는 객체
    [SerializeField] bool isActive = false;     // 버튼을 눌렀는지 여부를 체크하는 변수
    [SerializeField] float speed = 5f;          // 버튼 누르는 속도
    bool swap = false;                          // 버튼 들어갔다가 나왔다 하는 분기를 지정함
    float timer = 0f;                           // 시간 제어
    Vector3 start = new Vector3(0, 0.2f, 0);    // 버튼 누르기 전 좌표
    Vector3 end = new Vector3(0, 0, 0);         // 버튼 눌렀을 때 좌표
    [SerializeField] GameObject arrow;
    private void Update() {
        if (isActive) {
            if (!swap) {
                PushButton();
            }
            else {
                PullButton();
            }
        }
    }
    private void PushButton() { // 버튼 들어가기
        timer += Time.deltaTime * speed;
        button.transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer > 1f) {
            AudioManager.instance.OneShotEvent("butttonSound");
            swap = !swap;
        }
    }
    private void PullButton() { // 버튼 나오기
        timer -= Time.deltaTime * speed;
        button.transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer < 0f) {
            swap = !swap;
            isActive = false;
        }
    }

    /// <summary>
    /// 버튼을 누르며 Spike들의 공격을 중지 시켜줍니다.
    /// </summary>
    public void Work() {
        quest.Invoke();
        isActive = true;
        foreach (Spike spike in spikes) {   // 할당 받은 모든 Spike들을 탐색해서 공격 비활성화
            spike.isAttack = false;
        }
        arrow.SetActive(false);
    }
}
