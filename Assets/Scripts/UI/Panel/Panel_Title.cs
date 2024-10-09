using UnityEngine;
using UnityEngine.UI;

public class Panel_Title : UIBase
{
    [SerializeField] private Button _touchButton;

    public override void Open()
    {
        base.Open();

        _touchButton?.onClick.AddListener(OnClickTitle);
    }

    public override void Close()
    {
        base.Close();

        _touchButton?.onClick.RemoveAllListeners();
    }

    private void OnClickTitle()
    {
        // 먼저 테이블 등의 로드작업이 필요.
        TableManager.Instance.LoadTable();
        SoundManager.Instance.LoadTable();
        LocalizationManager.Instance.LoadTable();

        // 완료되면 Scene_Lobby를 불러온다.
        SceneManager.Instance.LoadScene("Scene_Lobby");
    }
}
