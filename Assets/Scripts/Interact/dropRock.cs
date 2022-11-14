using UnityEngine;
using UnityEngine.Events;

public class dropRock : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    [SerializeField] HoldingBlock Rock; // ������ų ������Ʈ
    [SerializeField] GameObject button;         // ��ư�� ������ ���� ������ ���ֱ� ���� ��� ��ü
    [SerializeField] bool isActive = false;     // ��ư�� �������� ���θ� üũ�ϴ� ����
    [SerializeField] float speed = 5f;          // ��ư ������ �ӵ�
    bool swap = false;                          // ��ư ���ٰ� ���Դ� �ϴ� �б⸦ ������
    float timer = 0f;                           // �ð� ����
    Vector3 start = new Vector3(0, 0.2f, 0);    // ��ư ������ �� ��ǥ
    Vector3 end = new Vector3(0, 0, 0);         // ��ư ������ �� ��ǥ
    [SerializeField] GameObject arrow;
    private void Update() {
        if (isActive) {
            if (!swap) {
                PushButton();
            }
            else {
                PullButton();
            }
        }
    }
    private void PushButton() { // ��ư ����
        timer += Time.deltaTime * speed;
        button.transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer > 1f) {
            AudioManager.instance.OneShotEvent("buttonSound");
            swap = !swap;
        }
    }
    private void PullButton() { // ��ư ������
        timer -= Time.deltaTime * speed;
        button.transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer < 0f) {
            swap = !swap;
            isActive = false;
        }
    }

    /// <summary>
    /// ��ư�� ������ Spike���� ������ ���� �����ݴϴ�.
    /// </summary>
    public void Work() {
        isActive = true;
        Rock.gameObject.SetActive(true);
        arrow.SetActive(false);
        quest.Invoke();
    }
}
