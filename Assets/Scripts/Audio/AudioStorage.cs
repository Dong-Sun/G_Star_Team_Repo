using UnityEngine;

public class AudioStorage {
    private string soundPath = @"Audio/";    // ���带 ��Ƶ� ���� ���

    // �� �� ��� �Ҹ�
    private AudioClip startSceneBGM;
    private AudioClip thirdFloor;
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

    // ���� ���� �Ҹ�, ���� �Դ� �Ҹ�
    private AudioClip createKey;
    private AudioClip getKey;

    // �������� Ŭ���� �Ҹ�, �÷��̾ �״� �Ҹ�
    private AudioClip stageClear;
    private AudioClip playerDie;

    // ���� ��ġ�� �Ҹ�, ���� ������ �Ҹ�
    private AudioClip paperOpen;
    private AudioClip paperClose;

    // ��Ż �Ҹ�
    private AudioClip portal;

    // Property ReadOnly
    public AudioClip StartSceneBGM { get => startSceneBGM; }
    public AudioClip ThirdFloor { get => thirdFloor; }
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
    public AudioClip CreateKey { get => createKey; }
    public AudioClip GetKey { get => getKey; }
    public AudioClip StageClear { get => stageClear; }
    public AudioClip PlayerDie { get => playerDie; }
    public AudioClip PaperOpen { get => paperOpen; }
    public AudioClip PaperClose { get => paperClose; }
    public AudioClip Portal { get => portal; }

    public AudioStorage() {
        LoadAudioClips();
    }

    /// <summary>
    /// ���� ��θ� ���ؼ� ����� Ŭ���� �������� �Լ�
    /// </summary>
    private void LoadAudioClips() {
        // �� �� ��� �Ҹ�
        startSceneBGM = Resources.Load<AudioClip>(soundPath + "startSceneBGM");
        thirdFloor = Resources.Load<AudioClip>(soundPath + "thirdFloor");
        fourthFloor = Resources.Load<AudioClip>(soundPath + "fourthFloor");
        fifthFloor = Resources.Load<AudioClip>(soundPath + "fifthFloor");
        sixthFloor = Resources.Load<AudioClip>(soundPath + "sixthFloor");

        // �� ���ݴ� �Ҹ� 
        openDoor = Resources.Load<AudioClip>(soundPath + "openDoor");
        closeDoor = Resources.Load<AudioClip>(soundPath + "closeDoor");

        // spike �Ѿ� �߻� �Ҹ�
        bulletFire = Resources.Load<AudioClip>(soundPath + "bulletFire");

        // �÷��̾� �����̴� �Ҹ�
        walk = Resources.Load<AudioClip>(soundPath + "walk");

        // �� ���� �Ҹ�, ����߸��� �Ҹ�(���, ��)
        dragRock = Resources.Load<AudioClip>(soundPath + "dragRock");
        fallRockLava = Resources.Load<AudioClip>(soundPath + "fallRockLava");
        fallRockFloor = Resources.Load<AudioClip>(soundPath + "fallRockFloor");

        // ���� ���� �Ҹ�, �� ���ư��� �Ҹ�
        switchingLever = Resources.Load<AudioClip>(soundPath + "switchingLever");
        rotateField = Resources.Load<AudioClip>(soundPath + "rotateField");

        // ���� ���� �Ҹ�, ���� �Դ� �Ҹ�
        createKey = Resources.Load<AudioClip>(soundPath + "createKey");
        getKey = Resources.Load<AudioClip>(soundPath + "getKey");

        // �������� Ŭ���� �Ҹ�, �÷��̾ �״� �Ҹ�
        stageClear = Resources.Load<AudioClip>(soundPath + "stageClear");
        playerDie = Resources.Load<AudioClip>(soundPath + "playerDie");

        // ���� ��ġ�� �Ҹ�, ���� ������ �Ҹ�
        paperOpen = Resources.Load<AudioClip>(soundPath + "paperOpen");
        paperClose = Resources.Load<AudioClip>(soundPath + "paperClose");

        // ��Ż �Ҹ�
        portal = Resources.Load<AudioClip>(soundPath + "portal");
    }
}