using UnityEngine;
using DG.Tweening;
using Extensions;
using EntitySystem.PlayerSystem;
using Zenject;

public class WinGame : MonoBehaviour
{
    [Header("---- Componnet to Win Game ----")]
    [SerializeField] private TriggerSystem.WinGameTrigger _winGameTrigger;
    [Space(5)]
    [SerializeField] private ParticleSystem _magicEffect;
    [Space(5)]
    [SerializeField] private GameObject _uiWinGame;
    [SerializeField] private GameObject _panelWinGame;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake(){
        LogErrorExtensions.LogError(_winGameTrigger);
        LogErrorExtensions.LogError(_magicEffect);
        LogErrorExtensions.LogError(_uiWinGame);
        LogErrorExtensions.LogError(_panelWinGame);
    }

    private void OnEnable(){
        _winGameTrigger.OnWinGameEvent += SetWinGame;
        UISystem.UIButtons.ButtonClickRestartOnWinGamePanelEvent += RestartGame;
    }

    private void OnDisable(){
        _winGameTrigger.OnWinGameEvent -= SetWinGame;
        UISystem.UIButtons.ButtonClickRestartOnWinGamePanelEvent -= RestartGame;
    }

    private void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void SetWinGame(){
        _player.transform.Deactivate();
        SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_WIN_GAME);
        _uiWinGame.SetActive(true);
        AnimationWinPanel();
    }

    private void AnimationWinPanel(){
        DOTween.Sequence()
            .AppendInterval(1f)
            .Append(_panelWinGame.transform.DOScale(1.1f, 2f))
            .Append(_panelWinGame.transform.DOScale(0.9f, 0.5f))
            .Append(_panelWinGame.transform.DOScale(1f, 0.5f))
            .AppendCallback(PlayEffect);
    }

    private void PlayEffect(){
        SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_SOUND_WIN_EFFECT);
        _magicEffect.Play();
    }
}