using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using CustomExtensions;

namespace CustomExtensions
{
    // Extension methods must be defined in a static class.
    public static class StringExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}

namespace teste_encripta
{
    class Program
    {


        public class Empresa
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
        public class Pessoa
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public DateTime DataNascimento { get; set; }
            public Empresa Empresa { get; set; }
        }

        //Iterators
        public class Weeks //Aggregate object
        {
            private string[] weeks = new string[]{
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
                };

            public IWeeksIterator GetWeeksIterator()
            {
                //creates an Iterator object
                return new WeeksIterator(weeks);
            }
        }

        public interface IWeeksIterator //Iterator interface
        {
            string Current { get; }

            bool MoveNext();
        }

        public class WeeksIterator : IWeeksIterator //Iterator object
        {
            private readonly string[] weeks;
            private int position;

            public WeeksIterator(string[] weeks)
            {
                this.weeks = weeks;
                this.position = -1;
            }

            public string Current => weeks[position];

            public bool MoveNext()
            {
                if (++position == weeks.Length) return false;
                return true;
            }
        }

        //Flyweight

        interface IShape
        {
            void Print();
        }
        class Rectangle : IShape
        {
            public void Print()
            {
                Console.WriteLine("Printing Rectangle");
            }
        }

        class Circle : IShape
        {
            public void Print()
            {
                Console.WriteLine("Printing Circle");
            }
        }
        class ShapeObjectFactory
        {
            Dictionary<string, IShape> shapes = new Dictionary<string, IShape>();

            public int TotalObjectsCreated
            {
                get { return shapes.Count; }
            }

            public IShape GetShape(string ShapeName)
            {
                IShape shape = null;
                if (shapes.ContainsKey(ShapeName))
                {
                    shape = shapes[ShapeName];
                }
                else
                {
                    switch (ShapeName)
                    {
                        case "Rectangle":
                            shape = new Rectangle();
                            shapes.Add("Rectangle", shape);
                            break;
                        case "Circle":
                            shape = new Circle();
                            shapes.Add("Circle", shape);
                            break;
                        default:
                            throw new Exception("Factory cannot create the object specified");
                    }
                }
                return shape;
            }
        }


        static void Main()
        {
            //Respostas para questão 1 e 2
            List<Empresa> Empresas = new List<Empresa>()
            {
                new Empresa { Id=1, Nome="Encripta" },
                new Empresa { Id=2, Nome="Nao Encripta" },
            };
            List<Pessoa> Pessoas = new List<Pessoa>()
            {
                new Pessoa { Id=2, Nome="Bdriano", Sobrenome="Daversan", DataNascimento=new DateTime(1976, 04, 02), Empresa=Empresas[1]},
                new Pessoa { Id=3, Nome="Cdriano", Sobrenome="Vaversan", DataNascimento=new DateTime(1976, 02, 02), Empresa=Empresas[1]},
                new Pessoa { Id=4, Nome="Ddriano", Sobrenome="Naversan", DataNascimento=new DateTime(1976, 01, 02), Empresa=Empresas[1]},
                new Pessoa { Id=1, Nome="Adriano", Sobrenome="Caversan", DataNascimento=new DateTime(1976, 07, 02), Empresa=Empresas[0]},
            };

            var orderedPesssoas = Pessoas.OrderByDescending(Pessoa => Pessoa.Sobrenome).ToList();

            //var orderedPesssoas = (from Pessoa in Pessoas.AsParallel().AsOrdered() orderby Pessoa.Sobrenome descending select Pessoa);


            /*
            foreach (var Pessoa in orderedPesssoas)
            {
                Console.WriteLine("{0} {1} {2} {3}", Pessoa.Nome, Pessoa.Sobrenome, String.Format("{0:dd/MM/yyyy}", Pessoa.DataNascimento), Pessoa.Empresa.Nome);
            }
            */
            Console.WriteLine("Questões 1 and 2");
            Parallel.ForEach(orderedPesssoas, Pessoa =>
            {
                Console.WriteLine("{0} {1} {2} {3}", Pessoa.Nome, Pessoa.Sobrenome, String.Format("{0:dd/MM/yyyy}", Pessoa.DataNascimento), Pessoa.Empresa.Nome);
            });
            Console.WriteLine("");

            //Questão 3
            Console.WriteLine("Questão 3 Iterator");
            var weeks = new Weeks();
            var iterator = weeks.GetWeeksIterator();
            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.Current);
            }
            Console.WriteLine("");


            Console.WriteLine("Questão 3 Flyweight");
            //Flyweight
            ShapeObjectFactory sof = new ShapeObjectFactory();

            IShape shape = sof.GetShape("Rectangle");
            shape.Print();
            shape = sof.GetShape("Rectangle");
            shape.Print();
            shape = sof.GetShape("Rectangle");
            shape.Print();

            shape = sof.GetShape("Circle");
            shape.Print();
            shape = sof.GetShape("Circle");
            shape.Print();
            shape = sof.GetShape("Circle");
            shape.Print();

            int NumObjs = sof.TotalObjectsCreated;
            Console.WriteLine("\nTotal No of Objects created = {0}", NumObjs);

            //Questão 10 Extension Methods
            Console.WriteLine("");
            Console.WriteLine("Questão 4 Extension Methods");

            // Import the extension method namespace.

            string s = "The quick brown fox jumped over the lazy dog.";
            // Call the method as if it were an
            // instance method on the type. Note that the first
            // parameter is not specified by the calling code.
            int i = s.WordCount();
            System.Console.WriteLine("Word count of s is {0}", i);

            //Questão 10
            Console.WriteLine("");
            Console.WriteLine("Questão 10 Algoritimo fatorial");
            int fatorial = 1;
            for (int n = 1; n <= 10; n++)
            {
                fatorial *= n;
                Console.WriteLine(n + " fatorial= " + fatorial);
            }

        }
    }
}


