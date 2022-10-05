using System.Collections;
using UnityEngine;

public class StealthRock : MonoBehaviour, Obstacle {
    float range = 1f;       // 투명도를 조절하기 위한 변수
    float cycle = 2f;       // 투명해지는 속도를 조절하기 위한 변수
    Color color;            // 오브젝트의 색상을 가져와서 새로 초기화 시키기 위한 변수
    bool isStealth = true;  // 한번에 하나의 루틴만 타게끔 하기 위한 논리 변수
    private void Start() {
        color = GetComponent<MeshRenderer>().material.color;    // 현재 색상값 가져옴
    }
    public void Work() {
        if (isStealth) {
            isStealth = false;          // 한번 만 호출 시킨다.
            StartCoroutine(Stealth());  // 코루틴 실행
        }
    }
    IEnumerator Stealth() {
        yield return null;
        while (range > 0) {     // 투명도가 0이면 탈출
            color.a = range;    // range로 color의 투명도를 감소시킨다.
            GetComponent<MeshRenderer>().material.color = color;    // 색상 초기화
            range -= 0.01f * cycle;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);    //완전 투명해지면 비활성화
    }
}
