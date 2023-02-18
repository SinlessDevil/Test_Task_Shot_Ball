using UnityEngine;
using EntitySystem.PlayerSystem;
using Zenject;

namespace CameraSystem
{
    public class CameraFollower : MonoBehaviour
    {
        [Header("Transform component")]
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothing;

        private Transform _targetTransform;

        [Inject]
        private void Construct(Player player)
        {
            _targetTransform = player.transform;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offset, Time.fixedDeltaTime * _smoothing);

            transform.position = nextPosition;
        } 
    }
}