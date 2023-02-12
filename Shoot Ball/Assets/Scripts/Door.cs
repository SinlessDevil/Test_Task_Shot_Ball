using UnityEngine;
using Extensions;

public class Door : MonoBehaviour
{
    [SerializeField] private HingeJoint _hingeJoint;
    [SerializeField] private TriggerSystem.DoorTrigger _doorArea;

    private void Awake(){
        LogErrorExtensions.LogError(_hingeJoint);
        LogErrorExtensions.LogError(_doorArea);
    }

    private void OnEnable(){
        _doorArea.OnOpenDoorEvent += OpenDoor;
    }

    private void OnDisable(){
        _doorArea.OnOpenDoorEvent -= OpenDoor;
    }

    private void OpenDoor(){
        SoundSystem.AudioClips.Instance.PlayClip(SoundSystem.DictionarSounds.STR_AUDIO_CLIP_OPEN_DOOR);
        JointSpring jointSpring = _hingeJoint.spring;
        jointSpring.targetPosition = -120f;
        _hingeJoint.spring = jointSpring;
    }
}