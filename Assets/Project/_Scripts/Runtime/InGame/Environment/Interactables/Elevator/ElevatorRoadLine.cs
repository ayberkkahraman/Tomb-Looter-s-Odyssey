using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Elevator
{
    public class ElevatorRoadLine : MonoBehaviour
    {
        public Transform TopLine;
        public Transform MiddleLine;
        public Transform BottomLine;

        public Transform ElevatorPlatform;

        public float RoadLineLenght;

        public void OnValidate()
        {
            MiddleLine.localScale = new Vector2(1, RoadLineLenght);

            TopLine.localPosition = new Vector2(0, RoadLineLenght/2);
            BottomLine.localPosition = new Vector2(0, -RoadLineLenght/2);

            ElevatorPlatform.position = new Vector2(BottomLine.position.x, BottomLine.position.y + .2f);
        }
    }
}
