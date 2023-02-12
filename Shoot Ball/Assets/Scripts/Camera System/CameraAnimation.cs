using UISystem;
using UnityEngine;

namespace CameraSystem
{
    [RequireComponent(typeof(Animator))]
    public class CameraAnimation : MonoBehaviour
    {
        private const string STR_START_GAME = "Start_Game";

        private Animator _anim;

        private void Start(){
            _anim = GetComponent<Animator>();
        }

        private void OnEnable(){
            MeinMenu.OnStartGameEvent += StartAnimation;
        }

        private void OnDisable(){
            MeinMenu.OnStartGameEvent -= StartAnimation;
        }

        private void StartAnimation(){
            _anim.SetTrigger(STR_START_GAME);
        }
    }
}