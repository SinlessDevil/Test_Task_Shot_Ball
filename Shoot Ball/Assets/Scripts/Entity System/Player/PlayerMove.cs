using UnityEngine;
using UniRx;
using Extensions;

namespace EntitySystem.PlayerSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [Header("---- Speed Setting ----")]
        [SerializeField] private float _maxJumpSpeed;
        [SerializeField] private float _speed;
        [Space(5)]
        [SerializeField] private Transform _targerPos;
        [Space(5)]
        [Header("---- Jump Setting ----")]
        [SerializeField] private Transform _chekerGroundPos;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundDistance;

        public float jumpSpeed { get; set; }

        private bool _isGround;

        private Vector3 _playerVector;
        private Vector3 _targetVector;

        private Rigidbody _rb;

        private void Awake(){
            LogErrorExtensions.LogError(_targerPos);
            LogErrorExtensions.LogError(_chekerGroundPos);

            _rb = GetComponent<Rigidbody>();

            jumpSpeed = _maxJumpSpeed;
        }

        private void OnEnable(){
            UISystem.TryGame.OnPlayerMoveToFinishEvent += StartMoveToTarger;
        }

        private void OnDisable(){
            UISystem.TryGame.OnPlayerMoveToFinishEvent -= StartMoveToTarger;

            _disposable.Clear();
        }

        private void StartMoveToTarger(){
            Observable.EveryFixedUpdate().Subscribe(value => {
                _isGround = IsOnTheGround();
                if (_isGround)
                    Jump();
                if (!_isGround)
                    Move();

            }).AddTo(_disposable);
        }
        private void Jump(){
            SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_SLIME_JUMP);
            _rb.velocity = Vector2.up * jumpSpeed;
        }

        private bool IsOnTheGround(){
            bool result = Physics.Raycast(transform.position, Vector3.down, _groundDistance);
            return result;
        }

        private void Move(){
            _playerVector = transform.position;
            _targetVector = _targerPos.position;

            transform.position = Vector3.MoveTowards(_playerVector, _targetVector, _speed);
        }
    }
}