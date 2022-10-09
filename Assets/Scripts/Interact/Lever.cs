using UnityEngine;

/// <summary>
/// 상호작용 시, 할당받은 오브젝트 필드를 돌려서 길을 만들어 줍니다.
/// </summary>
public class Lever : MonoBehaviour, Interact {
    [SerializeField] GameObject rotateField;        // 돌려줄 오브젝트
    [SerializeField] float rotateAngle = 90f;       // 한번에 돌아가게 될 각도
    float timer = 0f;                               // 시간 체크를 위한 임의의 타이머 변수
    float temp;
    bool isRotate = false;                          // 플레이어가 상호작용을 통해서 true로 변환 시키면 
    Vector3 rotating = new Vector3(0, 1f, 0);
    void Update() {
        Rotating();
    }
    private void Rotating() {
        if (isRotate) {
            timer += Time.deltaTime;
            rotateField.transform.Rotate(rotating * rotateAngle * Time.deltaTime);
            if (timer >= 1f) {
                temp = Mathf.Round(rotateField.transform.eulerAngles.y);
                if (temp > 270f) temp = 0f;
                rotateField.transform.eulerAngles
                    = new Vector3(0f, Mathf.Clamp(temp, 0f, 90f), 0f);
                rotateAngle = -rotateAngle;
                isRotate = false;
                timer = 0f;
            }
        }
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}