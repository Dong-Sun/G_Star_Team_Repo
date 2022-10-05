using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour {
    public float animTime = 2f; //Fade 애니메이션 재생시간
    private Image fadeImage; // UGUI의 Image컴포넌트 참조 변수

    private float start = 1f; // Mathf.Lerp 메소드의 첫번째 값.
    private float end = 0f; // Mathf.Lerp 메소드의 두번째 값.

    private float time = 0f;// Mathf.Lerp 메소드의 시간 값.  

    public bool stopIn = false; // false일때 실행, 게임 시작할때
                                // 페이드인으로 들어가기 위함
                                // 싫으면 true로

    public bool stopOut = true;

    private void Awake() {
        //Image 컴포넌트를 검색해서 참조 변수 값 설정.
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