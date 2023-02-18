using UnityEngine;
using Extensions;

namespace EntitySystem.PlayerSystem
{
    public class PlayerJumpAnimation : MonoBehaviour
    {
        [Header("---- Player Componnet ----")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _playerBody;
        [Space(5)]
        [Header("---- Scale Setting ----")]
        [SerializeField] private Vector3 _scaleDown = new(1.2f, 0.8f, 1.2f);
        [SerializeField] private Vector3 _scaleUp = new(0.8f, 1.2f, 0.8f);
        [SerializeField] private float _scaleFactor = 10f;

        private void Start()
        {
            LogErrorExtensions.LogError(_playerTransform);
            LogErrorExtensions.LogError(_playerBody);
        }

        private void Update()
        {
            Vector3 relativePosition = _playerTransform.InverseTransformPoint(transform.position);
            float interpolation = relativePosition.y * _scaleFactor;
            Vector3 scale = Lerp3(_scaleDown, Vector3.one, _scaleUp, interpolation);
            _playerBody.localScale = scale;
        }

        private Vector3 Lerp3(Vector3 a,Vector3 b,Vector3 c, float t)
        {
            if (t < 0)
                return Vector3.LerpUnclamped(a, b, t + 1f);
            else
                return Vector3.LerpUnclamped(b, c, t);
        }
    }
}
