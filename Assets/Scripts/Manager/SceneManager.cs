using System.Collections;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    private SceneBase _curScene;

    protected override void Init()
    {

    }

    protected override void Release()
    {

    }

    /// <summary>
    /// 씬 로드를 요청한다.
    /// </summary>
    /// <param name="sceneName">로드할 씬의 이름<br>ex) Scene_Title</br></param>
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

        // 이전 씬 오브젝트가 이미 있다면 해제
        if (_curScene != null)
        {
            _curScene.Release();
            _curScene = null;
        }

        // 씬 로드가 완료되면 씬 오브젝트를 생성
        GameObject sceneObj = new GameObject(sceneName);
        System.Type sceneType = System.Type.GetType(sceneName);
        if (sceneType != null)
        {
            // 부모 루트로 이동
            sceneObj.transform.SetParent(GameManager.Instance.SceneRoot);

            // 컴포넌트를 붙이고 초기화
            var component = sceneObj.AddComponent(sceneType);
            _curScene = component as SceneBase;

            if (_curScene != null)
            {
                _curScene.Initialize();
            }
        }
        else
        {
            Logger.LogErrorFormat("There is no type named [{0}].", sceneName);
            yield break;
        }
    }
}
