using UnityEngine;
using EntitySystem.PlayerSystem;
using Zenject;

public class GameOver : MonoBehaviour
{
    private Player _player;
    private const float WAIT_TIME = 2f;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        Extensions.LogErrorExtensions.LogError(_player);
    }

    private void OnEnable()
    {
        _player.OnGameOverEvent += RestartGame;
    }

    private void OnDisable()
    {
        _player.OnGameOverEvent -= RestartGame;
    }

    private void RestartGame()
    {
        StartCoroutine(RestartGameRoutine());
    }

    private System.Collections.IEnumerator RestartGameRoutine()
    {
        SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_DESTROY_OBJECT);
        SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_GAME_OVER);

        yield return new WaitForSeconds(WAIT_TIME);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}