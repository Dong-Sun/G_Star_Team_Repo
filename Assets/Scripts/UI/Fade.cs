using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour {
    public float animTime = 2f; //Fade �ִϸ��̼� ����ð�
    private Image fadeImage; // UGUI�� Image������Ʈ ���� ����

    private float start = 1f; // Mathf.Lerp �޼ҵ��� ù��° ��.
    private float end = 0f; // Mathf.Lerp �޼ҵ��� �ι�° ��.

    private float time = 0f;// Mathf.Lerp �޼ҵ��� �ð� ��.  

    public bool stopIn = false; // false�϶� ����, ���� �����Ҷ�
                                // ���̵������� ���� ����
                                // ������ true��

    public bool stopOut = true;

    private void Awake() {
        //Image ������Ʈ�� �˻��ؼ� ���� ���� �� ����.
        fadeImage = GetComponent<Image>();
    }
    void Start() {

    }
    void Update() {
        //Fade In 
        if (stopIn == false && time <= 2 * animTime) {
            PlayFadeIn();
        }
        if (stopOut == false && time <= animTime) {
            PlayFadeOut();
        }
        if (time >= 2 * animTime && stopIn == false) {
            stopIn = true;
            time = 0;
            Debug.Log("StopIn");
        }
        if (time >= animTime && stopIn == true) {
            stopIn = false;
            stopOut = true;
            time = 0;
            Debug.Log("StopOut");
        }
    }

    void PlayFadeIn() {

        time += Time.deltaTime / animTime;
        if (time > animTime) {
            Color color = fadeImage.color;

            color.a = Mathf.Lerp(start, end, time - animTime);

            fadeImage.color = color;
        }
    }

    void PlayFadeOut() {
        time += Time.deltaTime / animTime;

        Color color = fadeImage.color;

        color.a = Mathf.Lerp(end, start, time);

        fadeImage.color = color;
        if (time > animTime)
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}