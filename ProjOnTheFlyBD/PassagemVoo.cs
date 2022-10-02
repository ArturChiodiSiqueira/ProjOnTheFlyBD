using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class PassagemVoo
    {
        public string Id { get; set; }
        public string IdVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public float Valor { get; set; }
        public string Situacao { get; set; }

        Banco conn = new Banco();

        public PassagemVoo()
        {
        }

        public override string ToString()
        {
            return $"{Id}{IdVoo}{DataCadastro}{Valor}{Situacao}";
        }

        public bool GerarIdPassagem()
        {
            int quantidade = 0;
            while (true)
            {
                if (quantidade == 999)
                {
                    Console.WriteLine("Quantidade máxima de passagens atingida!");
                    Thread.Sleep(2000);
                    return false;
                }

                Random r = new Random();

                Id = "PA" + r.Next(0001, 9999).ToString("0000");

                if (conn.VerificaDados(Id, "ID", "PassagemVoo"))
                {
                    quantidade++;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool VerificaIdPassagem()
        {
            do
            {
                Console.Write("Informe o Id da passagem: ");
                Id = Console.ReadLine().ToUpper();
                if (conn.VerificaDados(Id, "ID", "PassagemVoo"))
                {
                    Console.WriteLine("Passagem encontrada com sucesso!");
                    Thread.Sleep(2000);
                    return true;
                }
                else
                {
                    Console.WriteLine("Passagem não encontrada!");
                    Id = "";
                    Thread.Sleep(2000);
                }
            } while (Id.Length == 0);
            return false;
        }

        public bool BuscaIdVoo()
        {
            do
            {
                Console.Write("Informe o Id do voo: ");
                IdVoo = Console.ReadLine().ToUpper();
                if (conn.VerificaDados(IdVoo, "ID", "Voo"))
                {
                    Console.WriteLine("Voo encontrado com sucesso!");
                    Thread.Sleep(2000);
                    return true;
                }
                else
                {
                    Console.WriteLine("Voo não encontrado!");
                    IdVoo = "";
                    Thread.Sleep(2000);
                }
            } while (IdVoo.Length == 0);
            return false;
        }

        public void GerarPassagem(string idVoo, float valorPas)
        {
            if (!GerarIdPassagem())
                return;

            Valor = valorPas;

            DataCadastro = DateTime.Now;

            Situacao = "L";

            string query = $"INSERT INTO PassagemVoo(ID,IDVoo,DataUltimaOperacao,Valor,Situacao) VALUES('{Id}','{idVoo}','{DataCadastro}','{Valor}','{Situacao}')";
            conn.Insert(query);
        }

        public void imprimePassagem()
        {
            if (!BuscaIdVoo())
                return;

            Console.Clear();
            Console.WriteLine(">>> Passagens Cadastradas <<<");

            int opcao = 7;
            string query = $"SELECT ID,IDVoo,DataUltimaOperacao,Valor,Situacao FROM PassagemVoo WHERE IDVoo = '{IdVoo}' and Situacao  ='L'";
            conn.Select(query, opcao);
        }

        public void AlteraDadoPassagem()
        {
            bool verificacao;
            if (BuscaIdVoo())
            {
                Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEM <<<");
                
                int opcao = 7;

                string query = $"SELECT ID,IDVoo,DataUltimaOperacao,Valor,Situacao FROM PassagemVoo WHERE IDVoo = '{IdVoo}' and Situacao  ='L'";

                verificacao = conn.Select(query, opcao);

                if (verificacao)
                {
                    string num;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEM <<<");
                        Console.Write("Para alterar digite:\n\n[1] Valor\n[2] Situação\n[0] Sair\nOpção: ");
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
                            Console.Write("Informe o novo preço: ");
                            double valor = double.Parse(Console.ReadLine().Replace(",", "").Replace(".", ""));

                            query = $"UPDATE PassagemVoo SET Valor = '{valor}' WHERE IDVoo = '{IdVoo}'";
                            conn.Update(query);

                            query = $"SELECT ID,IDVoo,DataUltimaOperacao,Valor,Situacao FROM PassagemVoo WHERE IDVoo = '{IdVoo}' and Situacao  ='L'";
                            conn.Select(query, opcao);
                            break;

                        case "2":
                            if (!AlteraSituacao())
                                return;
                            query = $"UPDATE PassagemVoo SET Situacao = '{Situacao}' where IDVoo = '{IdVoo}'";
                            conn.Update(query);

                            query = $"SELECT ID,IDVoo,DataUltimaOperacao,Valor,Situacao FROM PassagemVoo WHERE IDVoo = '{IdVoo}' and Situacao  ='L'";
                            conn.Select(query, opcao);
                            break;
                    }
                    Console.WriteLine("Cadastro alterado com sucesso!");
                    Thread.Sleep(3000);
                }
            }
        }

        public bool AlteraSituacao()
        {
            string num;
            do
            {
                Console.WriteLine("Alterar Situação:\n[L] Livre \n[P] Paga\n[R] Reservada");
                num = Console.ReadLine().ToUpper();
                if ((num != "V") && (num != "R") && (num != "L"))
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while ((num != "V") && (num != "R") && (num != "L"));
            Situacao = num;
            return true;
        }
    }
}

