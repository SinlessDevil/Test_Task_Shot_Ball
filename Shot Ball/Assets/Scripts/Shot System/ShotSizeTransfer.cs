using UnityEngine;
using EntitySystem.PlayerSystem;
using FactorySystem;
using UniRx;
using Zenject;

namespace ShotSystem
{
    public class ShotSizeTransfer : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [Header("--- Size Transfer Settings ---")]
        [SerializeField][Range(0.1f, 0.6f)] private float _maxSizeTransfer;
        [SerializeField][Range(0f, 0.5f)] private float _minSizeTransfer;
        private float currentSizeTransfer;

        private Player _player;
        private BulletFactory _bulletFactory;

        [Inject]
        private void Construct(Player player, BulletFactory bulletFactory)
        {
            _player = player;
            _bulletFactory = bulletFactory;
        }

        private void OnDisable()
        {
            EndSizeTransfer();
        }

        public void SubscribeToSizeTransfer()
        {
            _player.OnSizeTransferCommand.Subscribe(value =>
            {
                _player.ApplyChangeSize(value);
            }).AddTo(_disposable);

            _bulletFactory.currentBullet.OnSizeTransferCommand.Subscribe(value =>
            {
                _bulletFactory.currentBullet.ApplyChangeSize(value);
            }).AddTo(_disposable);
        }

        public void SetSizeTransfer(float value)
        {
            if (_bulletFactory.currentBullet != null)
            {
                _player.OnSizeTransferCommand.Execute(value);
                _bulletFactory.currentBullet.OnSizeTransferCommand.Execute(value);
            }

            currentSizeTransfer += value;
        }
        public System.Collections.IEnumerator SizeTransferRoutine()
        {
            while (currentSizeTransfer < _maxSizeTransfer)
            {
                yield return new WaitForSeconds(0.1f);
                if (_bulletFactory.currentBullet == null) break;
                SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_TRANSFER_SIZE);
                SetSizeTransfer(0.01f);
            }
        }
        public void EndSizeTransfer()
        {
            _disposable.Clear();
            StopCoroutine(SizeTransferRoutine());
        }

        public void SetSizeTransferByDefould()
        {
            currentSizeTransfer = 0;
        }
    }
}