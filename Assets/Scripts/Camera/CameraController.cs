using DataStruct;
using System.Collections;
using UnityEngine;

/// <summary>
/// ī�޶� ���ۿ� ���� ������ ����� �ֽ��ϴ�.
/// </summary>
public class CameraController : Camera {
    private void Start() {
        walls[(int)Dir.ForWard].SetActive(false);           // ���� �� ��Ȱ��ȭ
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
    }

    private void Update() {
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

    void Rotate(Dir dir) {
        Time.timeScale = 0f;                                        // ī�޶� ���� �߿��� �ð��� �����־�� ��
        GameManager.Game_Manager_Instance.Game_Dir = direction;     // ���Ⱚ�� ���� (GameManager���� �������� �Լ��� �ֱ⿡ ȸ�� �ʿ�)
        StartCoroutine(Around(dir));                                // ī�޶��� ȸ���� ����ϴ� �ڷ�ƾ
        StartCoroutine(CoverWall());                                // ���� Ȱ��ȭ ���θ� ����ϴ� �ڷ�ƾ
    }

    IEnumerator Around(Dir dirAround) {     // dirAround = ���ư��� ����
        yield return null;
        float timer = 0f;

        while (timer <= 1f / aroundCycle) {
            timer += 0.01f;
            if (dirAround == Dir.Left)      // ȸ�� �������� �������� center�� �߽����� ��
                transform.RotateAround(center.position, Vector3.up, aroundCycle * 0.9f);
            if (dirAround == Dir.Right)
                transform.RotateAround(center.position, Vector3.up, -aroundCycle * 0.9f);
            yield return new WaitForSecondsRealtime(0.01f);     // WaitForSecondsRealtime = ���� �ð����� �۵�
        }
        // �� ���� �� ��, ���� ������ ���� ī�޶� �и�(Ȥ�� ���� ��������)�� ����ֱ� ���� �ʱ�ȭ
        transform.rotation = Quaternion.Euler(new Vector3(8, initAngle[(int)direction], 0));
        transform.localPosition = initVector[(int)direction];
        Time.timeScale = 1f;        // ��� �۾��� ������ �ð� ���󺹱�
        isRotate = true;            // �ٽ� ���� �� �ִ� ���·� ���ƿ�
    }
    IEnumerator CoverWall() {
        yield return null;
        walls[(int)direction].SetActive(false);     // �ٶ󺸰� �� ���� �ٷ� ��Ȱ��ȭ ó����

        yield return new WaitForSecondsRealtime(1f / aroundCycle); // ���ư��� �ӵ��� ���缭 �ð� ������ ��Ŵ

        walls[(int)lastDirection].SetActive(true);  // ������ �� �ڿ���, ���� ��Ȱ��ȭ ���¿��� ���� �ٽ� Ȱ��ȭ ������
    }
}