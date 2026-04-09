namespace Labb2_Threads_Async
{
    internal class Car
    {
        public string Name { get; private set; } = string.Empty;
        public int Speed { get; set; }

        private Random _random = new Random();
        private bool _running = false;

        public Car(string name)
        {
            Name = name;
            Speed = 120;

            Thread thread = new Thread(Drive);
            thread.Start();
        }

        private void Drive()
        {
            while (_running)
            {
                int eventChance = _random.Next(50);

                if (eventChance == 1)
                {
                    Pause(15);
                }
                else if (eventChance <= 2)
                {
                    Pause(10);
                }
                else if (eventChance <= 5)
                {
                    Pause(5);
                }
                else if (eventChance <= 10)
                {
                    DecreaseSpeed(1);
                }

                Thread.Sleep(100);
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
