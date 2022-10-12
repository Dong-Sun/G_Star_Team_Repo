using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour {
    public float animTime = 2f; //Fade �ִϸ��̼� ����ð�
    private Image fadeImage; // UGUI�� Image������Ʈ ���� ����

    private float start = 1f; // Mathf.Lerp �޼ҵ��� ù��° ��.
    private float end = 0f; // Mathf.Lerp �޼ҵ��� �ι�° ��.

    private float time = 0f;// Mathf.Lerp �޼ҵ��� �ð� ��.  


    private void Awake() {
        //Image ������Ʈ�� �˻��ؼ� ���� ���� �� ����.
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