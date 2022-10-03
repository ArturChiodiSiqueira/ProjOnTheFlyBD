using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class ItemVenda
    {
        public int Id { get; set; }
        public string IdPassagem { get; set; }
        public double ValorUnitario { get; set; }
        public int IdVenda { get; set; }

        Banco conn = new Banco();

        public ItemVenda()
        {
        }

        public override string ToString()
        {
            return $"{Id}{IdPassagem}{ValorUnitario}{IdVenda}";
        }

        public void CadastraItemVenda(int idVenda, string idPassagem)
        {
            ValorUnitario = conn.GetValor(idPassagem);
            IdVenda = idVenda;
            IdPassagem = idPassagem;

            string query = $"INSERT INTO ItemVenda (IDVenda, IDPassagem, ValorUnitario) VALUES ('{IdVenda}', '{IdPassagem}', '{ValorUnitario}');";
            conn.Insert(query);

            ComprarItemVenda();
        }

        public string ComprarItemVenda()
        {
            int cont = 0;
            double valortotal = 0.0;
            valortotal += ValorUnitario;

            do
            {
                cont++;
                Console.Clear();

                Console.WriteLine("Compra realizada com sucesso!");
                Console.WriteLine("Voce comprou " + cont + " Passagens");

                Console.WriteLine("Deseja comprar novamente (s/n)?");
                string op = Console.ReadLine().ToLower();

                if (op != "s" && op != "n")
                    Console.WriteLine("Insira uma resposta valida! (s/n)");

                if (op == "n")
                    break;

            } while (cont < 4);
            if (cont == 4)
            {
                Console.WriteLine("Limite de passagens atingindo!\nPressione enter para continuar");
                Console.ReadKey();
            }
            return valortotal.ToString("0000000");
        }
    }
}