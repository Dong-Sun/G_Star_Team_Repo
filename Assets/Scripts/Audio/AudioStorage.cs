using UnityEditor;
using UnityEngine;

public class AudioStorage {
    private string soundPath = @"Assets/Sound/";    // ���带 ��Ƶ� ���� ���

    // �� �� ��� �Ҹ�
    private AudioClip fourthFloor;
    private AudioClip fifthFloor;
    private AudioClip sixthFloor;

    // �� ���ݴ� �Ҹ�
    private AudioClip openDoor;
    private AudioClip closeDoor;

    // spike �Ѿ� �߻� �Ҹ�
    private AudioClip bulletFire;

    // �÷��̾� �����̴� �Ҹ�
    private AudioClip walk;

    // �� ���� �Ҹ�, �������� �Ҹ�(���, ��)
    private AudioClip dragRock;
    private AudioClip fallRockLava;
    private AudioClip fallRockFloor;

    // ���� ���� �Ҹ�, �� ���ư��� �Ҹ�
    private AudioClip switchingLever;
    private AudioClip rotateField;

    // �������� Ŭ���� �Ҹ�, �÷��̾ �״� �Ҹ�
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
    /// ���� ��θ� ���ؼ� ����� Ŭ���� �������� �Լ�
    /// </summary>
    private void LoadAudioClips() {
        // �� �� ��� �Ҹ�
        fourthFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        fifthFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        sixthFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // �� ���ݴ� �Ҹ�
        openDoor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        closeDoor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // spike �Ѿ� �߻� �Ҹ�
        bulletFire = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // �÷��̾� �����̴� �Ҹ�
        walk = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // �� ���� �Ҹ�, �������� �Ҹ�(���, ��)
        dragRock = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        fallRockLava = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        fallRockFloor = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // ���� ���� �Ҹ�, �� ���ư��� �Ҹ�
        switchingLever = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        rotateField = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));

        // �������� Ŭ���� �Ҹ�, �÷��̾ �״� �Ҹ�
        stageClear = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
        playerDie = (AudioClip)AssetDatabase.LoadAssetAtPath(soundPath + "testAudio.wav", typeof(AudioClip));
    }
}
