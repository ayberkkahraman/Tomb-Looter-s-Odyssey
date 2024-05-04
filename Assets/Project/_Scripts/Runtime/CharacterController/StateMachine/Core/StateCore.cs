using Project._Scripts.Runtime.CharacterController.StateMachine.States;

namespace Project._Scripts.Runtime.CharacterController.StateMachine.Core
{
  public class StateCore
  {
    public CharacterStateFactory StateFactory { get; set; }
    public CharacterBaseState CurrentState { get; set; }
    public CharacterBaseState PreviousState { get; set; }
    public CharacterBaseState NextState { get; set; }
  }
}