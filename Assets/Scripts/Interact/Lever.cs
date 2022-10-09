using UnityEngine;

/// <summary>
/// ��ȣ�ۿ� ��, �Ҵ���� ������Ʈ �ʵ带 ������ ���� ����� �ݴϴ�.
/// </summary>
public class Lever : MonoBehaviour, Interact {
    [SerializeField] GameObject rotateField;        // ������ ������Ʈ
    [SerializeField] float rotateAngle = 90f;       // �ѹ��� ���ư��� �� ����
    float timer = 0f;                               // �ð� üũ�� ���� ������ Ÿ�̸� ����
    float temp;
    bool isRotate = false;                          // �÷��̾ ��ȣ�ۿ��� ���ؼ� true�� ��ȯ ��Ű�� 
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