using UnityEngine;
using DG.Tweening;
using Extensions;

namespace ShotSystem
{
    public class ShotReload : MonoBehaviour
    {
        [Header("--- Reload Setting ---")]
        [SerializeField] private UnityEngine.UI.Image _ringReload;
        [SerializeField][Range(1f, 3f)] private float _timeReload;
        
        public bool IsReloadShoot { get; private set; }

        private void Awake()
        {
            LogErrorExtensions.LogError(_ringReload);
        }

        private void Start()
        {
            IsReloadShoot = true;
        }

        public void ReloadShot()
        {
            StartCoroutine(ReloadRoutine());
        }
        private System.Collections.IEnumerator ReloadRoutine()
        {
            IsReloadShoot = false;
            _ringReload.fillAmount = 0;
            _ringReload.DOFillAmount(1f, _timeReload);
            yield return new WaitForSeconds(_timeReload);
            SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_RELOUD_SHOT);
            IsReloadShoot = true;
        }
    }
}