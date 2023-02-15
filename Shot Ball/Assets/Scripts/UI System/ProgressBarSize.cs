using UnityEngine;
using EntitySystem.PlayerSystem;
using Zenject;

namespace UISystem
{
    public class ProgressBarSize : MonoBehaviour
    {
        [SerializeField] private Transform _progressBar;
        private float _maxPlayerSize = 3.5f;

        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
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
            float progress = value / _maxPlayerSize;
            _progressBar.transform.localScale = new Vector3(progress, progress, progress);
        }
    }
}