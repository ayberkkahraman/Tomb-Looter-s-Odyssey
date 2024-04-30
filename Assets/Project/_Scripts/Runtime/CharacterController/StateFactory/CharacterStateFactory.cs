using Project._Scripts.Runtime.CharacterController.States;

namespace Project._Scripts.Runtime.CharacterController.StateFactory
{
    public class CharacterStateFactory
    {
        private readonly CharacterStateMachine.CharacterStateMachine _context;

        public InitializationState InitializationState;
        public CharacterWalkState WalkState;
        public CharacterIdleState IdleState;
        public CharacterInteractionState InteractionState;

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
        public CharacterBaseState Initialize()
        {
            return InitializationState ??= new InitializationState(_context, this);
        }
    }
}
 