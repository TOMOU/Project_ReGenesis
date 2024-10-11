using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MessageBox : UIBase
{
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textMessage;
    [SerializeField] private Button _btnCancel;
    [SerializeField] private Button _btnConfirm;
    private Action _callbackConfirm;
    private Action _callbackCancel;

    public override void Open()
    {
        base.Open();

        _btnConfirm.onClick.AddListener(OnClickConfirm);
        _btnCancel.onClick.AddListener(OnClickCancel);
    }

    public override void Close()
    {
        base.Close();
    }

    /// <summary>
    /// 메세지박스에 텍스트를 넣어 출력한다.
    /// </summary>
    /// <param name="title">제목</param>
    /// <param name="message">메세지 내용</param>
    /// <param name="callbackConfirm">'확인'버튼 터치 시 동작</param>
    /// <param name="callbackCancel">'취소'버튼 터치 시 동작</param>
    public void ShowMessage(string title, string message, Action callbackConfirm, Action callbackCancel)
    {
        _textTitle.text = title;
        _textMessage.text = message;
        _callbackConfirm = callbackConfirm; ;
        _callbackCancel = callbackCancel;
    }

    /// <summary>
    /// Confirm 버튼을 눌렀을 때의 동작.
    /// </summary>
    private void OnClickConfirm()
    {
        if (_callbackConfirm != null)
        {
            _callbackConfirm();
        }

        InputManager.Instance.OnRemoveOpenedUI();
    }

    /// <summary>
    /// Cancel 버튼을 눌렀을 때의 동작.
    /// </summary>
    private void OnClickCancel()
    {
        if (_callbackCancel != null)
        {
            _callbackCancel();
        }

        InputManager.Instance.OnRemoveOpenedUI();
    }
}
