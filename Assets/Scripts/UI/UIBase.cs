using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public virtual void Open()
    {
        //Logger.LogFormat("Open {0}", GetType().Name);
        InputManager.Instance.OnAddOpenedUI(this);
    }

    public virtual void Close()
    {
        //Logger.LogFormat("Close {0}", GetType().Name);
        Destroy(gameObject);
    }
}
