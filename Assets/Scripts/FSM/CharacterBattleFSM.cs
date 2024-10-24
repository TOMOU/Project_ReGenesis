using ReGenesis.Enums.Character;
using Spine.Unity;
using System;

namespace FSM
{
    public class CharacterBattleFSM : BaseFSM
    {
        public SkeletonAnimation skeleton;

        // callback list
        public Action cbIdle;
        public Action cbRun;

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
                cbIdle.Invoke();
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
                cbRun.Invoke();
            }
        }

        public void Run_Exit()
        {
        }

        private void PlayAnimation(string name, bool isLoop = false)
        {
            if (skeleton != null)
            {
                skeleton.AnimationState.SetAnimation(0, name, isLoop);
            }
        }
    }
}