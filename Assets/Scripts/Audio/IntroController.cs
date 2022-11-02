using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class IntroController : MonoBehaviour {
    float timer = 0;
    [SerializeField] PlayableDirector director;
    

    void Update() {
        timer += Time.deltaTime;
        if (timer > director.duration) {
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad(0);
            timer = 0;
        }
    }
}
