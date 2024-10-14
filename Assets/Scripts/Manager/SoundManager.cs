using ReGenesis.Enums.Sound;
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
        _sourceList = new List<AudioSource>();
    }

    protected override void Release()
    {
        _clipDic?.Clear();
        _clipDic = null;

        _sourceList?.Clear();
        _sourceList = null;
    }

    /// <summary>
    /// 테이블을 로드한다.<br>타이틀 까지는 에셋번들 등을 로드하지 않기 때문에...</br>
    /// </summary>
    public void LoadTable()
    {
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
    }

    /// <summary>
    /// 재생경로에 기반한 BGM을 재생한다.<br>주로 Resources 폴더 내의 BGM을 재생할 때 사용한다.</br>
    /// </summary>
    /// <param name="path">재생할 BGM의 경로<br>ex) Sound/BGM/Eternal Light</br></param>
    public void PlayBGM(string path)
    {
        if (_curBGM?.isPlaying == true)
        {
            _curBGM.Stop();
        }

        _curBGM = GetOrCreateAudioSource();

        // Title에서는 리소스를 번들이 아닌 다이렉트로 불러온다.
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip != null)
        {
            _curBGM.clip = clip;
            _curBGM.volume = 1f;
            _curBGM.loop = true;
            _curBGM.Play();
        }
    }

    /// <summary>
    /// index값에 기반한 BGM을 재생한다.
    /// </summary>
    /// <param name="index">재생할 BGM의 index</param>
    public void PlayBGM(int index)
    {
        if (_curBGM?.isPlaying == true)
        {
            _curBGM.Stop();
        }

        _curBGM = GetOrCreateAudioSource();
        PlayAudio(index, _curBGM);
    }

    /// <summary>
    /// index값에 기반한 일회성 사운드를 재생한다.
    /// </summary>
    /// <param name="index"></param>
    public void PlaySound(int index)
    {
        var source = GetOrCreateAudioSource();
        PlayAudio(index, source);
    }

    /// <summary>
    /// SoundManager에 풀링된 미사용되는 AudioSource를 찾고, 없다면 새로 추가.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 풀링된 사운드 리소스를 찾아 재생한다.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="source"></param>
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
