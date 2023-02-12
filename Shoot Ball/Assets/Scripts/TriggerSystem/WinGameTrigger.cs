namespace TriggerSystem
{
    public class WinGameTrigger : AbstractTriggerArea
    {
        public event System.Action OnWinGameEvent;
        public override void OnEventActive()
        {
            OnWinGameEvent?.Invoke();
        }
    }
}