using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCharacter();
            ShowCharacter();
        }
    }

    private void AddCharacter()
    {
        var table = TableManager.Instance.GetTable<CharacterStatusTable>();
        if (table != null)
        {
            foreach (var character in table.Table)
            {
                BattleManager.Instance.AddCharacter(new ReGenesis.Info.Character(1, 1, character), true);
            }
        }
    }

    private void ShowCharacter()
    {
        var list = BattleManager.Instance.GetCharacter();
        foreach (var c in list)
        {
            Logger.LogFormat("grade:{0}\nlevel:{1}\nname:{2}\nhp:{3}\natk:{4}\ndef:{5}\nmag:{6}", c.grade, c.level, c.name, c.hp, c.atk, c.def, c.mag);
        }
    }
}
