using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    private AudioStorage audioStorage;
    public AudioManager Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            audioStorage = new AudioStorage();
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(this);
    }
}