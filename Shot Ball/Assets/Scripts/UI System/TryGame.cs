using UnityEngine;
using SoundSystem;
using EntitySystem.PlayerSystem;
using Extensions;
using DG.Tweening;
using Zenject;

namespace UISystem
{
    public class TryGame : MonoBehaviour
    {
        public static event System.Action OnPlayerMoveToFinishEvent;

        private const string STR_TRIGGER_BUTTON_TRY = "Try_Trigger";

        [Header("---- Game Componnet ----")]
        [SerializeField] private CameraSystem.CameraFollower _cameraFollower;
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _track;
        [Space(5)]
        [Header("---- UI Componnet ----")]
        [SerializeField] private GameObject _shotPanel;
        [SerializeField] private GameObject _gamePanel;

        private Player _player;
        private Transform _playerBody;
        private Transform _playerTrailEffect;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            _playerBody = _player.body;
            _playerTrailEffect = _player.trail;
        }

        private const float WAIT_TIME = 4f;

        private void Awake()
        {
            LogErrorExtensions.LogError(_cameraFollower);
            LogErrorExtensions.LogError(_track);

            LogErrorExtensions.LogError(_shotPanel);
            LogErrorExtensions.LogError(_gamePanel);
            LogErrorExtensions.LogError(_anim);
        }

        private void OnEnable()
        {
            _player.OnStartGameEvent += SetTryGame;
            UIButtons.ButtonClickTryEvent += StartTryGame;
        }

        private void OnDisable()
        {
            _player.OnStartGameEvent -= SetTryGame;
            UIButtons.ButtonClickTryEvent -= StartTryGame;
        }

        private void SetTryGame()
        {
            _anim.SetTrigger(STR_TRIGGER_BUTTON_TRY);
        }

        private void StartTryGame()
        {
            SoundSystem.AudioClips.Instance.PlayClip(DictionarSounds.STR_AUDIO_CLIP_BUTTON_CLIKC);
            OnPlayerMoveToFinishEvent?.Invoke();
            _gamePanel.SetActive(false);
            _cameraFollower.enabled = true;
            StartCoroutine(TryGameRoutine());
        }

        private System.Collections.IEnumerator TryGameRoutine()
        {
            while (true)
            {
                Vector3 offsetTrack = _track.transform.localScale;
                Vector3 offsetPlayer = _player.transform.localScale;
                Vector3 offsetTrail = _playerTrailEffect.transform.localScale;

                yield return new WaitForSeconds(WAIT_TIME);

                offsetTrack = new Vector3(offsetTrack.x, offsetTrack.y - 0.2f, offsetTrack.y);
                offsetPlayer = new Vector3(offsetPlayer.x - 0.1f, offsetPlayer.y - 0.1f, offsetPlayer.x - 0.1f);
                offsetTrail = new Vector3(offsetTrail.x - 0.2f, offsetTrail.y - 0.2f, offsetTrail.x - 0.2f);

                _playerBody.DOScale(offsetPlayer, WAIT_TIME);
                _track.DOScaleY(offsetTrack.y, WAIT_TIME);
                _playerTrailEffect.DOScale(offsetTrail, WAIT_TIME);
            }
        }
    }
}