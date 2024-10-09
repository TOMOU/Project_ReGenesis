using TMPro;
using UnityEngine;

public class UILocalization : MonoBehaviour
{
    [SerializeField] private int _index;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();

        if (_text == null)
        {
            return;
        }

        RefreshText();
    }

    private void RefreshText()
    {
        _text.text = LocalizationManager.Instance.GetString(_index);
        Logger.Log(LocalizationManager.Instance.GetString(_index));
    }
}
