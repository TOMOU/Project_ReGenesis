using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private class ClipCache
    {
        public string Name { get; }
        public SoundType Type { get; }
        public AudioClip Clip { get; }
        public float Volume { get; }
        public bool IsLoop { get; }

        public ClipCache(string name, SoundType type, AudioClip clip, float volume, bool isLoop)
        {
            Name = name;
            Type = type;
            Clip = clip;
            Volume = volume;
            IsLoop = isLoop;
        }
    }

    private Dictionary<int, ClipCache> _clipDic;
    private List<AudioSource> _sourceList;
    private AudioSource _curBGM;

    protected override void Init()
    {
        _clipDic = new Dictionary<int, ClipCache>();
        var table = TableManager.Instance.GetTable<SoundTable>();
        if (table != null)
        {
            foreach (var data in table.Table)
            {
                int index = data.Index;
                SoundType type = data.Type;
                string name = data.Name;
                string filePath = data.FilePath;
                float volume = data.Volume;
                bool isLoop = data.IsLoop;

                AudioClip clip = ResourceManager.Instance.LoadAudioClip(filePath);
                if (clip != null)
                {
                    _clipDic.Add(index, new ClipCache(name, type, clip, volume, isLoop));
                }
            }
        }

        _sourceList = new List<AudioSource>();
    }

    protected override void Release()
    {
        _clipDic?.Clear();
        _clipDic = null;

        _sourceList?.Clear();
        _sourceList = null;
    }

    public void PlayBGM(int index)
    {
        if (_curBGM?.isPlaying == true)
        {
            _curBGM.Stop();
        }

        _curBGM = GetOrCreateAudioSource();
        PlayAudio(index, _curBGM);
    }

    public void PlaySound(int index)
    {
        var source = GetOrCreateAudioSource();
        PlayAudio(index, source);
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

    private void PlayAudio(int index, AudioSource source)
    {
        if (_clipDic.TryGetValue(index, out var cache))
        {
            source.clip = cache.Clip;
            source.volume = cache.Volume;
            source.loop = cache.IsLoop;
            source.Play();
        }
    }
}
