using UnityEngine;
using Extensions;

namespace TriggerSystem
{
    public abstract class AbstractTriggerArea : MonoBehaviour
    {
        [SerializeField] protected BoxCollider _triggerArea;

        private void Awake()
        {
            LogErrorExtensions.LogError(_triggerArea);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out EntitySystem.PlayerSystem.Player player))
            {
                OnEventActive();
            }
        }
        public abstract void OnEventActive();
    }
}