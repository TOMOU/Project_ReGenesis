using ReGenesis.Enums.Character;
using Spine.Unity;
using System;
using System.Collections;

namespace FSM
{
    public class CharacterBattleFSM : BaseFSM
    {
        public SkeletonAnimation skeleton;

        // callback list
        public Action cbIdle = null;
        public Action cbRun = null;
        public Action cbAttack = null;
        public Action cbSkill_0 = null;
        public Action cbSkill_1 = null;
        public Action cbSkill_2 = null;
        public Action cbStun = null;
        public Action cbVictory = null;
        public Action cbDie = null;

        /// <summary>
        /// Skeleton 파싱 후 초기화.
        /// </summary>
        public void Initialize()
        {
            if (skeleton == null)
            {
                skeleton = GetComponent<SkeletonAnimation>();
            }

            Initialize<StateFSM>(this);
            ChangeState(StateFSM.Idle);
        }

        public void Idle_Enter()
        {
            PlayAnimation("idle", true);
        }

        public void Idle_Update()
        {
            if (cbIdle != null)
            {
                cbIdle();
            }
        }

        public void Idle_Exit()
        {
        }

        public void Run_Enter()
        {
            PlayAnimation("run", true);
        }

        public void Run_Update()
        {
            if (cbRun != null)
            {
                cbRun();
            }
        }

        public void Run_Exit()
        {
        }

        public IEnumerator Attack_Enter()
        {
            PlayAnimation("attack");

            yield return new WaitForSpineEvent(skeleton, "attack");
        }

        public void Attack_Update()
        {
            if (cbAttack != null)
            {
                cbAttack();
            }
        }

        public void Attack_Exit()
        {

        }

        public void Skill0_Enter()
        {
            PlayAnimation("skill3");
        }

        public void Skill0_Update()
        {
            if (cbSkill_0 != null)
            {
                cbSkill_0();
            }
        }

        public void Skill0_Exit() { }

        public void Skill1_Enter()
        {
            PlayAnimation("skill1");
        }

        public void Skill1_Update()
        {
            if (cbSkill_1 != null)
            {
                cbSkill_1();
            }
        }

        public void Skill1_Exit() { }

        public void Skill2_Enter()
        {
            PlayAnimation("skill2");
        }

        public void Skill2_Update()
        {
            if (cbSkill_2 != null)
            {
                cbSkill_2();
            }
        }

        public void Skill2_Exit() { }

        public void Stun_Enter() { }

        public void Stun_Update()
        {
            if (cbStun != null)
            {
                cbStun();
            }
        }

        public void Stun_Exit() { }

        public void Victory_Enter()
        {
            PlayAnimation("victory");
        }

        public void Victory_Update()
        {
            if (cbVictory != null)
            {
                cbVictory();
            }
        }

        public void Victory_Exit() { }

        public void Die_Enter()
        {
            PlayAnimation("die");
        }

        public void Die_Update()
        {
            if (cbDie != null)
            {
                cbDie();
            }
        }

        public void Die_Exit() { }

        private void PlayAnimation(string name, bool isLoop = false)
        {
            if (skeleton != null)
            {
                skeleton.AnimationState.SetAnimation(0, name, isLoop);
            }
        }
    }
}