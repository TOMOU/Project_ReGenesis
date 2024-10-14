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

    /// <summary>
    /// AudioClip을 불러온다.
    /// </summary>
    /// <param name="fileName">음원 파일의 이름</param>
    /// <returns></returns>
    public AudioClip LoadAudioClip(string fileName)
    {
#if UNITY_EDITOR
        AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>($"Assets/AssetBundles/{fileName}.mp3");
#endif

        return clip;
    }

    /// <summary>
    /// UI Prefab을 불러온다.
    /// </summary>
    /// <param name="path">UI Prefab의 경로.<br>ex) UI/Panel/Panel_Title</br></param>
    /// <returns></returns>
    public GameObject LoadUIPrefab(string path)
    {
#if UNITY_EDITOR
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/AssetBundles/{path}.prefab");
#endif

        return prefab;
    }

    /// <summary>
    /// 테이블 CSV파일을 불러온다.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public TextAsset LoadTextAsset(string path)
    {
#if UNITY_EDITOR
        TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/AssetBundles/Table/{path}.csv");
#endif

        return asset;
    }

    public GameObject LoadCharacterSpineModel(string path)
    {
#if UNITY_EDITOR
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/AssetBundles/Character/{path}/SpineModel.prefab");
#endif

        return obj;
    }
}
