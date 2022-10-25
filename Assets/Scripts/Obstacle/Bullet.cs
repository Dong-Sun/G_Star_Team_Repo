using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spike에서 나오는 총알의 기능을 구현한 클래스입니다.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;  // 총알의 이동속도
    float destroyTimer = 5f;            // 총알이 자체적으로 사라지는데 걸리는 시간
    float timer = 0f;
    Spike spike;                        // 재생성을 위한 부모클래스 할당
    private void Start() {
        spike = transform.parent.GetComponent<Spike>();
    }
    void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * speed);   // 전방으로 이동
        timer += Time.deltaTime;
        if(timer > destroyTimer) {
            Init();
        }
    }
    private void Init() {   // 총알 재활용을 위해서 데이터들을 초기화 시켜주는 함수
        transform.localPosition = Vector3.zero;
        spike.setBullet = true;
        timer = 0f;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {   // 총알 충돌 처리

        if (other.TryGetComponent<PlayerManager>(out PlayerManager playermanger) == true)
        {
            if (GameManager.Game_Manager_Instance.Game_Stop == false)
                playermanger.Player_Dying();
        }

    }
}
