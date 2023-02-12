using UnityEngine;
using Extensions;

namespace ParticalFXSystem
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ExplosionFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionFX;

        private void Awake(){
            LogErrorExtensions.LogError(_explosionFX);
        }

        private void OnEnable(){
            StartCoroutine(ReloudParticalEffectRoutine());
        }

        public void PlayParticalSystem(){
            _explosionFX.Play();
        }

        private System.Collections.IEnumerator ReloudParticalEffectRoutine(){
            yield return new WaitForSeconds(2f);
            transform.Deactivate();
        }
    }
}