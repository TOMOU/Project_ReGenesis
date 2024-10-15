using ReGenesis.Enums.Character;
using System.Collections;
using UnityEngine;

namespace FSM
{
    public class CharacterBattleFSM : BaseFSM
    {
        private void Awake()
        {
            Initialize<StateFSM>(this);
            ChangeState(StateFSM.Idle);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                ChangeState(StateFSM.Idle);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                ChangeState(StateFSM.Run);
            }
        }

        public IEnumerator Idle_Enter()
        {
            Logger.Log("Idle_Enter");
            yield return new WaitForSeconds(10f);
            Logger.Log("Idle_Enter_Fin");
        }

        public IEnumerator Idle_Exit()
        {
            Logger.Log("Idle_Exit");
            yield return new WaitForSeconds(2f);
            Logger.Log("Idle_Exit_Fin");
        }

        public IEnumerator Run_Enter()
        {
            Logger.Log("Run_Enter");
            yield return new WaitForSeconds(10f);
            Logger.Log("Run_Enter_Fin");
        }

        public IEnumerator Run_Exit()
        {
            Logger.Log("Run_Exit");
            yield return new WaitForSeconds(2f);
            Logger.Log("Run_Exit_Fin");
        }
    }
}