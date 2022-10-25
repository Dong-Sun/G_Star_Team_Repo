using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spike���� ������ �Ѿ��� ����� ������ Ŭ�����Դϴ�.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;  // �Ѿ��� �̵��ӵ�
    float destroyTimer = 5f;            // �Ѿ��� ��ü������ ������µ� �ɸ��� �ð�
    float timer = 0f;
    Spike spike;                        // ������� ���� �θ�Ŭ���� �Ҵ�
    private void Start() {
        spike = transform.parent.GetComponent<Spike>();
    }
    void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * speed);   // �������� �̵�
        timer += Time.deltaTime;
        if(timer > destroyTimer) {
            Init();
        }
    }
    private void Init() {   // �Ѿ� ��Ȱ���� ���ؼ� �����͵��� �ʱ�ȭ �����ִ� �Լ�
        transform.localPosition = Vector3.zero;
        spike.setBullet = true;
        timer = 0f;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {   // �Ѿ� �浹 ó��

        if (other.TryGetComponent<PlayerManager>(out PlayerManager playermanger) == true)
        {
            if (GameManager.Game_Manager_Instance.Game_Stop == false)
                playermanger.Player_Dying();
        }

    }
}
