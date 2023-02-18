using System.Collections.Generic;
using UnityEngine;

namespace FactorySystem{
    public class PoolMono<T> where T : MonoBehaviour{
        public T prefab { get; }
        public bool autoExpand { get; set; }
        public Transform container { get; }
        private List<T> _pool;

        public PoolMono(T prefab, int count){
            this.prefab = prefab;
            container = null;

            CreatePool(count);
        }
        public PoolMono(T prefab, int count, Transform container){
            this.prefab = prefab;
            this.container = container;

            CreatePool(count);
        }

        private void CreatePool(int count){
            _pool = new List<T>();

            for (int i = 0; i < count; i++){
                CreateObject();
            }
        }
        private T CreateObject(bool isActiveByDefault = false){
            var createdObject = UnityEngine.Object.Instantiate(prefab, container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element){
            foreach (var mono in _pool){
                if (!mono.gameObject.activeInHierarchy){
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }
        public T GetFreeElement(){
            if (HasFreeElement(out var element)){
                return element;
            }
            if (autoExpand){
                return CreateObject(true);
            }
            return null;
            // throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }
    }
}