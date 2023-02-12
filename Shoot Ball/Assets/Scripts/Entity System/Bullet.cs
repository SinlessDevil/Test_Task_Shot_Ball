using System;
using System.Collections;
using UnityEngine;
using Extensions;

namespace EntitySystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : Entity
    {
        public event Action<Obstacle> OnDetectionObstacleEvent;
        public event Action OnContaminationObstacleEvent;

        [SerializeField] private Transform _body;
        [SerializeField] private Transform _areaBodyDetection;
        [SerializeField] private SphereCollider _detectionRange;
        private float currentSizeDetection;

        [HideInInspector] public Rigidbody rb;

        private const float WAIT_TIME_TO_CONTAMINATION = 0.5f;
        private const float WAIT_TIME_TO_DESTROY = 10f;

        private void Awake()
        {
            LogErrorExtensions.LogError(_body);
            LogErrorExtensions.LogError(_areaBodyDetection);
            LogErrorExtensions.LogError(_detectionRange);
        }

        protected override void Start(){
            _currentSize = _minSize;
            currentSizeDetection = 2f;

            rb = GetComponent<Rigidbody>();
            StartCoroutine(DestroyGameObjectRoutine());
        }

        public override void ApplyChangeSize(float value){
            _currentSize += value;
            gameObject.transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);
            _detectionRange.radius += value * 3;
            currentSizeDetection += value * 6;
            _areaBodyDetection.localScale = new Vector3(currentSizeDetection, currentSizeDetection, currentSizeDetection);
        }

        private void OnTriggerEnter(Collider other){
            if (other.gameObject.TryGetComponent(out Obstacle obstacle)){
                OnDetectionObstacleEvent?.Invoke(obstacle);
            }
        }

        private void OnCollisionEnter(Collision collision){
            if(collision.gameObject.TryGetComponent(out Obstacle obstacle)){
                _detectionRange.enabled = true;
                rb.isKinematic = true;
                _body.Deactivate();

                SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_DESTROY_OBJECT);
                ParticalFXSystem.SpawnerFX.Instance.CreatePacticalFX(collision.contacts[0].point);

                StartCoroutine(ContaminationRoutine());
            }
        }

        private IEnumerator ContaminationRoutine(){
            yield return new WaitForSeconds(WAIT_TIME_TO_CONTAMINATION);
            OnContaminationObstacleEvent?.Invoke();
        }

        private IEnumerator DestroyGameObjectRoutine(){
            yield return new WaitForSeconds(WAIT_TIME_TO_DESTROY);
            Destroy(gameObject);
        }
    }
}