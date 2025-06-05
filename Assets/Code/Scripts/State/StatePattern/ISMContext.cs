using System.Collections.Generic;
using System.Linq;

namespace Platformer2D
{
    public interface ISPContext<T_ID>
    {
        List<BaseSPState<T_ID>> CurrentStates { get; set; }
        Dictionary<T_ID, BaseSPState<T_ID>> States { get; set; }

        void InitializeStates();
        
        void SetDefaultState();

        public void SetCurrentState(T_ID id)
        {
            ClearAllCurrentState();
            AddCurrentState(id);
        }

        public void ClearAllCurrentState()
        {
            if (CurrentStates.Count == 0) return;

            foreach (var currentState in CurrentStates)
                currentState.ExitState();

            CurrentStates.Clear();
        }

        public void AddCurrentState(T_ID id)
        {
            if (!States.TryGetValue(id, out var state)) return;
            if (CurrentStates.Contains(state)) return;

            CurrentStates.Add(state);

            var priorityState = CurrentStates.OrderByDescending(s => s.ID).First();
            if (state.Equals(priorityState))
                state.EnterState(true);
            else
                state.EnterState(false);
        }

        public void RemoveCurrentState(BaseSPState<T_ID> stateRemoved)
        {
            if (!CurrentStates.Remove(stateRemoved)) return;

            stateRemoved.ExitState();

            if (CurrentStates.Count == 0)
                SetDefaultState();
            else
            {
                var priorityState = CurrentStates.OrderByDescending(s => s.ID).First();
                priorityState.EnterState(true);
            }
        }
    }
}