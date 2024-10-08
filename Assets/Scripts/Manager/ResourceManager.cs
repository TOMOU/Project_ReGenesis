using UnityEditor;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    protected override void Init()
    {

    }

    protected override void Release()
    {

    }

    public AudioClip LoadAudioClip(string fileName)
    {
#if UNITY_EDITOR
        AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>($"Assets/AssetBundles/{fileName}.mp3");
#endif

        return clip;
    }

    public GameObject LoadUIPrefab(string path)
    {
#if UNITY_EDITOR
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/AssetBundles/{path}.prefab");
#endif

        return prefab;
    }

    public TextAsset LoadTextAsset(string path)
    {
#if UNITY_EDITOR
        TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/AssetBundles/Table/{path}.csv");
#endif

        return asset;
    }
}
