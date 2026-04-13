namespace Labb2_Threads_Async
{
    internal class RaceTrack
    {
        private const double TrackLength = 5000;
        private List<Car> _cars =
        [
            new("Audi"),
            new("Volvo"),
            new("Toyota"),
            new("Lådbil"),
        ];
        private List<string> _finished = [];

        // Begins the race by telling each car to start its own thread
        internal void StartRace()
        {
            foreach (var car in _cars)
            {
                Console.WriteLine($"{car.Name} startar!");
                car.Start(TrackLength);
            }

            ListenForInput();
            RaceLoop();
        }

        // Listens for user input to determine if race status should be shown
        private void ListenForInput()
        {
            Thread inputThread = new Thread(() =>
            {
                while (_finished.Count < _cars.Count)
                {
                    string? input = Console.ReadLine();
                    if (input == "" || input?.ToLower() == "status")
                    {
                        PrintStatus();
                    }
                }
            });
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        // Main loop for the race
        private void RaceLoop()
        {
            while (_finished.Count < _cars.Count)
            {
                Thread.Sleep(1000);

                foreach (var car in _cars)
                {
                    HandleCarFinish(car);
                }
            }

            Console.WriteLine("Alla bilar är i mål!");
        }

        // Checks if a car has finished the race and displays a message
        private void HandleCarFinish(Car car)
        {
            if (car.Distance >= TrackLength && !_finished.Contains(car.Name))
            {
                _finished.Add(car.Name);
                string placement = GetPlacement(_finished.Count);
                Console.WriteLine($"{placement} plats: {car.Name} har kommit i mål!");

                if (_finished.Count == 1)
                {
                    Console.WriteLine($"{car.Name} vinner!");
                }
            }
        }

        private string GetPlacement(int position) => position switch
        {
            1 => "1:a",
            2 => "2:a",
            _ => $"{position}:e"
        };

        // Prints the current status for each car in the race
        private void PrintStatus()
        {
            Console.WriteLine("\n--- Status ---");
            foreach (var car in _cars)
            {
                Console.WriteLine($"{car.Name}: {car.Distance:F0}m / {TrackLength}m | {car.Speed} km/h");
            }
            Console.WriteLine("--------------\n");
        }
    }
}
