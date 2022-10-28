using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    static private AudioManager instance;
    private string soundPath = @"Assets/Sound/";    // ���带 ��Ƶ� ���� ���
    public AudioManager Instance {
        get { return instance; }
    }

    // �� �� ��� �Ҹ�
    [SerializeField] AudioClip fourthFloor;
    [SerializeField] AudioClip fifthFloor;
    [SerializeField] AudioClip sixthFloor;

    // �� ���ݴ� �Ҹ�
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;

    // spike �Ѿ� �߻� �Ҹ�
    [SerializeField] AudioClip bulletFire;

    // �÷��̾� �����̴� �Ҹ�
    [SerializeField] AudioClip walk;

    // �� ���� �Ҹ�, �������� �Ҹ�(���, ��)
    [SerializeField] AudioClip dragRock;
    [SerializeField] AudioClip fallRockLava;
    [SerializeField] AudioClip fallRockFloor;

    // ���� ���� �Ҹ�, �� ���ư��� �Ҹ�
    [SerializeField] AudioClip switchingLever;
    [SerializeField] AudioClip rotateField;

    // �������� Ŭ���� �Ҹ�, �÷��̾ �״� �Ҹ�
    [SerializeField] AudioClip stageClear;
    [SerializeField] AudioClip playerDie;


    private void Awake() {
        if (instance == null) {
            instance = this;
            LoadAudioClips();
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(this);
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