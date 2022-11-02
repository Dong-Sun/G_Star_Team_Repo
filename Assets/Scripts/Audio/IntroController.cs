using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class IntroController : MonoBehaviour {
    [SerializeField] PlayableDirector director;
    
    float timelineTimer = 0;
    float holdingTimer = 0;
    

    void Update() {
        timelineTimer += Time.deltaTime;
        if (timelineTimer > director.duration) {
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad(0);
            timelineTimer = 0;
        }

        if(holdingTimer > 1f) {
            SceneLoadManager.scene_load_manager_instance.NextSceneLoad(0);
            timelineTimer = 0;
        }

        if (Input.GetKey(KeyCode.Space))
            holdingTimer += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space))
            holdingTimer = 0;
    }
}
