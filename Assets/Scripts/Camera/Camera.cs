using DataStruct;
using UnityEngine;

/// <summary>
/// 카메라를 조작하는데 있어서 필요한 정보들을 담는 클래스 입니다.
/// </summary>
public class Camera : MonoBehaviour {
    [HideInInspector] public Dir direction = Dir.ForWard;              // 돌린 후 보고있는 면을 담는 변수
    [HideInInspector] public bool isRotate = true;                     // 한번씩만 돌아갈 수 있게 제어 해주는 변수
    protected Dir lastDirection;                        // 돌리기 직전 방향을 담는 변수
    protected int[] initAngle = new int[] { 0, 90, 180, -90 };  // 돌리면서 발생하는 각 오차를 없애기 위한 배열
    protected Vector3[] initVector = new Vector3[] {            // 돌리면서 발생하는 좌표 오차를 없애기 위한 벡터
        new Vector3(0f, 5f, -10f),
        new Vector3(-10f, 5f, 0f),
        new Vector3(0f, 5f, 10f),
        new Vector3(10f, 5f, 0f)
    };
}