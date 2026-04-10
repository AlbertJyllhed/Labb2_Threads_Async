namespace Labb2_Threads_Async
{
    internal class Car
    {
        public string Name { get; private set; } = string.Empty;
        public int Speed { get; set; }

        private bool _running = false;
        private Random _random = new Random();

        public Car(string name)
        {
            Name = name;
            Speed = 120;
        }

        // Start the car thread
        public void Start()
        {
            Console.WriteLine($"{Name} startar!");
            _running = true;
            Thread thread = new Thread(Drive);
            thread.Start();
        }

        // Main driving loop
        private void Drive()
        {
            while (_running)
            {
                Thread.Sleep(10000);
                TriggerEvent();
            }
        }

        // Call a random event for the car
        private void TriggerEvent()
        {
            int eventChance = _random.Next(50);

            if (eventChance <= 1)
            {
                Console.WriteLine($"{Name}: Behöver tanka, stannar 15 sekunder");
                Pause(15);
            }
            else if (eventChance <= 2)
            {
                Console.WriteLine($"{Name}: Behöver byta däck, stannar 10 sekunder");
                Pause(10);
            }
            else if (eventChance <= 5)
            {
                Console.WriteLine($"{Name}: Behöver tvätta vindrutan, stannar 5 sekunder");
                Pause(5);
            }
            else if (eventChance <= 10)
            {
                Console.WriteLine($"{Name}: Hastigheten på bilen sänks med 1 km/h");
                DecreaseSpeed(1);
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
