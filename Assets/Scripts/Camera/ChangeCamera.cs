using UnityEngine;

public class ChangeCamera : MonoBehaviour {
    [SerializeField] GameObject main;   // �÷��� ���� ī�޶�
    [SerializeField] GameObject start;  // ������ �� ������ ���� �����ִ� ī�޶�
    [SerializeField] GameObject end;    // ���� �� ���� ���鼭 ���� ����� �����ִ� ī�޶�

    private void Start() {
        ChangeToStart();    // ó�� ������ ��� start ī�޶� ���ְ�

    }

    private void Update() {
        // �׽�Ʈ�� ����Ű�� ī�޶� ��ȯ
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
    }

    public void ChangeToStart() {
        main.SetActive(false);
        start.SetActive(true);
        end.SetActive(false);
    }

    public void ChangeToEnd() {
        main.SetActive(false);
        start.SetActive(false);
        end.SetActive(true);
    }
}
