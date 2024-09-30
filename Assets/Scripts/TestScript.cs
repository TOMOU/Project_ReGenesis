using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 디버그
        GameManager gm = GameManager.Instance;
        SoundManager sm = SoundManager.Instance;
        LocalizationManager lm = LocalizationManager.Instance;
        NotificationManager nm = NotificationManager.Instance;
        TutorialManager tm = TutorialManager.Instance;
    }
}
