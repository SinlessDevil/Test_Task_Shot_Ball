using UnityEngine;
using Extensions;

namespace CameraSystem
{
    public class CameraFollower : MonoBehaviour
    {
        [Header("Transform component")]
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _followerTransform;
        [Space(5)]
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothing;

        private void Awake()
        {
            LogErrorExtensions.LogError(_targetTransform);
            LogErrorExtensions.LogError(_followerTransform);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offset, Time.fixedDeltaTime * _smoothing);

            _followerTransform.position = nextPosition;
        } 
    }
}