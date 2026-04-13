namespace Labb2_Threads_Async
{
    internal class Car
    {
        public string Name { get; private set; } = string.Empty;
        public int Speed { get; set; }
        public double Distance { get; private set; } = 0;

        private Random _random = new Random();
        private static readonly List<CarEvent> _events =
        [
            new(2, "Behöver tanka, stannar 15 sekunder", car => car.Pause(15)),
            new(3, "Behöver byta däck, stannar 10 sekunder", car => car.Pause(10)),
            new(8, "Behöver tvätta vindrutan, stannar 5 sek", car => car.Pause(5)),
            new(15, "Hastigheten sänks med 1 km/h", car => car.DecreaseSpeed(1)),
            new(72, null, _ => {}), // Nothing happens
        ];

        public Car(string name)
        {
            Name = name;
            Speed = 120;
        }

        // Start the car thread
        public void Start(double trackLength)
        {
            Thread thread = new Thread(() => Drive(trackLength));
            thread.Start();
        }

        // Main driving loop
        private void Drive(double trackLength)
        {
            while (Distance < trackLength)
            {
                Thread.Sleep(10000);
                Distance += Speed / 3.6 * 10; // Convert km/h to m/s times 10 seconds
                TriggerEvent();
            }
        }

        // Call a random event for the car
        private void TriggerEvent()
        {
            int roll = _random.Next(_events.Sum(e => e.Weight));
            int weightTotal = 0;

            foreach (var ev in _events)
            {
                weightTotal += ev.Weight;
                if (roll < weightTotal)
                {
                    if (ev.Message != null)
                    {
                        Console.WriteLine($"{Name}: {ev.Message}");
                    }
                    ev.Effect(this);
                    return;
                }
            }
        }

        private void Pause(int delayInSeconds)
        {
            Thread.Sleep(delayInSeconds * 1000);
        }

        private void DecreaseSpeed(int decreaseAmount)
        {
            Speed = Math.Max(Speed - decreaseAmount, 0);
        }
    }
}
