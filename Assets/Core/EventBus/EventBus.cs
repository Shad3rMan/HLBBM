namespace Core.EventBus
{
    public class EventBus
    {
        public GameEvent ElectricityOn = new GameEvent();
        public GameEvent ElectricityOff = new GameEvent();
        public GameEvent<int> ElevatorRequested = new GameEvent<int>();
    }
}