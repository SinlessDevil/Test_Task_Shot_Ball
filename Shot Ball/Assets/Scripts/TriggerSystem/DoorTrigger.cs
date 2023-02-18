namespace TriggerSystem
{
    public class DoorTrigger : AbstractTriggerArea
    {
        public event System.Action OnOpenDoorEvent;
        public override void OnEventActive()
        {
            OnOpenDoorEvent?.Invoke();
        }
    }
}