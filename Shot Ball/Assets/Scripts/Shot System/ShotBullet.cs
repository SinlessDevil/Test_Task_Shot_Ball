using UnityEngine;
using FactorySystem;
using Extensions;
using Zenject;

namespace ShotSystem
{
    public class ShotBullet : MonoBehaviour
    {
        [Header("--- Push Force ---")]
        [SerializeField][Range(1f, 30f)] private float _maxPushForce;
        [SerializeField][Range(0f, 30f)] private float _minPushForce;
        private float _currentPushForce;
        [Space(5)]
        [SerializeField] private ShotDirection _shotDirection;

        private BulletFactory _bulletFactory;

        [Inject]
        private void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        private void Awake()
        {
            LogErrorExtensions.LogError(_shotDirection);
        }

        public void PushBullet()
        {
            if (_bulletFactory.currentBullet != null)
            {
                SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_SHOOT_BULLET);
                _bulletFactory.currentBullet.rb.AddForce(_shotDirection.GetDirection().forward * _currentPushForce, ForceMode.Impulse);
                _bulletFactory.currentBullet.SetTimeDestroyBullet();
                _bulletFactory.currentBullet = null;
            }
        }
        public System.Collections.IEnumerator PushForceRoutine()
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
        public void SetPushForceByDefould()
        {
            _currentPushForce = _minPushForce;
        }
    }
}