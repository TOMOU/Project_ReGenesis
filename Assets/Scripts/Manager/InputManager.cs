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


    public void OnAddOpenedUI(UIBase ui)
    {
        _uiStack.Push(ui);
    }

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

    public void OnRemoveOpenedUIAll()
    {
        _uiStack?.Clear();
    }
}
