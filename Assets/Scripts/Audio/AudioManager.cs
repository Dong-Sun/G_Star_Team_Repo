using UnityEngine;

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
    }

    private void Update() {
        audioSource.volume = volume;
    }

    public void ChangeBackSound(int currentScene) {
        switch (currentScene) {
            case 0:
                audioSource.clip = audioStorage.SixthFloor;
                break;
            case 1:
                audioSource.clip = audioStorage.FourFloor;
                break;
            case 2:
                audioSource.clip = audioStorage.FifthFloor;
                break;
            default:
                Debug.LogError("Not Found SceneIndex");
                break;
        }
    }

    public void OpenDoor() {
        Debug.Log(audioStorage.OpenDoor);
        audioSource.PlayOneShot(audioStorage.OpenDoor);
    }
    public void CloseDoor() {
        Debug.Log(audioStorage.CloseDoor);
        audioSource.PlayOneShot(audioStorage.CloseDoor);
    }
    public void BulletFire() {
        Debug.Log(audioStorage.BulletFire);
        audioSource.PlayOneShot(audioStorage.BulletFire);
    }
    public void DragRock() {
        Debug.Log(audioStorage.DragRock);
        audioSource.clip = audioStorage.DragRock;
        audioSource.Play();
    }
    public void Walk() {
        Debug.Log(audioStorage.Walk);
        audioSource.clip = audioStorage.Walk;
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }
    
    public bool Is_Playing()
    {
        return audioSource.isPlaying;
    }
}