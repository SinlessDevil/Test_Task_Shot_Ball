using System;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

namespace UISystem
{
    public class MusicControl : MonoBehaviour
    {
        private const string STR_SOUND_KEY = "sounds";

        [Space(10)]
        [Header("----------------- Image Variabls -----------------")]
        [SerializeField] private Image[] _iconSound;
        [SerializeField] private Sprite _soundActiveIsTrue;
        [SerializeField] private Sprite _soundActiveIsFalse;
        [Space(10)]
        [SerializeField] private AudioListener _audioListener;

        private bool _isActive = true;

        private void Start(){
            LogErrorExtensions.LogError(_iconSound);
            LogErrorExtensions.LogError(_soundActiveIsTrue);
            LogErrorExtensions.LogError(_soundActiveIsFalse);
            LogErrorExtensions.LogError(_audioListener);

            Set—urrentSoundSettings();
        }

        private void OnEnable(){
            UIButtons.ButtonClickSoundEvent += AudioControl;
        }
        private void OnDisable(){
            UIButtons.ButtonClickSoundEvent -= AudioControl;
        }

        private void Set—urrentSoundSettings()
        {
            int currentState = Convert.ToInt32(_isActive);
            currentState = SaveSystem.SaveSettings.Instance.LoadInt(STR_SOUND_KEY, currentState);
            _isActive = Convert.ToBoolean(currentState);

            if (_isActive) for (int i = 0; i < _iconSound.Length; i++) _iconSound[i].sprite = _soundActiveIsTrue;
            else for (int i = 0; i < _iconSound.Length; i++) _iconSound[i].sprite = _soundActiveIsFalse;

            AudioListener.pause = !_isActive;
        }

        private void AudioControl()
        {
            StartCoroutine(MuteDelayRoutine());
        }

        private System.Collections.IEnumerator MuteDelayRoutine()
        {
            SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_BUTTON_CLIKC);

            yield return new WaitForSecondsRealtime(0.1f);

            if (_isActive) for (int i = 0; i < _iconSound.Length; i++) _iconSound[i].sprite = _soundActiveIsFalse;
            else for (int i = 0; i < _iconSound.Length; i++) _iconSound[i].sprite = _soundActiveIsTrue;

            _isActive = !_isActive;
            AudioListener.pause = !_isActive;

            SaveSystem.SaveSettings.Instance.SaveInt(STR_SOUND_KEY, Convert.ToInt32(_isActive));
        }
    }
}