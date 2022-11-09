using UnityEngine;
using System.Collections.Generic;

public class AudioStorage {
    private string soundPath = @"Audio/";    // 사운드를 모아둘 폴더 경로
    private Dictionary<string, AudioClip> soundTrack = new Dictionary<string, AudioClip>();
    private AudioClip[] sounds;

    public AudioStorage() {
        LoadAudioClips();
    }

    public AudioClip GetSoundTrack(string name) {
        return soundTrack[name];
    }

    /// <summary>
    /// 파일 경로를 통해서 오디오 클립을 가져오는 함수
    /// </summary>
    private void LoadAudioClips() {
        sounds = Resources.LoadAll<AudioClip>(soundPath);
        foreach (AudioClip s in sounds) {
            soundTrack.Add(s.name, s);
        }
    }
}