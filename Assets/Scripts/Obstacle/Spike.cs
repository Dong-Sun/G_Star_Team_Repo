using UnityEngine;

/// <summary>
/// �Ѿ� �߻�� ������ ���� ��ũ��Ʈ �Դϴ�.
/// </summary>
public class Spike : MonoBehaviour {
    [SerializeField] GameObject Bullet; // �ڱ� �Ѿ��� ���� ���ֱ� ���� ����ִ� ��ü
    [SerializeField] float spawnTimer;  // �Ѿ� ����� �ð�
    float timer = 0f;
    public bool isAttack = true;        // ���� Ȱ��ȭ ����
    public bool setBullet = true;       // �Ѿ� ����� ���� ����

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
    // �Ѿ� Ȱ��ȭ
    private void InitBullet() {
        Bullet.gameObject.SetActive(true);
        AudioManager.instance.BulletFire();
    }
}
