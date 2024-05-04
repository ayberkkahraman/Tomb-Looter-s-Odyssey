using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.ScriptableObjects
{
  [CreateAssetMenu(fileName = "Character Locomotion Data", menuName = "Character Locomotion")]
  public class CharacterLocomotionData : ScriptableObject
  {
    [Range(1f,10f)]public float MovementSpeed = 4f;
  }
}
