using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Lobby : UIBase
{
    [Header("- 플레이어 정보 (좌상단)")]
    [SerializeField] private TextMeshProUGUI _textPlayerLevel;
    [SerializeField] private TextMeshProUGUI _textPlayerName;
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
    [SerializeField] private Button _btnMail;
    [SerializeField] private Button _btnSetting;

    public override void Open()
    {
        base.Open();

        InitButton();
        Refresh();
    }

    public override void Close()
    {
        base.Close();

        ReleaseButton();
    }

    private void InitButton()
    {
        _btnNotice.onClick.AddListener(OnClickNotice);
        _btnQuest.onClick.AddListener(OnClickQuest);
        _btnDictionary.onClick.AddListener(OnClickDictionary);
        _btnShop.onClick.AddListener(OnClickShop);

        _btnCharacter.onClick.AddListener(OnClickCharacter);
        _btnInventory.onClick.AddListener(OnClickInventory);
        _btnStorybook.onClick.AddListener(OnClickStorybook);
        _btnGuild.onClick.AddListener(OnClickGuild);
        _btnGacha.onClick.AddListener(OnClickGacha);

        _btnWorld.onClick.AddListener(OnClickWorld);

        _btnMail.onClick.AddListener(OnClickMail);
        _btnSetting.onClick.AddListener(OnClickSetting);
    }

    private void ReleaseButton()
    {
        _btnNotice?.onClick.RemoveAllListeners();
        _btnQuest?.onClick.RemoveAllListeners();
        _btnDictionary?.onClick.RemoveAllListeners();
        _btnShop?.onClick.RemoveAllListeners();

        _btnCharacter?.onClick.RemoveAllListeners();
        _btnInventory?.onClick.RemoveAllListeners();
        _btnStorybook?.onClick.RemoveAllListeners();
        _btnGuild?.onClick.RemoveAllListeners();
        _btnGacha?.onClick.RemoveAllListeners();

        _btnWorld?.onClick.RemoveAllListeners();

        _btnMail?.onClick.RemoveAllListeners();
        _btnSetting?.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Refresh()
    {
        _textPlayerLevel.text = "1";
        _textPlayerName.text = "토모우";

        int curExp = 103;
        int maxExp = 1000;
        _imgPlayerExpSlider.fillAmount = curExp / (float)maxExp;
        _textPlayerExpValue.text = string.Format("{0}/{1}", curExp, maxExp);

        int curStamina = 53;
        int maxStamina = 65;
        _textStaminaValue.text = string.Format("{0}/{1}", curStamina, maxStamina);

        int curGold = 30000;
        _textGoldValue.text = string.Format("{0:##,##0}", curGold);

        int curCash = 0;
        _textCashValue.text = string.Format("{0:##,##0}", curCash);
    }

    private void OnClickNotice()
    {
        UIManager.Instance.Open<Panel_Notice>();
    }

    private void OnClickQuest()
    {
        UIManager.Instance.Open<Panel_Quest>();
    }

    private void OnClickDictionary()
    {
        UIManager.Instance.Open<Panel_Dictionary>();
    }

    private void OnClickShop()
    {
        UIManager.Instance.Open<Panel_Shop>();
    }

    private void OnClickCharacter()
    {
        UIManager.Instance.Open<Panel_Character>();
    }

    private void OnClickInventory()
    {
        UIManager.Instance.Open<Panel_Inventory>();
    }

    private void OnClickStorybook()
    {
        UIManager.Instance.Open<Panel_StoryBook>();
    }

    private void OnClickGuild()
    {
        UIManager.Instance.Open<Panel_Guild>();
    }

    private void OnClickGacha()
    {
        UIManager.Instance.Open<Panel_Gacha>();
    }

    private void OnClickWorld()
    {
        UIManager.Instance.Open<Panel_World>();
    }

    private void OnClickMail()
    {
        UIManager.Instance.Open<Panel_Mail>();
    }

    private void OnClickSetting()
    {
        UIManager.Instance.Open<Panel_Setting>();
    }
}
