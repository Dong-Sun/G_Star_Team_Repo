using UnityEngine;

public class AudioStorage {
    private string soundPath = @"Sound/";    // ���带 ��Ƶ� ���� ���

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
        fourthFloor = Resources.Load<AudioClip>(soundPath + "fourthFloor.mp3");
        fifthFloor = Resources.Load<AudioClip>(soundPath + "fifthFloor.mp3");
        sixthFloor = Resources.Load<AudioClip>(soundPath + "sixthFloor.mp3");

        // �� ���ݴ� �Ҹ� 
        openDoor = Resources.Load<AudioClip>(soundPath + "openDoor.wav");
        closeDoor = Resources.Load<AudioClip>(soundPath + "closeDoor.wav");

        // spike �Ѿ� �߻� �Ҹ�
        bulletFire = Resources.Load<AudioClip>(soundPath + "bulletFire.wav");

        // �÷��̾� �����̴� �Ҹ�
        walk = Resources.Load<AudioClip>(soundPath + "walk.mp3");

        // �� ���� �Ҹ�, ����߸��� �Ҹ�(���, ��)
        dragRock = Resources.Load<AudioClip>(soundPath + "dragRock.mp3");
        fallRockLava = Resources.Load<AudioClip>(soundPath + "fallRockLava.mp3");
        fallRockFloor = Resources.Load<AudioClip>(soundPath + "fallRockFloor.mp3");

        // ���� ���� �Ҹ�, �� ���ư��� �Ҹ�
        switchingLever = Resources.Load<AudioClip>(soundPath + "switchingLever.mp3");
        rotateField = Resources.Load<AudioClip>(soundPath + "rotateField.mp3");

        // �������� Ŭ���� �Ҹ�, �÷��̾ �״� �Ҹ�
        stageClear = Resources.Load<AudioClip>(soundPath + "stageClear.mp3");
        playerDie = Resources.Load<AudioClip>(soundPath + "playerDie.mp3");
    }
}
