using UnityEngine;

public class TestScript : MonoBehaviour
{
    private int _index = 1000000;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.Instance.LoadScene("Scene_Title");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.Instance.LoadScene("Scene_Lobby");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.Instance.LoadScene("Scene_Stage");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.Instance.LoadScene("Scene_Arena");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            string str = LocalizationManager.Instance.GetString(_index++);
            if (string.IsNullOrEmpty(str) == false)
            {
                Logger.LogFormat("[{0}] {1}", _index, str);
            }
            else
            {
                Logger.LogWarningFormat("[{0}] string is null", _index);
            }
        }
    }
}
