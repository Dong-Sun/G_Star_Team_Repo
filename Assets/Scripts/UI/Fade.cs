using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour{

    public float animTime = 2f; //Fade 애니메이션 재생시간
    private Image fadeImage; // UGUI의 Image컴포넌트 참조 변수

    private float time = 0f;// Mathf.Lerp 메소드의 시간 값.  


    private void Awake() {
        //Image 컴포넌트를 검색해서 참조 변수 값 설정.
        fadeImage = GetComponent<Image>();
    }

    private void Update()
    {
    }

    public void PlayFadeIn()
    {
        time = 0;
        Color color = fadeImage.color;
        while (time < animTime)
        {
            color.a = Mathf.Lerp(0, 1, time % animTime);
            fadeImage.color = color;
            time += Time.deltaTime;
        }
    }

    public void PlayFadeOut() {
        time = 0;
        Color color = fadeImage.color;
        fadeImage.color = color;
        while (time < animTime)
        {
            color.a = Mathf.Lerp(1, 0, time % animTime);
            fadeImage.color = color;
            time += Time.deltaTime;
        }
        
    }
}