using System.Collections;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {
    [SerializeField] GameObject main;   // �÷��� ���� ī�޶�
    [SerializeField] GameObject start;  // ������ �� ������ ���� �����ִ� ī�޶�
    [SerializeField] GameObject end;    // ���� �� ���� ���鼭 ���� ����� �����ִ� ī�޶�
    [Range(0, 10f)][SerializeField] float delay;

    private void Start() {
        ChangeToStart();    // ó�� ������ ��� start ī�޶� ���ְ�
        StartCoroutine(DelayToChangeMain(delay));   // ���� �ð� ������ ���� ī�޶�� ��ȯ �����ش�
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
