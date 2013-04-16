namespace Zad3
{
    public class Plane
    {
        public Plane(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public uint Passengers;
    }
}