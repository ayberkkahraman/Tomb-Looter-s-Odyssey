using Project._Scripts.GameCore.CharacterController.StateMachine.States;

namespace Project._Scripts.GameCore.CharacterController.StateMachine.Core
{
  public class StateCore
  {
    public CharacterStateFactory StateFactory { get; set; }
    public CharacterBaseState CurrentState { get; set; }
    public CharacterBaseState PreviousState { get; set; }
    public CharacterBaseState NextState { get; set; }
  }
}