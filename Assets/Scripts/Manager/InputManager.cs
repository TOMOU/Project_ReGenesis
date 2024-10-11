using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public int count = 0;
    private Stack<UIBase> _uiStack;

    protected override void Init()
    {
        _uiStack = new Stack<UIBase>();
    }

    protected override void Release()
    {
        _uiStack?.Clear();
        _uiStack = null;
    }

    private void Update()
    {
        count = _uiStack.Count;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPopOpenedUI();
        }
    }

    /// <summary>
    /// UI가 열릴 때마다 스택에 추가한다.<br>_uiStack.Count는 언제나 1이다 (각 씬별 기본UI)</br>
    /// </summary>
    /// <param name="ui"></param>
    public void OnAddOpenedUI(UIBase ui)
    {
        _uiStack.Push(ui);
    }

    /// <summary>
    /// Escape키를 눌렀을 때의 동작.<br>기본 UI만 남았을 때는 게임종료 메세지박스를 출력한다.</br><br>그 이외엔 최상단 UI를 Close한다.</br>
    /// </summary>
    private void OnPopOpenedUI()
    {
        // 기본 UI만 남았을 때는 종료
        if (_uiStack.Count <= 1)
        {
            GameManager.Instance.ShowMessage_GameQuit();
            return;
        }

        OnRemoveOpenedUI();
    }

    /// <summary>
    /// 최상단 UI를 Close한다.
    /// </summary>
    public void OnRemoveOpenedUI()
    {
        // 최상단에 Open된 UI를 닫는다.
        UIBase ui = _uiStack.Peek();
        if (ui != null)
        {
            if (_uiStack.Count > 1)
            {
                _uiStack.Pop();
            }

            UIManager.Instance.Close(ui);
        }

        ui = null;
    }

    /// <summary>
    /// 모든 UI를 Close 처리한다.<br>씬 해제 시에 호출한다.</br>
    /// </summary>
    public void OnRemoveOpenedUIAll()
    {
        _uiStack?.Clear();
    }
}
