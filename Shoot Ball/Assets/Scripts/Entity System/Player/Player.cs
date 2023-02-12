using System;
using UnityEngine;
using Extensions;

namespace EntitySystem.PlayerSystem
{
    public sealed class Player : Entity
    {
        public event Action OnGameOverEvent;
        public event Action OnStartGameEvent;
        public event Action<float> OnChangeFillBarSizeEvent;

        [Space(10)]
        [Header("----- Player Component -----")]
        [SerializeField] private Transform _body;
        [SerializeField] private PlayerMove _playerMove;

        private void Awake()
        {
            LogErrorExtensions.LogError(_body);
            LogErrorExtensions.LogError(_playerMove);
        }

        public override void ApplyChangeSize(float value)
        {
            if(_currentSize <= _minSize){
                OnStartGameEvent?.Invoke();
                return;
            }

            _currentSize -= value;
            _playerMove.jumpSpeed -= value * 4;

            gameObject.transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);
            OnChangeFillBarSizeEvent?.Invoke(_currentSize);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out EntitySystem.Obstacle obstacle))
            {
                ParticalFXSystem.SpawnerFX.Instance.CreatePacticalFX(collision.contacts[0].point);

                OnGameOverEvent?.Invoke();

                _body.Deactivate();
                _playerMove.enabled = false;
            }
        }
    }
}