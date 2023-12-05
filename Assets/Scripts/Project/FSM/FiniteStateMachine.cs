using System;

namespace Project.FSM
{
    public class FiniteStateMachine : IDisposable
    {
        private readonly Transition[] _transitions;

        private State _currentState;

        public FiniteStateMachine(Transition[] transitions, State currentState)
        {
            _transitions = transitions;
            _currentState = currentState;
        }

        public void Start()
        {
            _currentState.Enter();
        }

        public void Stop()
        {
            _currentState?.Exit();
        }

        private void SetState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public virtual void Tick(float deltaTime)
        {
            if (_currentState == null)
            {
                throw new ArgumentNullException(nameof(_currentState));
            }

            foreach (var transition in _transitions)
            {
                if (!transition.CanTransit(_currentState))
                    continue;

                SetState(transition.TargetState);
                return;
            }

            _currentState?.Update(deltaTime);
        }

        public virtual void FixedTick(float deltaTime)
        {
            _currentState?.FixedUpdate(deltaTime);
        }

        public virtual void Dispose()
        {
        }

        public override string ToString()
        {
            return _currentState.GetType().Name;
        }

        public static string GetStateID<T>() where T : State
        {
            return typeof(T).Name;
        }
    }
}