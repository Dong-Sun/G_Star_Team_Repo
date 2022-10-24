using System.Collections;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {
    [SerializeField] GameObject main;   // 플레이 중인 카메라
    [SerializeField] GameObject start;  // 시작할 때 나오는 문을 보여주는 카메라
    [SerializeField] GameObject end;    // 끝날 때 문에 들어가면서 들어가는 장면을 보여주는 카메라
    [Range(0, 10f)][SerializeField] float delay;

    private void Start() {
        ChangeToStart();    // 처음 시작할 대는 start 카메라를 켜주고
        StartCoroutine(DelayToChangeMain(delay));   // 일정 시간 지나면 메인 카메라로 전환 시켜준다
    }

    private void Update() {
        // 테스트용 숫자키로 카메라 전환
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeToStart();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeToMain();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeToEnd();
    }

    public void ChangeToMain() {
        main.SetActive(true);
        start.SetActive(false);
        end.SetActive(false);
        GameManager.Game_Manager_Instance.Game_Stop = false;
    }

    public void ChangeToStart() {
        main.SetActive(false);
        start.SetActive(true);
        end.SetActive(false);
        GameManager.Game_Manager_Instance.Game_Stop = true;
    }

    public void ChangeToEnd() {
        main.SetActive(false);
        start.SetActive(false);
        end.SetActive(true);
    }

    IEnumerator DelayToChangeMain(float timer) {
        yield return new WaitForSeconds(timer);
        ChangeToMain();
    }
}
