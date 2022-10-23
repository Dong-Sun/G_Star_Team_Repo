using DataStruct;
using System.Collections;
using UnityEngine;

/// <summary>
/// ī�޶� ���ۿ� ���� ������ ����� �ֽ��ϴ�.
/// </summary>
public class CameraController : Camera {
    [SerializeField] protected Transform center;        // ī�޶� ���ư��� �Ǵ� �߽�
    [SerializeField] protected GameObject[] walls;      // 4������ ���� ��Ʈ�� �ϱ� ���� ����
    [SerializeField] protected float aroundCycle = 1f;  // �� ���� �����µ� �ɸ��� �ð�
    protected float myDeletaTime = 0.004f;              // ī�޶� ���� �� �ݺ��ϴ� Ƚ���� ����

    private void Start() {
        walls[(int)Dir.ForWard].SetActive(false);           // ���� �� ��Ȱ��ȭ
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
    }

    private void Update() {
        if (PlayerManager.Player_Manager_Instance.Can_Move == true) {   // �÷��̾ �̵� �� �϶��� ī�޶� ȸ���� ������ ��
            if (Input.GetKeyDown(KeyCode.A) && isRotate) {      // ī�޶� ȸ������ ���� + AŰ �Է�
                isRotate = false;                               // �Է��� �����
                lastDirection = direction;                      // ������ �� ������ �����ص�
                if (direction < Dir.Right)                      // Enum������ ����� ���� �����ϱ� ���� ���ǹ�
                    direction = direction + 1;
                else
                    direction = Dir.ForWard;
                Rotate(Dir.Left);                               // �����غ� �Ϸ� �� Rotate�Լ� ����
            }
            else if (Input.GetKeyDown(KeyCode.D) && isRotate) { // DŰ �Է�, ���ǹ� ���� ������ ���� ����
                isRotate = false;
                lastDirection = direction;
                if (direction > Dir.ForWard)
                    direction = direction - 1;
                else
                    direction = Dir.Right;
                Rotate(Dir.Right);
            }
        }
    }

    void Rotate(Dir dir) {
        Time.timeScale = 0f;                                        // ī�޶� ���� �߿��� �ð��� �����־�� ��
        GameManager.Game_Manager_Instance.Game_Dir = direction;     // ���Ⱚ�� ���� (GameManager���� �������� �Լ��� �ֱ⿡ ȸ�� �ʿ�)
        StartCoroutine(RotateCamera(dir));                          // ī�޶��� ȸ���� ���� Ȱ��ȭ ���θ� ����ϴ� �ڷ�ƾ
    }

    IEnumerator RotateCamera(Dir dirAround) {     // dirAround = ���ư��� ����
        yield return null;
        walls[(int)direction].SetActive(false);     // �ٶ󺸰� �� ���� �ٷ� ��Ȱ��ȭ ó����
        float timer = 0f;

        while (timer <= 1f / aroundCycle) {
            timer += myDeletaTime;
            if (dirAround == Dir.Left)      // ȸ�� �������� �������� center�� �߽����� ��
                transform.RotateAround(center.position, Vector3.up, aroundCycle * (90f * myDeletaTime));
            if (dirAround == Dir.Right)
                transform.RotateAround(center.position, Vector3.up, -aroundCycle * (90f * myDeletaTime));
            yield return new WaitForSecondsRealtime(myDeletaTime);     // WaitForSecondsRealtime = ���� �ð����� �۵�
        }
        // �� ���� �� ��, ���� ������ ���� ī�޶� �и�(Ȥ�� ���� ��������)�� ����ֱ� ���� �ʱ�ȭ
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
        walls[(int)lastDirection].SetActive(true);  // ������ �� �ڿ���, ���� ��Ȱ��ȭ ���¿��� ���� �ٽ� Ȱ��ȭ ������
        Time.timeScale = 1f;        // ��� �۾��� ������ �ð� ���󺹱�
        isRotate = true;            // �ٽ� ���� �� �ִ� ���·� ���ƿ�
    }
}