using System;
using UnityEngine;
using Extensions;

namespace EntitySystem.PlayerSystem
{
    [RequireComponent(typeof(PlayerMove))]
    public sealed class Player : Entity
    {
        public event Action OnGameOverEvent;
        public event Action OnStartGameEvent;
        public event Action<float> OnChangeFillBarSizeEvent;

        [Header("----- Player Component -----")]
        public Transform body;
        public Transform trail;

        private PlayerMove _playerMove;

        private void Awake()
        {
            _playerMove = GetComponent<PlayerMove>();

            LogErrorExtensions.LogError(body);
            LogErrorExtensions.LogError(trail);
        }

        public override void ApplyChangeSize(float value)
        {
            if(_currentSize <= _minSize){
                OnStartGameEvent?.Invoke();
                return;
            }

            _currentSize -= value;
            _playerMove.JumpSpeed -= value * 4;

            gameObject.transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);
            OnChangeFillBarSizeEvent?.Invoke(_currentSize);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out EntitySystem.Obstacle obstacle))
            {
                ParticalFXSystem.SpawnerFX.Instance.CreatePacticalFX(collision.contacts[0].point);

                OnGameOverEvent?.Invoke();

                body.Deactivate();
                _playerMove.enabled = false;
            }
        }
    }
}