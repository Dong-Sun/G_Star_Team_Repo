using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour{

    public float animTime = 2f; //Fade �ִϸ��̼� ����ð�
    private Image image;
    public float time = 0f;// Mathf.Lerp �޼ҵ��� �ð� ��.  
    public bool Fade_out = false;
    public bool Fade_in = false;
    private Color color;
    private void Awake() {
        //Image ������Ʈ�� �˻��ؼ� ���� ���� �� ����.
        image = GetComponent<Image>();
        color = image.color;
    }

    private void Start()
    {
        time = animTime;
        Fade_out = true;
        SceneLoadManager.scene_load_manager_instance.Fade_UI_Control = this;
    }

    private void Update()
    {
        if (Fade_out == true)
        {
            time -= Time.deltaTime;
            PlayFadeControl();
            return;
        }
        if (Fade_in == true)
        {
            time += Time.deltaTime;
            PlayFadeControl();
            return;
        }
    }
    private bool PlayFadeControl()
    {
        color.a = Mathf.Lerp(0, 1, time / animTime);
        if (Fade_out == true && time <0)
        {
            Fade_out = false;
            return true;
        }
        if ((Fade_in == true && time > animTime))
        {
            Fade_in = false;
            return true;
        }
        image.color = color;
        return false;
    }

}