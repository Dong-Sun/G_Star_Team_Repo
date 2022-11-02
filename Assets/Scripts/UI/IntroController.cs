using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class IntroController : MonoBehaviour {
    [SerializeField] PlayableDirector director;
    
    float timelineTimer = 0;

    void Update() {
        timelineTimer += Time.deltaTime;
        if (timelineTimer > director.duration) {
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad(0);
            timelineTimer = 0;
        }
    }
}
