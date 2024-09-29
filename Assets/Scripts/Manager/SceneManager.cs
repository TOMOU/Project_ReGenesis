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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.Instance.LoadScene("Scene_Title");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.Instance.LoadScene("Scene_Lobby");
        }
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
