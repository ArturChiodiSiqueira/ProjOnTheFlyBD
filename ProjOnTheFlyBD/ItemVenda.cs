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

        public void GeraId()
        {
            for (int i = 1; i <= 999; i++)
            {
                if (!conn.VerificaDados(i.ToString(), "ID", "ItemVenda"))
                {
                    Id = i;
                    break;
                }
            }
        }

        public void CadastraItemVenda(int idVenda, string idPassagem)
        {
            ValorUnitario = conn.GetValor(idPassagem);
            IdVenda = idVenda;
            IdPassagem = idPassagem;

            string query = $"INSERT INTO ItemVenda (IDVenda, IDPassagem, ValorUnitario) VALUES ('{IdVenda}', '{IdPassagem}', '{ValorUnitario}');";
            conn.Insert(query);
        }

        public void AlteraPassagem(int id, string passagemVoo)
        {
            Console.WriteLine(">>> ALTERANDO VOO <<<");
            string query = $"UPDATE ItemVenda SET IDPassagem = '{passagemVoo}' WHERE ID = {id}";
            conn.Update(query);

            Console.WriteLine(">>> ALTERANDO VALOR <<<");
            ValorUnitario = conn.GetValor(passagemVoo);
            string queryy = $"UPDATE ItemVenda SET ValorUnitario = '{ValorUnitario}' WHERE IDPassagem = {passagemVoo}";
            conn.Update(queryy);
        }

        public void ImprimiItemVenda(int id)
        {
            int opcao = 8;
            Console.WriteLine(">>> Itens Cadastrados <<<");

            string query = "SELECT * FROM ItemVenda";
            conn.Select(query, opcao);
        }
    }
}