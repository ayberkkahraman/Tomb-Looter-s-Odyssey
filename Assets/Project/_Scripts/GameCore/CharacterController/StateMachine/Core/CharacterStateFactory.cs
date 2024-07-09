using Project._Scripts.GameCore.CharacterController.StateMachine.States;

namespace Project._Scripts.GameCore.CharacterController.StateMachine.Core
{
    public class CharacterStateFactory
    {
        private readonly CharacterStateMachine _context;

        public CharacterInitializationState CharacterInitializationState;
        public CharacterIdleState IdleState;
        public CharacterWalkState WalkState;
        public CharacterInteractionState InteractionState;

        public CharacterStateFactory(CharacterStateMachine currentContext){_context = currentContext;}
        public CharacterBaseState Initialize() => CharacterInitializationState ??= new CharacterInitializationState(_context, this);
        public CharacterBaseState Idle() => IdleState ??= new CharacterIdleState(_context, this);
        public CharacterBaseState Walk() => WalkState ??= new CharacterWalkState(_context, this);
        public CharacterBaseState Interaction(bool canMove = false)
        {
            InteractionState ??= new CharacterInteractionState(_context, this, canMove);
            InteractionState.CanMove = canMove;
            return InteractionState;
        }
    }
}