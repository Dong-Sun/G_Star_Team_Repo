using DataStruct;
using UnityEngine;

/// <summary>
/// ī�޶� �����ϴµ� �־ �ʿ��� �������� ��� Ŭ���� �Դϴ�.
/// </summary>
public class Camera : MonoBehaviour {
    [SerializeField] protected Transform center;        // ī�޶� ���ư��� �Ǵ� �߽�
    [SerializeField] protected GameObject[] walls;      // 4������ ���� ��Ʈ�� �ϱ� ���� ����
    [SerializeField] protected float aroundCycle = 1f;            // �� ���� �����µ� �ɸ��� �ð�
    protected Dir direction = Dir.ForWard;              // ���� �� �����ִ� ���� ��� ����
    protected Dir lastDirection;                        // ������ ���� ������ ��� ����
    protected float myDeletaTime = 0.004f;              // ī�޶� ���� �� �ݺ��ϴ� Ƚ���� ����
    protected bool isRotate = true;                     // �ѹ����� ���ư� �� �ְ� ���� ���ִ� ����

    protected int[] initAngle = new int[] { 0, 90, 180, -90 };  // �����鼭 �߻��ϴ� �� ������ ���ֱ� ���� �迭
    protected Vector3[] initVector = new Vector3[] {            // �����鼭 �߻��ϴ� ��ǥ ������ ���ֱ� ���� ����
        new Vector3(0f, 5f, -10f),
        new Vector3(-10f, 5f, 0f),
        new Vector3(0f, 5f, 10f),
        new Vector3(10f, 5f, 0f)
    };
}