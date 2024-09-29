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
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SoundManager.Instance.PlayBGM("Sound/BGM/Test_01");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SoundManager.Instance.PlayBGM("Sound/BGM/Test_02");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SoundManager.Instance.PlaySound("Sound/FX/Test_01");
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
