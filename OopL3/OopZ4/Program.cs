using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopZ4
{
    public class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Square
    {
        public int Size { get; set; }        
    }

    public class AreaCalculator
    {
        public int CalculateArea(Rectangle rect)
        {
            return rect.Width * rect.Height;
        }

        // lub zmienic Square i Rectangle zeby byli sami odpowiedzialni za liczenie
        // swojego pola powierzchni
        public int CalculateArea(Square square)
        {
            return square.Size * square.Size;
        }
    }    

    class Program
    {
        static void Main(string[] args)
        {
            const int w = 4;
            const int h = 4;

            //propozycja nr 1 - usuniecie relacji dziedziczenia
            var square = new Square { Size = w };
            var calc = new AreaCalculator();
            Console.WriteLine("Kwadrat o wymiarze {0} ma pole {1}", square.Size, calc.CalculateArea(square));

            //propozycja nr 2 - usuniecie Square w ogole - nie dodaje zadnej konkretnej funkcjonalnosci do Rectangle
            var rectangle = new Rectangle() {Height = h, Width = w};
            Console.WriteLine("Prostokąt o wymiarach {0} na {1} ma pole {2}", rectangle.Width, rectangle.Height, calc.CalculateArea(rectangle));
        }
    }
}