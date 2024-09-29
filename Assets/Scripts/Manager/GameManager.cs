using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform ManagerRoot { get; private set; }
    public Transform SceneRoot { get; private set; }
    public Transform UICanvasRoot { get; private set; }

    private void Awake()
    {
        GameManager gm = GameManager.Instance;
        DontDestroyOnLoad(this);
    }

    protected override void Init()
    {
        ManagerRoot = transform.Find("Manager");
        SceneRoot = transform.Find("Scene");
        UICanvasRoot = transform.Find("UI/UICanvas");

        // 타이틀 씬 로드
        SceneManager.Instance.LoadScene("Scene_Title");
    }

    protected override void Release()
    {

    }
}
