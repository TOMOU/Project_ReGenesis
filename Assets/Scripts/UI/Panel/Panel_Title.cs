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
        SceneManager.Instance.LoadScene("Scene_Lobby");
    }
}
