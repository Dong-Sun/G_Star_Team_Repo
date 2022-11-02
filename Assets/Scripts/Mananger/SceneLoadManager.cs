using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour {
    public static SceneLoadManager scene_load_manager_instance;
    public Fade Fade_UI_Control;

    private void Awake() {
        if (scene_load_manager_instance == null) {
            scene_load_manager_instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void NextSceneLoad(int time) {
        if (!(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)) {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1, time));
        }
    }

    public void CurrentSceneLoad(int time) {

        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex, time));
    }
    public void Start_Scene_Load(int time)
    {
        StartCoroutine(LoadScene(0, time));
    }
    
    IEnumerator LoadScene(int buildIndex, float Time) {
        Fade_UI_Control.Fade_in = true;
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(buildIndex);
        AudioManager.instance.ChangeBackSound(buildIndex);
        yield return new WaitForSeconds(0.5f);
        GameManager.Game_Manager_Instance.Initialize_GameData();
    }
}
