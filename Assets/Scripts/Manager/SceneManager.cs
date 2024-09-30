using System.Collections;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    protected override void Init()
    {

    }

    protected override void Release()
    {

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(coLoadScene(sceneName));
    }

    private IEnumerator coLoadScene(string sceneName)
    {
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (async.isDone == false)
        {
            yield return null;
        }

        Logger.Log($"Complete load {sceneName}.");
    }
}
