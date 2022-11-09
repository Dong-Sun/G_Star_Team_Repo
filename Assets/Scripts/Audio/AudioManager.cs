using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public AudioManager Instance { get => instance; }

    [Range(0f, 1f)] public float volume;

    private AudioStorage audioStorage;
    private AudioSource audioSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
            audioStorage = new AudioStorage();
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        ChangeBackSound(SceneManager.GetActiveScene().buildIndex);
        audioSource.Play();
    }

    private void Update() {
        audioSource.volume = volume;
    }

    public void ChangeBackSound(int currentScene) {
        switch (currentScene) {
            case 0:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("startSceneBGM");
                audioSource.Play();
                break;
            case 1:
                audioSource.clip = null;
                break;
            case 2:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("fifthFloor");
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("fourthFloor");
                audioSource.Play();
                break;
            case 4:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("thirdFloor");
                audioSource.Play();
                break;
            case 5:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("secondFloor");
                audioSource.Play();
                break;
            case 6:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("firstFloor");
                audioSource.Play();
                break;
            case 7:
                audioSource.clip = null;
                audioSource.clip = audioStorage.GetSoundTrack("EndingBGM");
                audioSource.Play();
                break;
            default:
                Debug.LogError("not found sceneindex");
                break;
        }
    }

    public void OneShotEvent(string name) {
        audioSource.PlayOneShot(audioStorage.GetSoundTrack(name));
    }
}