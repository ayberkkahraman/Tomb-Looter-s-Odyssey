using System.Collections.Generic;
using Project._Scripts.GameCore.CharacterController.StateMachine.Core;

namespace Project._Scripts.GameCore.CharacterController.StateMachine.States
{
     public abstract class CharacterBaseState
      {
            protected readonly CharacterStateMachine Context;
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

            protected CharacterBaseState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory)
            {
                  Context = currentContext;
                  Factory = characterStateFactory;
            }
            public abstract void EnterState();
            public abstract void FixedUpdateState();
            public abstract void UpdateState();
            protected abstract void ExitState();
            public abstract void CheckSwitchStates();
            public abstract void InitializeState();

            protected void SwitchState(CharacterBaseState newState)
            {
                  Context.Core.PreviousState = this;
                  Context.Core.NextState = newState;
                  ExitState(); 
                  Context.Core.CurrentState = newState;
                  Context.Core.CurrentState.EnterState();
                  
            }
      }
}