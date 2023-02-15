using UnityEngine;
using EntitySystem.PlayerSystem;
using FactorySystem;
using UISystem;
using Extensions;
using Zenject;

namespace ShotSystem
{
    [RequireComponent(typeof(ShotReload),typeof(ShotSizeTransfer), typeof(Shot))]
    public class Shot : MonoBehaviour
    {
        [SerializeField] private GameObject _panelShot;

        private ShotReload _shotReload;
        private ShotSizeTransfer _shotSizeTransfer;
        private ShotBullet _shotBullet;
        private ShotDisplay _shotDisplay;

        private Player _player;
        private BulletFactory _bulletFactory;

        private bool _isStartGame = false;

        [Inject]
        private void Construct(Player player, BulletFactory bulletFactory)
        {
            _player = player;
            _bulletFactory = bulletFactory;
        }

        private void Awake()
        {
            _shotReload = GetComponent<ShotReload>();
            _shotSizeTransfer = GetComponent<ShotSizeTransfer>();
            _shotBullet = GetComponent<ShotBullet>();

            LogErrorExtensions.LogError(_panelShot);
        }

        private void Start()
        {
            _shotDisplay = _panelShot.GetComponent<ShotDisplay>();

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
                if (_shotDisplay.IsActive && !_isStartGame)
                {
                    if (_bulletFactory.currentBullet == null)
                    {
                        _bulletFactory.SetBullet();
                        _shotSizeTransfer.SubscribeToSizeTransfer();
                        StartCoroutine(_shotSizeTransfer.SizeTransferRoutine());
                        StartCoroutine(_shotBullet.PushForceRoutine());
                    }
                }
                else if (!_shotDisplay.IsActive && _bulletFactory.currentBullet != null)
                {
                    _shotSizeTransfer.EndSizeTransfer();
                    _shotReload.ReloadShot();
                    _shotBullet.PushBullet();
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
            _shotBullet.SetPushForceByDefould();
            _shotSizeTransfer.SetSizeTransferByDefould();
        }
    }
}