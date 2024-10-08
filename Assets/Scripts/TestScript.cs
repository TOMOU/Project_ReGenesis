using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowUnitStatus();
        }
    }

    private void ShowUnitStatus()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < 5; i++)
        {
            UnitStatusData statusData = GetStatusData(i);
            sb.AppendLineFormat("{0}", statusData.Name);
            for (int grade = 1; grade <= 5; grade++)
            {
                for (int level = 1; level <= 10; level++)
                {
                    int hp = statusData.GetHP(grade, level);
                    int atk = statusData.GetATK(grade, level);
                    int def = statusData.GetDEF(grade, level);
                    int mag = statusData.GetMAG(grade, level);
                    sb.AppendLineFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", grade, level, hp, atk, def, mag);
                }
            }

            sb.AppendLine();
        }

        GUIUtility.systemCopyBuffer = sb.ToString();
    }

    private UnitStatusData GetStatusData(int index)
    {
        var table = TableManager.Instance.GetTable<UnitStatusTable>();
        if (table != null)
        {
            return table.GetData(index);
        }

        return null;
    }
}
