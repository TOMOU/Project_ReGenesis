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
        // 게임의 초기 세팅
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Transform Root를 캐싱.
        ManagerRoot = transform.Find("Manager");
        SceneRoot = transform.Find("Scene");
        UICanvasRoot = transform.Find("UI/UICanvas");

        // 첫 시작부터 미리 로드할 Manager들을 불러온다.
        LoadManager();

        // 초기 세팅동작이 완료되었으니 Logo 화면으로 이동.
        SceneManager.Instance.LoadScene("Scene_Logo");
    }

    protected override void Release()
    {

    }

    /// <summary>
    /// 첫 시작부터 사용될 Manager 클래스를 미리 로드한다.
    /// </summary>
    private void LoadManager()
    {
        SceneManager sceneManager = SceneManager.Instance;
        InputManager inputManager = InputManager.Instance;
        UIManager uiManager = UIManager.Instance;
        ResourceManager resourceManager = ResourceManager.Instance;
        SoundManager soundManager = SoundManager.Instance;
    }

    /// <summary>
    /// 게임 종료 메세지를 출력한다.
    /// </summary>
    public void ShowMessage_GameQuit()
    {
        // 7: 게임 종료
        // 8: 게임을 종료하시겠습니까?
        Panel_MessageBox message = UIManager.Instance.Open<Panel_MessageBox>();
        message.ShowMessage(LocalizationManager.Instance.GetString(7), LocalizationManager.Instance.GetString(8), GameQuit, null);
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    private void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
