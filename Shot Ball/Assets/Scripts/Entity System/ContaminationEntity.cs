using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FactorySystem;
using UniRx;
using Zenject;

namespace EntitySystem
{
    public class ContaminationEntity : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [SerializeField] private Color _colorContamination;
        [SerializeField] private float _speedContamination;

        private List<Obstacle> _obstacles = new();
        private BulletFactory _bulletFactory;
        private Bullet _currentÑontaminator;

        [Inject]
        private void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        private void OnEnable(){
            _bulletFactory.OnÑontaminatorActiveEvent += SetCurrentÑontaminator;
        }

        private void OnDisable(){
            _bulletFactory.OnÑontaminatorActiveEvent -= SetCurrentÑontaminator;

            if(_currentÑontaminator != null)
            {
                _currentÑontaminator.OnDetectionObstacleEvent -= AddContamination;
                _currentÑontaminator.OnContaminationObstacleEvent -= StartContaminationCommand;
            }

            _disposable.Clear();
        }

        private void SetCurrentÑontaminator(Bullet contaminator)
        {
            _currentÑontaminator = contaminator;
 
            _currentÑontaminator.OnDetectionObstacleEvent += AddContamination;
            _currentÑontaminator.OnContaminationObstacleEvent += StartContaminationCommand;
        }

        private void AddContamination(Obstacle obstacle){
            _obstacles.Add(obstacle);
        }

        private void StartContaminationCommand()
        {
            StartCoroutine(ContaminationCommand());
        }

        private IEnumerator ContaminationCommand()
        {
            _currentÑontaminator.OnDetectionObstacleEvent -= AddContamination;
            _currentÑontaminator.OnContaminationObstacleEvent -= StartContaminationCommand;

            foreach (var obstacle in _obstacles)
            {
                StartCoroutine(SubscribeContamination(obstacle));
                yield return new WaitForSeconds(0.1f);
            }
            _obstacles.Clear();
        }

        private IEnumerator SubscribeContamination(Obstacle obstacles){
            CompositeDisposable _currentdisposable = new();

            Observable.EveryUpdate().Subscribe(value =>{
                obstacles.obstacleMeshRender.material.color = Color.Lerp(obstacles.obstacleMeshRender.material.color, _colorContamination, _speedContamination);
            }).AddTo(_disposable).AddTo(_currentdisposable);

            yield return new WaitForSeconds(1f);

            _currentdisposable.Clear();

            SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_DESTROY_OBJECT);
            ParticalFXSystem.SpawnerFX.Instance.CreatePacticalFX(obstacles.gameObject.transform.position);

            obstacles.gameObject.SetActive(false);
        }
    }
}