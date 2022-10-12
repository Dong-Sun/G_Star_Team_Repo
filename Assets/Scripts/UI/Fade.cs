using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour {
    public float animTime = 2f; //Fade 애니메이션 재생시간
    private Image fadeImage; // UGUI의 Image컴포넌트 참조 변수

    private float start = 1f; // Mathf.Lerp 메소드의 첫번째 값.
    private float end = 0f; // Mathf.Lerp 메소드의 두번째 값.

    private float time = 0f;// Mathf.Lerp 메소드의 시간 값.  


    private void Awake() {
        //Image 컴포넌트를 검색해서 참조 변수 값 설정.
        fadeImage = GetComponent<Image>();
    }
   
    void PlayFadeIn() {

        time += Time.deltaTime / animTime;
        if (time > animTime) {
            Color color = fadeImage.color;

            color.a = Mathf.Lerp(start, end, time - animTime);

            fadeImage.color = color;
        }
    }

    public void PlayFadeOut() {
        time += Time.deltaTime / animTime;

        Color color = fadeImage.color;

        color.a = Mathf.Lerp(end, start, time);

        fadeImage.color = color;
        if (time > animTime)
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad();
    }
}