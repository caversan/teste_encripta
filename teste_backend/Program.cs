using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using ExtensionMethods;

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

        static void Main()
        {
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
var orderedCities = (from city in cities.AsParallel().AsOrdered()
                     where city.Population > 10000
                     select city)
                    .Take(1000);*/

            // foreach (var Pessoa in orderedPesssoas)
            // {
            //     Console.WriteLine("{0} {1} {2} {3}", Pessoa.Nome, Pessoa.Sobrenome, String.Format("{0:dd/MM/yyyy}", Pessoa.DataNascimento), Pessoa.Empresa.Nome);
            // }
            Console.WriteLine("");
            Parallel.ForEach(orderedPesssoas, Pessoa => {
                Console.WriteLine("{0} {1} {2} {3}", Pessoa.Nome, Pessoa.Sobrenome, String.Format("{0:dd/MM/yyyy}", Pessoa.DataNascimento), Pessoa.Empresa.Nome);
            });
            Console.WriteLine("");
            string s = "Hello Extension Methods";
int i = s.WordCount();
            
        }
    }
}


