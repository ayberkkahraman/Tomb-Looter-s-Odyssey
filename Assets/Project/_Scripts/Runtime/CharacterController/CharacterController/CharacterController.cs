using System;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.CharacterController
{
  public class CharacterController : MonoBehaviour
  {
    #region Fields
    public enum Direction{Right,Left,Up,Down}
    public Direction CharacterDirection;
    #endregion

    #region Unity Functions
    private void Start()
    {
      SetCharacterDirection(Direction.Up);
    } 
    #endregion

    #region Direction Configuration
    public void SetCharacterDirection(Direction direction) => CharacterDirection = direction;
    #endregion

    #region Animation Events
    public void ANIM_EVENT_SetCharacterDirection(int index) => SetCharacterDirection((Direction)index);
    #endregion
  }
}
