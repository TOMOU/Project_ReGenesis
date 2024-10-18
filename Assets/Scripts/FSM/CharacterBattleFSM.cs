using ReGenesis.Enums.Character;
using Spine.Unity;

namespace FSM
{
    public class CharacterBattleFSM : BaseFSM
    {
        private SkeletonAnimation _skeleton;

        /// <summary>
        /// Skeleton 파싱 후 초기화.
        /// </summary>
        public void Initialize()
        {
            if (_skeleton == null)
            {
                _skeleton = GetComponent<SkeletonAnimation>();
            }

            Initialize<StateFSM>(this);
            ChangeState(StateFSM.Idle);
        }

        public void Idle_Enter()
        {
            PlayAnimation("idle", true);
        }

        public void Idle_Exit()
        {
        }

        public void Run_Enter()
        {
            PlayAnimation("run", true);
        }

        public void Run_Exit()
        {
        }

        private void PlayAnimation(string name, bool isLoop = false)
        {
            if (_skeleton != null)
            {
                _skeleton.AnimationState.SetAnimation(0, name, isLoop);
            }
        }
    }
}