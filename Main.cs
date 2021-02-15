using System;
using TestePleno.Controllers;
using TestePleno.Models;

namespace TestePleno
{
    class Program
    {
        static void Main(string[] args)
        {
            var fareController = new FareController();
            while (true)
            {
                Console.Clear();
                var fare = new Fare();
                fare.Id = Guid.NewGuid();

                Console.WriteLine("Informe o valor da tarifa a ser cadastrada:");
                var fareValueInput = Console.ReadLine();
                fare.Value = decimal.Parse(fareValueInput);

                Console.WriteLine("Informe o código da operadora para a tarifa:");
                Console.WriteLine("Exemplos: OP01, OP02, OP03...");
                var operatorCodeInput = Console.ReadLine();

                try
                {
                    fareController.CreateFare(fare, operatorCodeInput);
                    Console.WriteLine("Tarifa cadastrada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                Console.WriteLine("[Esc] Sair - [Enter] Continuar");
                var info = Console.ReadKey();
                if (info.Key == ConsoleKey.Escape)
                    break;
            }
        }
    }
}
