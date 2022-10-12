using DataStruct;
using UnityEngine;

/// <summary>
/// ī�޶� �����ϴµ� �־ �ʿ��� �������� ��� Ŭ���� �Դϴ�.
/// </summary>
public class Camera : MonoBehaviour {
    [HideInInspector] public Dir direction = Dir.ForWard;              // ���� �� �����ִ� ���� ��� ����
    [HideInInspector] public bool isRotate = true;                     // �ѹ����� ���ư� �� �ְ� ���� ���ִ� ����
    protected Dir lastDirection;                        // ������ ���� ������ ��� ����
    protected int[] initAngle = new int[] { 0, 90, 180, -90 };  // �����鼭 �߻��ϴ� �� ������ ���ֱ� ���� �迭
    protected Vector3[] initVector = new Vector3[] {            // �����鼭 �߻��ϴ� ��ǥ ������ ���ֱ� ���� ����
        new Vector3(0f, 5f, -10f),
        new Vector3(-10f, 5f, 0f),
        new Vector3(0f, 5f, 10f),
        new Vector3(10f, 5f, 0f)
    };
}