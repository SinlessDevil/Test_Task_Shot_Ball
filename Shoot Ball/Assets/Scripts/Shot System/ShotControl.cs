using UnityEngine;
using FactorySystem;
using Extensions;

namespace ShotSystem
{
    public class ShotControl : MonoBehaviour
    {
        [Header("----------------------- Setting Shot Control -----------------------")]
        [Space(5)]
        [Header("--- Push Force ---")]
        [SerializeField] [Range(1f,30f)] private float _maxPushForce;
        [SerializeField] [Range(0f,30f)] private float _minPushForce;
        private float _currentPushForce;
        [Space(5)]
        [Header("--- Shot Componnet ---")]
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private ShotReload _shotReload;
        [SerializeField] private ShotDirection _shotDirection;
        [SerializeField] private ShotSizeTransfer _shotSizeTransfer;
        [Space(5)]
        [SerializeField] private EntitySystem.PlayerSystem.Player _player;
        [SerializeField] private GameObject _gameObjectPanelShot;
        [SerializeField] private StatePanelShot _panelShot;

        private bool _isStartGame = false;

        private void Awake()
        {
            LogErrorExtensions.LogError(_bulletFactory);
            LogErrorExtensions.LogError(_shotReload);
            LogErrorExtensions.LogError(_shotDirection);
            LogErrorExtensions.LogError(_shotSizeTransfer);

            LogErrorExtensions.LogError(_player);
            LogErrorExtensions.LogError(_gameObjectPanelShot);
            LogErrorExtensions.LogError(_panelShot);
        }

        private void Start()
        {
            _panelShot = _gameObjectPanelShot.GetComponent<StatePanelShot>();

            SetSettingShotByDefould();
        }

        private void OnEnable()
        {
            _player.OnStartGameEvent += SetTryGame;
        }

        private void OnDisable()
        {
            _player.OnStartGameEvent -= SetTryGame;
        }

        private void Update()
        {
            if (_shotReload.IsReloadShoot)
            {
                if (_panelShot.IsActive && !_isStartGame)
                {
                    if (_bulletFactory.currentBullet == null)
                    {
                        _bulletFactory.SetBullet();
                        _shotSizeTransfer.SubscribeToSizeTransfer();
                        StartCoroutine(_shotSizeTransfer.SizeTransferRoutine());
                        StartCoroutine(PushForceRoutine());
                    }
                }
                else if (!_panelShot.IsActive && _bulletFactory.currentBullet != null)
                {
                    _shotSizeTransfer.EndSizeTransfer();
                    _shotReload.ReloadShot();
                    ShotBullet();
                    SetSettingShotByDefould();
                }
            }
        }

        private void SetTryGame()
        {
            _isStartGame = true;
        }

        private void SetSettingShotByDefould()
        {
            _currentPushForce = _minPushForce;
            _shotSizeTransfer.currentSizeTransfer = 0;
        }

        private void ShotBullet()
        {
            if (_bulletFactory.currentBullet != null)
            {
                SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_SHOOT_BULLET);
                _bulletFactory.currentBullet.rb.AddForce(_shotDirection.GetDirection().forward * _currentPushForce, ForceMode.Impulse);
                _bulletFactory.currentBullet = null;
            }
        }
        private System.Collections.IEnumerator PushForceRoutine()
        {
            while (_currentPushForce < _maxPushForce)
            {
                yield return new WaitForSeconds(0.1f);
                if (_bulletFactory.currentBullet == null) break;
                SetPushForce(0.5f);
            }
        }
        private void SetPushForce(float value)
        {
            _currentPushForce += value;
        }
    }
}