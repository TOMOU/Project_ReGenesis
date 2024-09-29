using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    protected override void Init()
    {

    }

    protected override void Release()
    {

    }

    public AudioClip LoadAudioClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
