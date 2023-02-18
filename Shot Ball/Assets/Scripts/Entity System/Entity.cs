using UnityEngine;
using UniRx;

namespace EntitySystem
{
    public abstract class Entity : MonoBehaviour
    {
        [HideInInspector] public ReactiveCommand<float> OnSizeTransferCommand = new();

        [Header("---- Setting Size ----")]
        [SerializeField] protected float _maxSize;
        [SerializeField] protected float _minSize;
        protected float _currentSize;

        protected virtual void Start(){
            _currentSize = _maxSize;
        }

        public abstract void ApplyChangeSize(float value);
    }
}