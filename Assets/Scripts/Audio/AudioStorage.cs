using UnityEngine;
using System.Collections.Generic;

public class AudioStorage {
    private string soundPath = @"Audio/";    // ���带 ��Ƶ� ���� ���
    private Dictionary<string, AudioClip> soundTrack = new Dictionary<string, AudioClip>();
    private AudioClip[] sounds;

    public AudioStorage() {
        LoadAudioClips();
    }

    public AudioClip GetSoundTrack(string name) {
        return soundTrack[name];
    }

    /// <summary>
    /// ���� ��θ� ���ؼ� ����� Ŭ���� �������� �Լ�
    /// </summary>
    private void LoadAudioClips() {
        sounds = Resources.LoadAll<AudioClip>(soundPath);
        foreach (AudioClip s in sounds) {
            soundTrack.Add(s.name, s);
        }
    }
}