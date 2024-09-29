using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private class ClipCache
    {
        public AudioClip Clip { get; }
        public float Volume { get; }
        public bool IsLoop { get; }

        public ClipCache(AudioClip clip, float volume, bool isLoop)
        {
            Clip = clip;
            Volume = volume;
            IsLoop = isLoop;
        }
    }

    private Dictionary<string, ClipCache> _clipDic;
    private List<AudioSource> _sourceList;
    private AudioSource _curBGM;

    protected override void Init()
    {
        _clipDic = new Dictionary<string, ClipCache>
        {
            { "Sound/BGM/Test_01", new ClipCache(ResourceManager.Instance.LoadAudioClip("Sound/BGM/Test_01"), 1f, true) },
            { "Sound/BGM/Test_02", new ClipCache(ResourceManager.Instance.LoadAudioClip("Sound/BGM/Test_02"), 1f, true) },
            { "Sound/FX/Test_01", new ClipCache(ResourceManager.Instance.LoadAudioClip("Sound/FX/Test_01"), 1f, false) }
        };

        _sourceList = new List<AudioSource>();
    }

    protected override void Release()
    {
        _clipDic?.Clear();
        _clipDic = null;

        _sourceList?.Clear();
        _sourceList = null;
    }

    public void PlayBGM(string bgmName)
    {
        if (_curBGM?.isPlaying == true)
        {
            _curBGM.Stop();
        }

        _curBGM = GetOrCreateAudioSource();
        PlayAudio(bgmName, _curBGM);
    }

    public void PlaySound(string soundName)
    {
        var source = GetOrCreateAudioSource();
        PlayAudio(soundName, source);
    }

    private AudioSource GetOrCreateAudioSource()
    {
        var source = _sourceList.Find(e => !e.isPlaying);
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _sourceList.Add(source);
        }
        return source;
    }

    private void PlayAudio(string audioName, AudioSource source)
    {
        if (_clipDic.TryGetValue(audioName, out var cache))
        {
            source.clip = cache.Clip;
            source.volume = cache.Volume;
            source.loop = cache.IsLoop;
            source.Play();
        }
    }
}
