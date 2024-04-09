using JetBrains.Annotations;
using Project._Scripts.Runtime.CharacterController.States;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Door;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.StateFactory
{
    public class CharacterStateFactory
    {
        private readonly CharacterStateMachine.CharacterStateMachine _context;

        public InitializationState InitializationState;
        public CharacterWalkState WalkState;
        public CharacterIdleState IdleState;
        public CharacterFallState FallState;
        public CharacterTurnState TurnState;
        public CharacterInteractionState InteractionState;
        public CharacterDeadState DeadState;
        public CharacterJumpState JumpState;
        public CharacterTransportState TransportState;

        public CharacterStateFactory(CharacterStateMachine.CharacterStateMachine currentContext)
        {
            _context = currentContext;
        }

        public CharacterBaseState Idle()
        {
            return IdleState ??= new CharacterIdleState(_context, this);
        }
        public CharacterBaseState Walk()
        {
            return WalkState ??= new CharacterWalkState(_context, this);
        }
        

        public CharacterBaseState Interaction(bool canMove = false)
        {
            InteractionState ??= new CharacterInteractionState(_context, this, canMove);
            InteractionState.CanMove = canMove;
            return InteractionState;
        }

        public CharacterBaseState Fall()
        {
            FallState ??= new CharacterFallState(_context, this);
            return FallState;
        }

        public CharacterBaseState Dead()
        {
            return DeadState ??= new CharacterDeadState(_context, this);
        }

        public CharacterBaseState Turn(bool isTurningRight = true)
        {
            TurnState ??= new CharacterTurnState(_context, this, isTurningRight);
            TurnState.IsTurningRight = isTurningRight;
            return TurnState;
        }
        public CharacterBaseState Initialize()
        {
            return InitializationState ??= new InitializationState(_context, this);
        }
    
        public CharacterBaseState Jump(Vector2 moveDirection, bool isMoving = false)
        {
            JumpState ??= new CharacterJumpState(_context, this, isMoving);
            JumpState.IsMoving = isMoving;
            return JumpState;
        }


        public CharacterBaseState Transport(TransporterDoor targetDoor)
        {
            TransportState ??= new CharacterTransportState(_context, this, targetDoor);
            TransportState.TargetDoor = targetDoor;
            return TransportState;
        }
    }
}
 