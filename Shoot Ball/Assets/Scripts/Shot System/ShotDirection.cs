using UnityEngine;

namespace ShotSystem
{
    public class ShotDirection : MonoBehaviour
    {
        [SerializeField] private GameObject _shotDirection;
        [SerializeField] [Range(1f,15f)] private float _rotationSpeed = 5f;

        private void Awake()
        {
            Extensions.LogErrorExtensions.LogError(_shotDirection);
        }

        public Transform GetDirection()
        {
            return _shotDirection.transform;
        }

        private void Update()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float touchDeltax = touch.deltaPosition.x;
                _shotDirection.transform.Rotate(0, touchDeltax * _rotationSpeed * Time.deltaTime, 0);
            }
        }
    }
}