using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FSM
{
    public class StateMap
    {
        public Enum State { get; private set; }
        public Func<IEnumerator> Enter = DoNothingCoroutine;
        public Func<IEnumerator> Exit = DoNothingCoroutine;
        public Action Update = DoNothing;

        public StateMap(Enum state)
        {
            this.State = state;
        }

        public static IEnumerator DoNothingCoroutine() { yield break; }
        public static void DoNothing() { }
    }

    public enum StateTransition
    {
        Overwrite,
        Safe
    }

    public class BaseFSM : MonoBehaviour
    {
        /// <summary>
        /// 현재 적용중인 State 정보
        /// </summary>
        private StateMap _currentState;
        /// <summary>
        /// 전환될 다음 State 정보
        /// </summary>
        private StateMap _nextState;
        /// <summary>
        /// Enum에 해당하는 StateMap 조회용 Dictionary
        /// </summary>
        private Dictionary<Enum, StateMap> _stateDic;
        /// <summary>
        /// 상태 전환이 진행중인지? (Enter, Exit Coroutine 등)
        /// </summary>
        private bool _isInTransition = false;
        private IEnumerator _curTransition;
        private IEnumerator _enterRoutine;
        private IEnumerator _exitRoutine;
        private IEnumerator _queuedChange;

        /// <summary>
        /// 현재의 상태를 반환.
        /// </summary>
        /// <returns></returns>
        public Enum GetCurrentState()
        {
            if (_currentState != null)
            {
                return _currentState.State;
            }

            return null;
        }

        /// <summary>
        /// FSM 초기화 및 메서드 자동 파싱.
        /// </summary>
        /// <typeparam name="T">상태값이 되는 Enum값</typeparam>
        /// <param name="behaviour">호출하게 되는 FSM Class</param>
        public void Initialize<T>(MonoBehaviour behaviour)
        {
            // _stateDic 초기화
            _stateDic = new Dictionary<Enum, StateMap>();

            // Enum 파싱 후 상태값 베이스 추가
            Array values = Enum.GetValues(typeof(T));
            for (int i = 0; i < values.Length; i++)
            {
                StateMap map = new StateMap((Enum)values.GetValue(i));
                _stateDic.Add(map.State, map);
            }

            // 메서드 파싱 및 상태값 베이스와 연결
            MethodInfo[] methods = behaviour.GetType().GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);
            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];
                string[] names = method.Name.Split('_');

                // 메서드 형식이 맞지 않으면 건너뜀.
                if (names.Length <= 1 || Enum.TryParse(typeof(T), names[0], out var key) == false || _stateDic.ContainsKey((Enum)key) == false)
                {
                    continue;
                }

                StateMap map = _stateDic[(Enum)key];

                // name[1]: Enter, Exit, Update, ...
                switch (names[1])
                {
                    case "Enter":
                        map.Enter = method.ReturnType == typeof(IEnumerator) ?
                            CreateDelegate<Func<IEnumerator>>(method, behaviour) :
                            () => { CreateDelegate<Action>(method, behaviour)(); return null; };
                        break;

                    case "Exit":
                        map.Exit = method.ReturnType == typeof(IEnumerator) ?
                            CreateDelegate<Func<IEnumerator>>(method, behaviour) :
                            () => { CreateDelegate<Action>(method, behaviour)(); return null; };
                        break;

                    case "Update":
                        map.Update = CreateDelegate<Action>(method, behaviour);
                        break;
                }
            }
        }

        /// <summary>
        /// 대리자 생성
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">동작을 실행하는 메서드</param>
        /// <param name="target">메서드의 주체</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private T CreateDelegate<T>(MethodInfo method, object target) where T : class
        {
            var del = Delegate.CreateDelegate(typeof(T), target, method) as T;
            if (del == null)
            {
                throw new ArgumentException($"대리자 생성 실패: {method.Name}");
            }
            return del;
        }

        /// <summary>
        /// State를 변경한다.
        /// </summary>
        /// <param name="state">전환할 State</param>
        /// <param name="transition">기본 또는 강제진행할지? (기본값: Safe)</param>
        /// <exception cref="Exception"></exception>
        public void ChangeState(Enum state, StateTransition transition = StateTransition.Safe)
        {
            if (_stateDic == null)
            {
                throw new Exception("FSM이 초기화되지 않았습니다.");
            }
            else if (_stateDic.ContainsKey(state) == false)
            {
                throw new Exception($"FSM에서 상태 {state}를 찾을 수 없습니다.");
            }

            StateMap next = _stateDic[state];

            // 현재 State와 동일하면 건너뛴다.
            if (_currentState == next)
            {
                return;
            }

            if (_queuedChange != null)
            {
                StopCoroutine(_queuedChange);
                _queuedChange = null;
            }

            switch (transition)
            {
                // Enter, Exit의 코루틴이 동작하고 있다면 종료할 때까지 대기한 후 안전하게 전환한다.
                case StateTransition.Safe:
                    {
                        if (_isInTransition == true)
                        {
                            if (_exitRoutine != null)
                            {
                                _nextState = next;
                                return;
                            }

                            if (_enterRoutine != null)
                            {
                                _queuedChange = WaitForPreviousTransition(next);
                                StartCoroutine(_queuedChange);
                                return;
                            }
                        }
                    }
                    break;

                // 코루틴 상태와 상관없이 강제로 전환한다.
                case StateTransition.Overwrite:
                    {
                        if (_curTransition != null)
                        {
                            StopCoroutine(_curTransition);
                        }

                        if (_exitRoutine != null)
                        {
                            StopCoroutine(_exitRoutine);
                        }

                        if (_enterRoutine != null)
                        {
                            StopCoroutine(_enterRoutine);
                        }

                        _currentState = null;
                    }
                    break;
            }

            _isInTransition = true;
            _curTransition = ChangeToNewStateRoutine(next);
            StartCoroutine(_curTransition);
        }

        /// <summary>
        /// 상태 전환을 처리하는 코루틴
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        private IEnumerator ChangeToNewStateRoutine(StateMap next)
        {
            _nextState = next;

            if (_currentState != null)
            {
                _exitRoutine = _currentState.Exit();
                if (_exitRoutine != null)
                {
                    yield return StartCoroutine(_exitRoutine);
                }

                _exitRoutine = null;
            }

            _currentState = _nextState;
            _enterRoutine = _currentState.Enter();
            if (_enterRoutine != null)
            {
                yield return StartCoroutine(_enterRoutine);
            }
            _enterRoutine = null;

            _isInTransition = false;
        }

        /// <summary>
        /// 이전 전환이 끝날 때까지 대기하는 코루틴
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        private IEnumerator WaitForPreviousTransition(StateMap next)
        {
            while (_isInTransition == true)
            {
                yield return null;
            }

            ChangeState(next.State);
        }

        private void Update()
        {
            _currentState?.Update();
        }
    }
}
