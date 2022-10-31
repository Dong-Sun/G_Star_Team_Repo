using UnityEngine;

public class AudioStorage {
    private string soundPath = @"Audio/";    // 사운드를 모아둘 폴더 경로

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
        fourthFloor = Resources.Load<AudioClip>(soundPath + "fourthFloor");
        fifthFloor = Resources.Load<AudioClip>(soundPath + "fifthFloor");
        sixthFloor = Resources.Load<AudioClip>(soundPath + "sixthFloor");

        // 문 여닫는 소리 
        openDoor = Resources.Load<AudioClip>(soundPath + "openDoor");
        closeDoor = Resources.Load<AudioClip>(soundPath + "closeDoor");

        // spike 총알 발사 소리
        bulletFire = Resources.Load<AudioClip>(soundPath + "bulletFire");

        // 플레이어 움직이는 소리
        walk = Resources.Load<AudioClip>(soundPath + "walk");

        // 돌 끄는 소리, 떨어뜨리는 소리(용암, 땅)
        dragRock = Resources.Load<AudioClip>(soundPath + "dragRock");
        fallRockLava = Resources.Load<AudioClip>(soundPath + "fallRockLava");
        fallRockFloor = Resources.Load<AudioClip>(soundPath + "fallRockFloor");

        // 레버 조작 소리, 맵 돌아가는 소리
        switchingLever = Resources.Load<AudioClip>(soundPath + "switchingLever");
        rotateField = Resources.Load<AudioClip>(soundPath + "rotateField");

        // 스테이지 클리어 소리, 플레이어가 죽는 소리
        stageClear = Resources.Load<AudioClip>(soundPath + "stageClear");
        playerDie = Resources.Load<AudioClip>(soundPath + "playerDie");
    }
}
