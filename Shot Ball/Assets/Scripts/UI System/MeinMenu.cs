using System;
using UnityEngine;
using SoundSystem;
using DG.Tweening;
using Extensions;

namespace UISystem
{
    public class MeinMenu : MonoBehaviour
    {
        public static event Action OnStartGameEvent;

        [Header("---- UI Componnet ----")]
        [SerializeField] private UnityEngine.UI.Image _fadeImage;
        [SerializeField] private GameObject _audioManagerPrefab;
        [SerializeField] private GameObject _meinMenu;
        [SerializeField] private GameObject _gamePlay;
        [SerializeField] private GameObject _gameManager;

        private void Awake()
        {
            LogErrorExtensions.LogError(_fadeImage);
            LogErrorExtensions.LogError(_audioManagerPrefab);
            LogErrorExtensions.LogError(_meinMenu);
            LogErrorExtensions.LogError(_gamePlay);
            LogErrorExtensions.LogError(_gameManager);

            if (GameObject.Find("[AUDIO MANAGER](Clone)") == null)
            {
                Instantiate(_audioManagerPrefab, transform.position, Quaternion.identity);
            }

            Time.timeScale = 0f;

            ShowPanels(false);
        }

        private void OnEnable()
        {
            UIButtons.ButtonClickExitEvent += ExitGame;
            UIButtons.ButtonClickPlayGameEvent += PlayGame;
        }

        private void OnDisable()
        {
            UIButtons.ButtonClickExitEvent -= ExitGame;
            UIButtons.ButtonClickPlayGameEvent -= PlayGame;
        }

        private void PlayGame()
        {
            Time.timeScale = 1f;

            ShowPanels(true);

            FadeAtStart();

            AudioClips.Instance.PlayClip(DictionarSounds.STR_AUDIO_CLIP_BUTTON_CLIKC);

            OnStartGameEvent?.Invoke();
        }

        private void ShowPanels(bool isActive)
        {
            _meinMenu.SetActive(!isActive);

            _gamePlay.SetActive(isActive);
            _gameManager.SetActive(isActive);
        }

        private void ExitGame()
        {
            AudioClips.Instance.PlayClip(DictionarSounds.STR_AUDIO_CLIP_BUTTON_CLIKC);
            Application.Quit();
            Debug.Log("Exit");
        }

        private void FadeAtStart()
        {
            _fadeImage.DOFade(0f, 1.3f).From(1f);
        }
    }
}