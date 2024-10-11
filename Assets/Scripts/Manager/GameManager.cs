using UnityEditor;
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

    public void ShowMessage_GameQuit()
    {
        // 7: 게임 종료
        // 8: 게임을 종료하시겠습니까?
        Panel_MessageBox message = UIManager.Instance.Open<Panel_MessageBox>();
        message.ShowMessage(LocalizationManager.Instance.GetString(7), LocalizationManager.Instance.GetString(8), GameQuit, null);
    }

    private void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
