using UnityEditor;
using UnityEngine;

public class AudioStorage {
    private string soundPath = @"Assets/Sound/";    // 사운드를 모아둘 폴더 경로

    // 각 층 배경 소리
    private AudioClip fourthFloor;
    private AudioClip fifthFloor;
    private AudioClip sixthFloor;

    // 문 여닫는 소리
    private AudioClip openDoor;
    private AudioClip closeDoor;

    // spike 총알 발사 소리
    private AudioClip bulletFire;

    // 플레이어 움직이는 소리
    private AudioClip walk;

    // 돌 끄는 소리, 떨어지는 소리(용암, 땅)
    private AudioClip dragRock;
    private AudioClip fallRockLava;
    private AudioClip fallRockFloor;

    // 레버 조작 소리, 맵 돌아가는 소리
    private AudioClip switchingLever;
    private AudioClip rotateField;

    // 스테이지 클리어 소리, 플레이어가 죽는 소리
    private AudioClip stageClear;
    private AudioClip playerDie;

    // Property ReadOnly
    public AudioClip FourFloor { get => fourthFloor; }
    public AudioClip FifthFloor { get => fifthFloor; }
    public AudioClip SixthFloor { get => sixthFloor; }
    public AudioClip OpenDoor { get => openDoor; }
    public AudioClip CloseDoor { get => closeDoor; }
    public AudioClip BulletFire { get => bulletFire; }
    public AudioClip Walk { get => walk; }
    public AudioClip DragRock { get => dragRock; }
    public AudioClip FallRockLava { get => fallRockLava; }
    public AudioClip FallRockFloor { get => fallRockFloor; }
    public AudioClip SwitchingLever { get => switchingLever; }
    public AudioClip RotateField { get => rotateField; }
    public AudioClip StageClear { get => stageClear; }
    public AudioClip PlayerDie { get => playerDie; }

    public AudioStorage() {
        LoadAudioClips();
    }

    /// <summary>
    /// 파일 경로를 통해서 오디오 클립을 가져오는 함수
    /// </summary>
    private void LoadAudioClips() {
        // 각 층 배경 소리
        fourthFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        fifthFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        sixthFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // 문 여닫는 소리
        openDoor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        closeDoor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // spike 총알 발사 소리
        bulletFire = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // 플레이어 움직이는 소리
        walk = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // 돌 끄는 소리, 떨어지는 소리(용암, 땅)
        dragRock = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        fallRockLava = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        fallRockFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // 레버 조작 소리, 맵 돌아가는 소리
        switchingLever = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        rotateField = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // 스테이지 클리어 소리, 플레이어가 죽는 소리
        stageClear = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        playerDie = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
    }
}
