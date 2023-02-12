using UnityEngine;
using FactorySystem;
using Extensions;

namespace ParticalFXSystem
{
    public class SpawnerFX : MonoBehaviour
    {
        [Header("--------- Pool Factory Setting ---------")]
        [SerializeField] private int _poolCount;
        [SerializeField] private bool _autoExpand;
        [SerializeField] private ExplosionFX _particalFXPrefabs;

        private PoolMono<ExplosionFX> _pool;
        public static SpawnerFX Instance;

        private void Awake()
        {
            LogErrorExtensions.LogError(_particalFXPrefabs);

            Instance = this;
        }

        private void Start()
        {
            _pool = new PoolMono<ExplosionFX>(_particalFXPrefabs, _poolCount, this.transform);
            _pool.autoExpand = _autoExpand;
        }

        public void CreatePacticalFX(Vector3 pos){
            var fx = this._pool.GetFreeElement();
            fx.transform.position = pos;
            fx.PlayParticalSystem();
        }
    }
}