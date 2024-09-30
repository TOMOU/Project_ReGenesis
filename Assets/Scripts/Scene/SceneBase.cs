using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    public virtual void Initialize() { }

    public virtual void Release()
    {
        Destroy(gameObject);
    }
}
