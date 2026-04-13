namespace Labb2_Threads_Async
{
    internal class CarEvent
    {
        public int Weight { get; }
        public string? Message { get; }
        public Action<Car> Effect { get; }

        public CarEvent(int weight, string? message, Action<Car> effect)
        {
            Weight = weight;
            Message = message;
            Effect = effect;
        }
    }
}
