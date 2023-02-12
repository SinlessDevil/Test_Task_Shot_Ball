using UnityEngine;

namespace EntitySystem
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class Obstacle : MonoBehaviour
    {
        [HideInInspector] public MeshRenderer obstacleMeshRender;

        private void Start(){
            obstacleMeshRender = GetComponent<MeshRenderer>();
        }
    }
}