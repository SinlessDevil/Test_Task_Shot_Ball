using UnityEngine;
using SoundSystem;

namespace UISystem
{
    [RequireComponent(typeof(Animator))]
    public class SettingsPanel : MonoBehaviour
    {
        private const string STR_STATE_PANEL = "IsState";

        private Animator _anim;
        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            UIButtons.ButtonClickOpenSettingsPanelEvent += OpenPanel;
            UIButtons.ButtonClickCloseSettingsPanelEvent += ClosePanel;
        }

        private void OnDisable()
        {
            UIButtons.ButtonClickOpenSettingsPanelEvent -= OpenPanel;
            UIButtons.ButtonClickCloseSettingsPanelEvent -= ClosePanel;
        }

        private void OpenPanel()
        {
            ShowPanel(true);
        }

        private void ClosePanel()
        {
            ShowPanel(false);
        }

        private void ShowPanel(bool isActive)
        {
            AudioClips.Instance.PlayClip(DictionarSounds.STR_AUDIO_CLIP_BUTTON_CLIKC);
            AudioClips.Instance.PlayClip(DictionarSounds.STR_AUDIO_CLIP_SHOWPANEL);
            _anim.SetBool(STR_STATE_PANEL, isActive);
        }
    }
}