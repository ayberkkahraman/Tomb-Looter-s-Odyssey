using UnityEngine;

namespace Project._Scripts.GameCore.CharacterController.CharacterDirectionHandler
{
  public class CharacterDirection : MonoBehaviour
  {
    #region Fields
    public enum Direction{Right,Left,Up,Down}
    public Direction CurrentDirection;
    #endregion

    #region Unity Functions
    private void Start()
    {
      SetCharacterDirection(Direction.Up);
    } 
    #endregion

    #region Direction Configuration
    public void SetCharacterDirection(Direction direction) => CurrentDirection = direction;
    #endregion

    #region Animation Events
    public void ANIM_EVENT_SetCharacterDirection(int index) => SetCharacterDirection((Direction)index);
    #endregion
  }
}
