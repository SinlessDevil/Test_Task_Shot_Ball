using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class ShotDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsActive { get; private set; }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            IsActive = true;
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            IsActive = false;
        }
    }
}