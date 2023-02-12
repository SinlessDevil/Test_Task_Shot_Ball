using UnityEngine;

namespace Extensions{
    public static class TransformExtensions{
        public static void Destroy(this Transform transform){
            Object.Destroy(transform.gameObject);
        }

        public static void Activate(this Transform transform){
            transform.gameObject.SetActive(false);
        }

        public static void Deactivate(this Transform transform){
            transform.gameObject.SetActive(false);
        }
    }
}