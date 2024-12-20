using UnityEngine;

public class Panel_Logo : UIBase
{
    [SerializeField] private Animation _ciAnimation;

    public override void Open()
    {
        base.Open();

        if (_ciAnimation == null)
        {
            Logger.LogError("CI Logo Animation이 캐싱되지 않았습니다.");
            return;
        }
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEventAnimationEnd()
    {
        SceneManager.Instance.LoadScene("Scene_Title");
    }
}
