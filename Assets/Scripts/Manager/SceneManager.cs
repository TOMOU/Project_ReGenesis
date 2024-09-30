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

    private int _bgmIndex = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _bgmIndex--;
            if (_bgmIndex < 0)
            {
                _bgmIndex = 15;
            }

            SoundManager.Instance.PlayBGM(_bgmIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _bgmIndex++;
            if (_bgmIndex > 15)
            {
                _bgmIndex = 0;
            }

            SoundManager.Instance.PlayBGM(_bgmIndex);
        }
    }
}
