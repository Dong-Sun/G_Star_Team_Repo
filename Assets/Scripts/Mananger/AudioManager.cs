using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    static private AudioManager instance;
    private string soundPath = @"Assets/Sound/";    // 사운드를 모아둘 폴더 경로
    public AudioManager Instance {
        get { return instance; }
    }

    // 각 층 배경 소리
    [SerializeField] AudioClip fourthFloor;
    [SerializeField] AudioClip fifthFloor;
    [SerializeField] AudioClip sixthFloor;

    // 문 여닫는 소리
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;

    // spike 총알 발사 소리
    [SerializeField] AudioClip bulletFire;

    // 플레이어 움직이는 소리
    [SerializeField] AudioClip walk;

    // 돌 끄는 소리, 떨어지는 소리(용암, 땅)
    [SerializeField] AudioClip dragRock;
    [SerializeField] AudioClip fallRockLava;
    [SerializeField] AudioClip fallRockFloor;

    // 레버 조작 소리, 맵 돌아가는 소리
    [SerializeField] AudioClip switchingLever;
    [SerializeField] AudioClip rotateField;

    // 스테이지 클리어 소리, 플레이어가 죽는 소리
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