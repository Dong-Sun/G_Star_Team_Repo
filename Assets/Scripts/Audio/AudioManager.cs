using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public AudioManager Instance {
        get { return instance; }
    }


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
                audioSource.clip = audioStorage.StartSceneBGM;
                audioSource.Play();
                break;
            case 1:
                audioSource.clip = null;
                break;
            case 2:
                audioSource.clip = null;
                audioSource.clip = audioStorage.SixthFloor;
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = null;
                audioSource.clip = audioStorage.FourFloor;
                audioSource.Play();
                break;
            case 4:
                audioSource.clip = null;
                audioSource.clip = audioStorage.FifthFloor;
                audioSource.Play();
                break;
            case 5:
                audioSource.clip = null;
                audioSource.clip = audioStorage.ThirdFloor;
                audioSource.Play();
                break;
            default:
                Debug.LogError("Not Found SceneIndex");
                break;
        }
    }

    public void FallRockLava() {
        audioSource.PlayOneShot(audioStorage.FallRockFloor);
    }
    public void SwitchingLever() {
        audioSource.PlayOneShot(audioStorage.SwitchingLever);
    }
    public void OpenDoor() {
        audioSource.PlayOneShot(audioStorage.OpenDoor);
    }
    public void CloseDoor() {
        audioSource.PlayOneShot(audioStorage.CloseDoor);
    }
    public void BulletFire() {
        audioSource.PlayOneShot(audioStorage.BulletFire);
    }
    public void DragRock() {
        audioSource.PlayOneShot(audioStorage.DragRock);
    }
    public void Walk() {
        audioSource.PlayOneShot(audioStorage.Walk);
    }
    public void PaperOpen() {
        audioSource.PlayOneShot(audioStorage.PaperOpen);
    }
    public void PaperClose() {
        audioSource.PlayOneShot(audioStorage.PaperClose);
    }
    public void CreateKey() {
        audioSource.PlayOneShot(audioStorage.CreateKey);
    }
    public void GetKey() {
        audioSource.PlayOneShot(audioStorage.GetKey);
    }
}