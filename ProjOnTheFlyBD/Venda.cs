using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class Venda
    {
        public int ID { get; set; }
        public string DataVenda { get; set; }
        public string Passageiro { get; set; }
        public double ValorTotal { get; set; }

        Banco conn = new Banco();
        ItemVenda item = new ItemVenda();

        public Venda()
        {
        }

        public override string ToString()
        {
            return $"{ID}{Passageiro}{DataVenda}{ValorTotal}";
        }

        public void GeraId()
        {
            for (int i = 1; i <= 999; i++)
            {
                if (!conn.VerificaDados(i.ToString(), "ID", "Venda"))
                {
                    ID = i;
                    break;
                }
            }
        }

        public void CadastrarVenda()
        {
            Console.WriteLine("VENDA DE PASSAGENS\nDigite 0 para sair");
            DataVenda = DateTime.Now.ToString("dd/MM/yyyy");

            string novoAssento, voo, query, aeronave, cpf, attAeronave, idPassagem, numPassagens, opcao, atualizaPass;
            int num, assento, capacidade;

            while (true)
            {
                Console.Write("Insira o CPF do comprador: ");
                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (cpf == "0") return;

                if (conn.VerificaDados(cpf, "CPF", "Restritos"))
                    Console.WriteLine("CPF Bloqueado!");
                else
                {
                    if (!conn.VerificaDados(cpf, "CPF", "Passageiro"))
                        Console.WriteLine("CPF não cadastrado!");
                    else
                        break;
                }
            }
            Passageiro = cpf;

            do
            {
                do
                {
                    Console.Write("Quantas passagens deseja comprar(max 4): ");
                    numPassagens = Console.ReadLine();
                    if (!int.TryParse(numPassagens, out num))
                        Console.WriteLine("Digite um valor válido");

                } while (!int.TryParse(numPassagens, out num));

            } while (num < 1 && num > 4);

            for (int i = 1; i <= num; i++)
            {
                while (true)
                {
                    Console.Write("Insira o ID da passagem: ");
                    idPassagem = Console.ReadLine().Replace(".", "").Replace("-", "");

                    if (idPassagem == "0") return;

                    if (!conn.VerificaDados(idPassagem, "ID", "Passagem Voo"))
                        Console.WriteLine("Passagem não encontrada!");
                    else
                        break;
                }
                aeronave = conn.GetIdAeronave(idPassagem);
                capacidade = conn.GetCapacidade(aeronave);
                voo = conn.GetVoo(idPassagem);
                assento = conn.GetAssentos(voo);

                if (assento <= capacidade)
                {
                    assento++;
                    novoAssento = $"UPDATE Voo SET AssentosOcupados = {assento} WHERE ID = '{voo}'";
                    conn.Update(novoAssento);
                }
                else
                {
                    Console.WriteLine("Voo não possui mais passagens disponíveis!");
                    return;
                }

                do
                {
                    Console.Write("Deseja vender[V] ou reservar[R]?: ");
                    opcao = Console.ReadLine().ToUpper();
                    if (opcao != "V" && opcao != "R")
                        Console.WriteLine("Dado inválido!");

                } while (opcao != "V" && opcao != "R");

                switch (opcao)
                {
                    case "V":
                        string queryy = $"UPDATE PassagemVoo SET Situacao = '{opcao}',DataUltimaOperacao = '{DataVenda}'  WHERE ID = '{idPassagem}';";
                        conn.Update(queryy);
                        break;

                    case "R":
                        string queryyy = $"UPDATE PassagemVoo SET Situacao = '{opcao}',DataUltimaOperacao = '{DataVenda}'  WHERE ID = '{idPassagem}';";
                        conn.Update(queryyy);
                        break;
                }

                attAeronave = $"UPDATE Aeronave SET UltimaVenda = '{DataVenda}' WHERE Inscricao = '{aeronave}'";
                conn.Update(attAeronave);

                ValorTotal += conn.GetValor(idPassagem);
                item.CadastraItemVenda(ID, idPassagem);
            }
            atualizaPass = $"UPDATE Passageiro SET UltimaCompra = '{DataVenda}' WHERE Cpf = '{Passageiro}'";
            conn.Update(atualizaPass);

            query = $"INSERT INTO Venda(ID, Cpf, DataVenda, TotalVendas) VALUES ({ID}, '{Passageiro}', {DataVenda}, '{ValorTotal}');";
            conn.Insert(query);

            Console.WriteLine("Passagens compradas/reservadas com sucesso");
            Thread.Sleep(2000);
        }

        public void ImprimeVenda()
        {
            int opcao = 9;
            Console.WriteLine(">>> Vendas Cadastrados <<<");

            string query = "SELECT * FROM Venda";
            conn.Select(query, opcao);
        }
    }
}
