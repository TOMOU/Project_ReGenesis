using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    obj.transform.SetParent(GameManager.Instance.ManagerRoot);
                    _instance = obj.AddComponent<T>();

                    //Logger.Log($"Complete initialize {typeof(T).Name}");
                }

                _instance.Init();
            }

            return _instance;
        }
    }

    protected virtual void Init() { }

    protected virtual void Release() { }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            Release();
            _instance = null;

            //Logger.Log($"Complete destroy {typeof(T).Name}");
        }
    }
}
