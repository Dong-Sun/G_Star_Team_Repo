using UnityEngine;

/// <summary>
/// 총알 발사대 구현을 위한 스크립트 입니다.
/// </summary>
public class Spike : MonoBehaviour {
    [SerializeField] GameObject Bullet; // 자기 총알을 관리 해주기 위해 담아주는 객체
    [SerializeField] float spawnTimer;  // 총알 재생성 시간
    float timer = 0f;
    public bool isAttack = true;        // 공격 활성화 여부
    public bool setBullet = true;       // 총알 재생성 할지 여부

    private void Update() {
        if (isAttack) {
            if (setBullet) {
                timer += Time.deltaTime;
                if (timer > spawnTimer) {
                    InitBullet();
                    setBullet = false;
                    timer = 0;
                }
            }
        }
    }
    // 총알 활성화
    private void InitBullet() {
        Bullet.gameObject.SetActive(true);
        AudioManager.instance.BulletFire();
    }
}
