using UnityEngine;

public class ProgressBarSize : MonoBehaviour
{
    [SerializeField] private Transform _progressBar;
    [SerializeField] private EntitySystem.PlayerSystem.Player _player;
    private float _maxPlayerSize = 3.5f;

    private void Awake()
    {
        Extensions.LogErrorExtensions.LogError(_player);
        Extensions.LogErrorExtensions.LogError(_progressBar);
    }


    private void OnEnable()
    {
        _player.OnChangeFillBarSizeEvent += UpdateProgressBar;
    }

    private void OnDisable()
    {
        _player.OnChangeFillBarSizeEvent += UpdateProgressBar;
    }

    private void UpdateProgressBar(float value)
    {      
        float progress = value/_maxPlayerSize;
        _progressBar.transform.localScale = new Vector3(progress, progress, progress);
    }
}