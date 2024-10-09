using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Lobby : UIBase
{
    [Header("- 플레이어 정보 (좌상단)")]
    [SerializeField] private TextMeshProUGUI _textPlayerLevel;
    [SerializeField] private Image _imgPlayerExpSlider;
    [SerializeField] private TextMeshProUGUI _textPlayerExpValue;

    [Header("- 기능 컨텐츠 메뉴 (좌상단)")]
    [SerializeField] private Button _btnNotice;
    [SerializeField] private Button _btnQuest;
    [SerializeField] private Button _btnDictionary;
    [SerializeField] private Button _btnShop;

    [Header("- 기능 컨텐츠 메뉴 (좌하단)")]
    [SerializeField] private Button _btnCharacter;
    [SerializeField] private Button _btnInventory;
    [SerializeField] private Button _btnStorybook;
    [SerializeField] private Button _btnGuild;
    [SerializeField] private Button _btnGacha;

    [Header("- 메인 컨텐츠 메뉴 (우하단)")]
    [SerializeField] private Button _btnWorld;

    [Header("- 재화 목록 (상단)")]
    [SerializeField] private TextMeshProUGUI _textStaminaValue;
    [SerializeField] private TextMeshProUGUI _textGoldValue;
    [SerializeField] private TextMeshProUGUI _textCashValue;

    [Header("- 옵션 버튼 (우상단)")]
    [SerializeField] private Button _btnPost;
    [SerializeField] private Button _btnOption;

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
