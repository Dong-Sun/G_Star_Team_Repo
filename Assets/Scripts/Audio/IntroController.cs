using UnityEngine;

public class IntroController : MonoBehaviour {
    float timer = 0;

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 24f) {
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad(0);
            timer = 0;
        }
    }
}
