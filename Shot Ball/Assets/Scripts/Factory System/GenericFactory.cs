using UnityEngine;
using Extensions;

namespace FactorySystem
{
    public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _pointToSpawn;

        private void Awake()
        {
            LogErrorExtensions.LogError(_prefab);
            LogErrorExtensions.LogError(_pointToSpawn);
        }

        public T GetNewInstance()
        {
            Vector3 pos = _pointToSpawn.position;
            return Instantiate(_prefab,pos,Quaternion.identity,_pointToSpawn.parent);
        }
    }
}