using UnityEngine;
using UnityEngine.EventSystems;

namespace Project._Scripts.Global.ManagerSystem.ManagerClasses
{
    public class CursorManager : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(eventData);
        }

        void LateUpdate()
        {
            gameObject.transform.position = Input.mousePosition;
        }
    }
}
