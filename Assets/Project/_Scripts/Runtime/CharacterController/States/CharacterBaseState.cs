using System.Collections.Generic;
using Project._Scripts.Runtime.CharacterController.StateFactory;

namespace Project._Scripts.Runtime.CharacterController.States
{
     public abstract class CharacterBaseState
      {
            protected readonly CharacterStateMachine.CharacterStateMachine Context;
            protected readonly CharacterStateFactory Factory;

            protected HashSet<CharacterBaseState> States;

            /// <summary>
            /// Handles the acceleration of the character movement speed
            /// </summary>
            /// <param name="multiplier"></param>
            /// <param name="rotationSmooth"></param>
            protected abstract void AccelerationConfiguration(float multiplier = 1f, bool rotationSmooth = true);
            
            /// <summary>
            /// Handles the rotation of the character
            /// </summary>
            /// <param name="multiplier"></param>
            protected abstract void RotationConfiguration(float multiplier = 1f);

            /// <summary>
            /// Handles the gravity behaviour of the character
            /// </summary>
            /// <param name="speedMultiplier"></param>
            protected abstract void HandleGravity(float speedMultiplier = 1f);

            protected CharacterBaseState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory)
            {
                  Context = currentContext;
                  Factory = characterStateFactory;
            }
            public abstract void EnterState();
            public abstract void FixedUpdateState();
            public abstract void UpdateState();
            public abstract void LateUpdateState();
            protected abstract void ExitState();
            public abstract void CheckSwitchStates();
            public abstract void InitializeSubState();
            public virtual void DrawGizmos(){}

            public abstract void InitializeState();
            
            public void SwitchState(CharacterBaseState newState)
            {
                  Context.PreviousState = this;
                  Context.NextState = newState;
                  ExitState(); 
                  Context.CurrentState = newState;
                  Context.CurrentState.EnterState();
                  
            }
            protected virtual void SetSuperState(){}
            protected virtual void SetSubState(){}

      }
}
