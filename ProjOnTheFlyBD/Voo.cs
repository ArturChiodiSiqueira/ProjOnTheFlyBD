using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoOnTheFlyBD
{
    internal class Voo
    {
        public string Id { get; set; }
        public string Destino { get; set; }
        public string IdAeronave { get; set; }
        public string DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }
        public int AssentosOcupados { get; set; }

        Banco conn = new Banco();

        public Voo()
        {
        }

        public override string ToString()
        {
            return $"{Id}{Destino}{IdAeronave}{DataVoo}{DataCadastro}{Situacao}";
        }

        public bool BuscarIata()
        {
            do
            {
                Console.Write("Informe a IATA do aeroporto de destino (XXX): ");
                Destino = Console.ReadLine().ToUpper();

                if (conn.VerificaDados(Destino, "Iata", "Aeroporto"))
                {
                    Console.WriteLine("Aeroporto encontrado!");
                    Thread.Sleep(2000);
                    return true;
                }
                else
                {
                    Console.WriteLine("Aeroporto não encontrado!");
                    Destino = "";
                    Thread.Sleep(2000);
                }
            } while (Destino.Length == 0);
            return false;
        }

        public bool BuscarAeronave()
        {
            do
            {
                Console.Write("Informe a inscrição da aeronave que irá realizar o voo: ");
                IdAeronave = Console.ReadLine().ToUpper();

                if (conn.VerificaDados(IdAeronave, "Inscricao", "Aeronave"))
                {
                    Console.WriteLine("Aeronave encontrada!");
                    Thread.Sleep(2000);
                    return true;
                }
                else
                {
                    Console.WriteLine("Aeronave não encontrada!");
                    IdAeronave = "";
                    Thread.Sleep(2000);
                }
            } while (IdAeronave.Length == 0);
            return false;
        }

        public void CadastrarVoo()
        {
            Console.WriteLine(">>> CADASTRO DE VOO <<<");

            BuscarIata();

            BuscarAeronave();

            if (!GerarIdVoo())
                return;

            Console.Write("Informe a data de partida do voo (dd/MM/yyyy): ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data de partida do voo (dd/MM/yyyy): ");
            }

            Console.Write("Informe a hora de partida do voo (HH:mm): ");
            DateTime horaVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out horaVoo))
            {
                Console.Write("Informe a hora de partida do voo (HH:mm): ");
            }

            DataVoo = dataVoo.ToString("dd-MM-yyyy") + horaVoo.ToString(" HH:mm");

            DataCadastro = DateTime.Now;

            Situacao = "A";

            AssentosOcupados = 0;

            string query = $"INSERT INTO Voo(ID, Inscricao, Destino, DataVoo, DataCadastro, AssentosOcupados, Situacao) VALUES('{Id}','{IdAeronave}','{Destino}','{DataVoo}','{DataCadastro}','{AssentosOcupados}','{Situacao}');";
            conn.Insert(query);

            Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\n");
            //Console.ReadKey();

            int capacidade = 0;
            capacidade = conn.GetCapacidade(IdAeronave);

            float valorPas;
            string valor;
            do
            {
                Console.Write("Digite o valor da Passagem: ");
                valor = Console.ReadLine().Replace(".", "").Replace(",", "");
                if (!float.TryParse(valor, out valorPas))
                    Console.WriteLine("Valor inválido!");
            } while (!float.TryParse(valor, out valorPas));

            for (int passagens = 1; passagens <= capacidade; passagens++)
            {
                PassagemVoo passagem = new();
                passagem.GerarPassagem(Id, valorPas);
            }
            Console.WriteLine("Passagens geradas com sucesso!");
            Thread.Sleep(2000);

            Console.ReadKey();
        }

        public bool GerarIdVoo()
        {
            Random random = new Random();
            Id = "V" + random.Next(0001, 9999).ToString("0000");

            if (conn.VerificaDados(Id, "ID", "Voo"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AlteraSituacao()
        {
            string num;
            do
            {
                Console.Write("Alterar Situação [A] Ativo / [C] Inativo / [0] Cancelar: ");
                num = Console.ReadLine().ToUpper();
                if (num != "A" && num != "C" && num != "0")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (num != "A" && num != "C" && num != "0");

            if (num.Contains("0"))
                return false;
            Situacao = num;
            return true;
        }

        public void ImprimeVoo()
        {
            int opcao = 6;
            Console.WriteLine(">>> Voos Cadastrados <<<");

            string query = "SELECT * FROM Voo";
            conn.Select(query, opcao);
        }

        public void AlteraDadoVoo()
        {
            bool verificacao;
            string id;
            Console.WriteLine(">>> ALTERAR DADOS DE VOO <<<\nPara sair digite 'S'.\n");
            Console.Write("Digite o ID do voo: ");
            id = Console.ReadLine().ToUpper().Trim();

            int opcao = 6;

            string query = $"SELECT ID,Inscricao,Destino,DataVoo,DataCadastro,Situacao FROM Voo WHERE ID = '{id}'";

            verificacao = conn.Select(query, opcao);

            if (verificacao)
            {
                string num;
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>> ALTERAR DADOS DE VOO <<<");
                    Console.Write("Para alterar digite:\n\n[1] Destino\n[2] Aeronave\n[3] Situação do Cadastro\n[0] Sair\nOpção: ");
                    num = Console.ReadLine();

                    if (num != "1" && num != "2" && num != "3" && num != "0")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(3000);
                    }

                } while (num != "1" && num != "2" && num != "3" && num != "0");

                if (num.Contains("0"))
                    return;

                switch (num)
                {
                    case "1":
                        if (!BuscarIata())
                            return;
                        query = $"UPDATE Voo SET Destino = '{Destino}' where ID = '{id}'";
                        conn.Update(query);

                        query = $"SELECT ID,Inscricao,Destino,DataVoo,DataCadastro,Situacao FROM Voo WHERE ID = '{id}'";
                        conn.Select(query, opcao);
                        break;

                    case "2":
                        if (!BuscarAeronave())
                            return;
                        query = $"UPDATE Voo SET Inscricao = '{IdAeronave}' where ID = '{id}'";
                        conn.Update(query);

                        query = $"SELECT ID,Inscricao,Destino,DataVoo,DataCadastro,Situacao FROM Voo WHERE ID = '{id}'";
                        conn.Select(query, opcao);
                        break;

                    case "3":
                        if (!AlteraSituacao())
                            return;
                        query = $"UPDATE Voo SET Situacao = '{Situacao}' where ID = '{id}'";
                        conn.Update(query);

                        query = $"SELECT ID,Inscricao,Destino,DataVoo,DataCadastro,Situacao FROM Voo WHERE ID = '{id}'";
                        conn.Select(query, opcao);
                        break;
                }
                Console.WriteLine("Cadastro alterado com sucesso!");
                Thread.Sleep(3000);
            }
        }


        //public void ImprimeVoos()
        //{
        //    string[] lines = File.ReadAllLines(Caminho);
        //    List<string> voos = new();

        //    for (int i = 1; i < lines.Length; i++)
        //    {
        //        if (lines[i].Substring(33, 1).Contains("A"))
        //            voos.Add(lines[i]);
        //    }

        //    for (int i = 0; i < voos.Count; i++)
        //    {
        //        string op;
        //        do
        //        {
        //            Console.Clear();
        //            Console.WriteLine(">>> Cadastro Voos <<<\nDigite para navegar:\n[1] Próximo Cadasatro\n[2] Cadastro Anterior" +
        //                "\n[3] Último cadastro\n[4] Voltar ao Início\n[0] Sair\n");

        //            Console.WriteLine($"Cadastro [{i + 1}] de [{voos.Count}]");

        //            ImprimeVoo(Caminho, voos[i].Substring(0, 5));

        //            Console.Write("Opção: ");
        //            op = Console.ReadLine();

        //            if (op != "0" && op != "1" && op != "2" && op != "3" && op != "4")
        //            {
        //                Console.WriteLine("Opção inválida!");
        //                Thread.Sleep(2000);
        //            }

        //            else if (op.Contains("0"))
        //                return;

        //            else if (op.Contains("2"))
        //                if (i == 0)
        //                    i = 0;
        //                else
        //                    i--;

        //            else if (op.Contains("3"))
        //                i = voos.Count - 1;

        //            else if (op.Contains("4"))
        //                i = 0;

        //        } while (op != "1");
        //        i = 0;
        //    }
        //}
    }
}
