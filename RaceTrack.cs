namespace Labb2_Threads_Async
{
    internal class RaceTrack
    {
        static bool _running = true;

        internal void StartRace()
        {
            var cars = new List<Car>()
            {
                new("Audi"),
                new("Volvo"),
                new("Toyota"),
                new("Lådbil"),
            };

            foreach (var car in cars)
            {
                car.Start();
            }
        }
    }
}
