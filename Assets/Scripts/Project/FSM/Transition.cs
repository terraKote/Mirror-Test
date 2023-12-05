using System;
using Unity.VisualScripting;

namespace Project.FSM
{
    public class Transition
    {
        private readonly Func<bool> _condition;
        private readonly State _sourceState;
        private readonly State _targetState;

        public State TargetState => _targetState;

        public Transition(Func<bool> condition, State sourceState, State targetState)
        {
            _condition = condition;
            _sourceState = sourceState;
            _targetState = targetState;
        }

        public bool CanTransit(State currentState)
        {
            // Prevent checking for null
            if (_sourceState == null)
                throw new ArgumentNullException(nameof(_sourceState));

            // if this is a traditional state change, do it
            if (_sourceState is not AnyState anyState)
            {
                return _condition.Invoke() && _sourceState.Equals(currentState);
            }

            // can we enter from the same state?
            if (anyState.AllowLoopEnter)
            {
                return _condition.Invoke();
            }

            // if this is our first time entering from any state
            if (anyState.Breadcrumb == null)
            {
                anyState.Breadcrumb = _targetState;
                return _condition.Invoke();
            }

            // if we entering from the same state
            if (anyState.Breadcrumb.Equals(_targetState))
            {
                return false;
            }

            // check if conditions true, so we can enter
            if (!_condition.Invoke())
            {
                return false;
            }

            anyState.Breadcrumb = _targetState;
            return true;
        }
    }
}