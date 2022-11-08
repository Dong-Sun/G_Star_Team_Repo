using System.Collections;
using UnityEngine;

public class PapyrusController : MonoBehaviour {
    [SerializeField] Animator tutorial;
    Coroutine c;
    public CameraController cam;
    bool isActive = false;
    bool playOneShot = true;
    private void Awake() {
        cam = GameObject.FindObjectOfType<CameraController>();
    }

    private void Update() {
        if(playOneShot && !SceneLoadManager.scene_load_manager_instance.SceneChanging) {
            playOneShot = false;
            OpenPapyrus();
        }

        if(Input.GetKeyDown(KeyCode.W) && !playOneShot && cam.isRotate) {
            if (isActive)
                c = StartCoroutine(ClosePapyrus());
            else {
                if(c != null)
                    StopCoroutine(c);
                OpenPapyrus();
            }     
        }
    }
    void OpenPapyrus() {
        Time.timeScale = 0;
        tutorial.SetBool("Active", true);
        isActive = true;
    }

    IEnumerator ClosePapyrus() {
        tutorial.SetBool("Active", false);
        yield return new WaitForSecondsRealtime(0.75f);
        Time.timeScale = 1;
        isActive = false;
    }
}
