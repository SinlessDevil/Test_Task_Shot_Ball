using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Extensions;

namespace UISystem
{
    public class UIButtons : MonoBehaviour
    {
        public static event Action ButtonClickExitEvent;
        public static event Action ButtonClickSoundEvent;

        public static event Action ButtonClickPlayGameEvent;

        public static event Action ButtonClickTryEvent;
        public static event Action ButtonClickOpenSettingsPanelEvent;

        public static event Action ButtonClickCloseSettingsPanelEvent;

        public static event Action ButtonClickRestartOnWinGamePanelEvent;

        [Header("----------------------- MeinMenu Buttons -----------------------")]
        [SerializeField] private Button _buttonPlayGame;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonSound;
        [Space(10)]
        [Header("----------------------- GamePlay Buttons -----------------------")]
        [Header("--- Buttons on Front ---")]
        [SerializeField] private Button _buttonTry;
        [SerializeField] private Button _buttonOpenSettingsPanel;
        [Space]
        [Header("--- Buttons on Setting Panel ---")]
        [SerializeField] private Button _buttonSoundOnSettingsPanel;
        [SerializeField] private Button _buttonExitOnSettingsPanel;
        [SerializeField] private Button _buttonCloseSettingsPanel;
        [Space]
        [Header("--- Buttons on WinGame Panel ---")]
        [SerializeField] private Button _buttonExitOnWinGamePanel;
        [SerializeField] private Button _buttonRestartOnWinGamePanel;

        private void Awake()
        {
            SetButtonClickSetting(_buttonPlayGame, OnPlayGameButtonClick);
            SetButtonClickSetting(_buttonExit, OnExitButtonClick);
            SetButtonClickSetting(_buttonSound, OnSoundButtonClick);

            SetButtonClickSetting(_buttonTry, OnTryButtonClick);
            SetButtonClickSetting(_buttonOpenSettingsPanel, OnOpenSettingsPanelButtonClick);
            SetButtonClickSetting(_buttonSoundOnSettingsPanel, OnSoundButtonClick);
            SetButtonClickSetting(_buttonExitOnSettingsPanel, OnExitButtonClick);
            SetButtonClickSetting(_buttonCloseSettingsPanel, OnCloseSettingsPanelButtonClick);

            SetButtonClickSetting(_buttonExitOnWinGamePanel, OnExitButtonClick);
            SetButtonClickSetting(_buttonRestartOnWinGamePanel, OnRestartOnWinGamePanelButtonClick);
        }

        private void SetButtonClickSetting(Button button, UnityAction call)
        {
            button.RemoveAllListeners();
            button.AddListener(call);
        }

        private void OnExitButtonClick() => ButtonClickExitEvent?.Invoke();
        private void OnPlayGameButtonClick() => ButtonClickPlayGameEvent?.Invoke();
        private void OnSoundButtonClick() => ButtonClickSoundEvent?.Invoke();
        private void OnTryButtonClick() => ButtonClickTryEvent?.Invoke();
        private void OnOpenSettingsPanelButtonClick() => ButtonClickOpenSettingsPanelEvent?.Invoke();
        private void OnCloseSettingsPanelButtonClick() => ButtonClickCloseSettingsPanelEvent?.Invoke();
        private void OnRestartOnWinGamePanelButtonClick() => ButtonClickRestartOnWinGamePanelEvent?.Invoke();
    }
}