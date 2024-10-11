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

        // Global로 사용할 Manager 로드
        LoadManager();

        // CI 로고 씬 로드
        SceneManager.Instance.LoadScene("Scene_Logo");

        // 테스트 게임오브젝트 생성
        GameObject testObj = new GameObject("TestScript");
        testObj.AddComponent<TestScript>();
        DontDestroyOnLoad(testObj);
    }

    protected override void Release()
    {

    }

    private void LoadManager()
    {
        SceneManager sceneManager = SceneManager.Instance;
        InputManager inputManager = InputManager.Instance;
        UIManager uiManager = UIManager.Instance;
        ResourceManager resourceManager = ResourceManager.Instance;
        SoundManager soundManager = SoundManager.Instance;
    }

    public void Quit()
    {
        Logger.Log("Exit Game?");
    }
}
