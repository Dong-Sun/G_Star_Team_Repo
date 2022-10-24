using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour{

    public float animTime = 2f; //Fade �ִϸ��̼� ����ð�
    private Image fadeImage; // UGUI�� Image������Ʈ ���� ����

    private float time = 0f;// Mathf.Lerp �޼ҵ��� �ð� ��.  


    private void Awake() {
        //Image ������Ʈ�� �˻��ؼ� ���� ���� �� ����.
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