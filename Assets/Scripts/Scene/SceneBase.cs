using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    public virtual void Initialize()
    {
        //Logger.LogFormat("Initialize {0}", GetType().Name);
    }

    public virtual void Release()
    {
        //Logger.LogFormat("Release {0}", GetType().Name);

        // 로드되었던 UI 삭제
        UIManager.Instance.CloseAll();

        // Escape키에 연결되었던 UI 리스트 삭제
        InputManager.Instance.OnRemoveOpenedUIAll();

        // Scene 오브젝트 삭제
        Destroy(gameObject);
    }
}
