using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoOnTheFlyBD
{
    internal class PassagemVoo
    {
        public string Id { get; set; }
        public string IdVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public float Valor { get; set; }
        public char Situacao { get; set; }

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
                if (conn.VerificaDados(IdVoo, "IDVoo", "Voo"))
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

            Situacao = 'L';

            string query = $"INSERT INTO PassagemVoo(ID,IDVoo,DataUltimaOperação,Valor,Situacao) VALUES('{Id}','{idVoo}','{DataCadastro}','{Valor}','{Situacao}')";
            conn.Insert(query);
        }

        public void imprimePassagem()
        {
            if (!BuscaIdVoo())
                return;

            Console.WriteLine(">>> Passagens Disponiveis <<<");

            int opcao = 7;
            string query = $"SELECT * FROM PassagemVoo WHERE IDVoo = '{IdVoo}' and Situacao  ='L'";
            conn.Select(query, opcao);
        }

        //public void AlterarSituação()
        //{
        //    char situacao;
        //    do
        //    {
        //        Console.WriteLine(" Alterar para Reservada ou Vendida uma passagem:\n L -  Livre \n P - Paga\n R - Reservada");
        //        situacao = char.Parse(Console.ReadLine().ToUpper());
        //        if ((situacao != 'V') && (situacao != 'R') && (situacao != 'L'))
        //        {
        //            Console.WriteLine(" Opção invalida!!!");
        //        }
        //    } while ((situacao != 'V') && (situacao != 'R') && (situacao != 'L'));
        //}

        //public void NevagarPassagem()
        //{
        //    string[] lines = File.ReadAllLines(Caminho);
        //    List<string> passagem = new();

        //    Console.WriteLine(" Digite o codigo do voo (V000): ");
        //    string codVoo = Console.ReadLine();

        //    for (int i = 0; i < lines.Length - 1; i++)
        //    {
        //        // Console.WriteLine(lines[i]);
        //        //Console.ReadKey();
        //        //Verifica passagens do voo
        //        if (lines[i].Substring(7, 4).Contains(codVoo))
        //            passagem.Add(lines[i]);
        //    }

        //    //Laço para navegar nos cadastros das Companhias
        //    for (int i = 0; i < passagem.Count; i++)
        //    {
        //        string op;
        //        do
        //        {
        //            Console.Clear();
        //            Console.WriteLine(">>> Lista de Passagem <<<\nDigite para navegar:\n[1] Próximo Cadasatro\n[2] Cadastro Anterior" +
        //                "\n[3] Último cadastro\n[4] Voltar ao Início\n[0] Sair\n");

        //            Console.WriteLine($"Cadastro [{i + 1}] de [{passagem.Count}]");
        //            //Imprimi o primeiro da lista 
        //            LocalPassagem(Caminho, passagem[i].Substring(0, 5));

        //            Console.Write("Opção: ");
        //            op = Console.ReadLine();

        //            if (op != "0" && op != "1" && op != "2" && op != "3" && op != "4")
        //            {
        //                Console.WriteLine("Opção inválida!");
        //                Thread.Sleep(2000);
        //            }
        //            //Sai do método
        //            else if (op.Contains("0"))
        //                return;

        //            //Volta no Cadastro Anterior
        //            else if (op.Contains("2"))
        //                if (i == 0)
        //                    i = 0;
        //                else
        //                    i--;

        //            //Vai para o fim da lista
        //            else if (op.Contains("3"))
        //                i = passagem.Count - 1;

        //            //Volta para o inicio da lista
        //            else if (op.Contains("4"))
        //                i = 0;
        //            //Vai para o próximo da lista    
        //        } while (op != "1");

        //    }
        //}
        //public void LocalPassagem(string caminho, string idPassagem)
        //{
        //    foreach (string linha in File.ReadLines(caminho))
        //    {
        //        if (linha.Contains(idPassagem))
        //        {
        //            Console.WriteLine($"Codigo Passagem: {linha.Substring(0, 6)}");
        //            Console.WriteLine($"Codigo do Voo: {linha.Substring(6, 5)}");
        //            Console.WriteLine($"Data Emissão da Passagem: {linha.Substring(11, 8)}");
        //            Console.WriteLine($"Preço: R${linha.Substring(19, 4)},{linha.Substring(23, 2)}");
        //            //Console.WriteLine($"Situação: {linha.Substring(26,1)}");
        //            if (linha.Substring(25, 1).Contains('L'))
        //            {
        //                Console.WriteLine($" Situaçã: Livre");
        //            }
        //            else if (linha.Substring(25, 1).Contains('P'))
        //            {
        //                Console.WriteLine($" Situação: Paga");
        //            }
        //            else if (linha.Substring(25, 1).Contains('R'))
        //            {
        //                Console.WriteLine($" Situação: Reservada");
        //            }
        //        }
        //        break;
        //    }
        //}



        //public void AlterarPrecoPassagem()
        //{

        //    string[] lines = File.ReadAllLines(Caminho);

        //    Console.WriteLine($" Digite o novo valor da passagem: ");
        //    float novoValor = float.Parse(Console.ReadLine().Replace(",", ""));

        //    Console.WriteLine(" Digite o codigo do voo (V000): ");
        //    bool retorna = true;
        //    do
        //    {
        //        string codVoo = Console.ReadLine();


        //        for (int i = 0; i < lines.Length; i++)
        //        {

        //            if (lines[i].Substring(7, 4).Contains(codVoo))
        //            {
        //                if (lines[i].Substring(25, 1).Contains('L'))
        //                {
        //                    lines[i] = lines[i].Replace(lines[i].Substring(19, 6), novoValor.ToString("000000"));
        //                    retorna = false;
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine(" Codigo de Voo não encontrado!");
        //            }
        //        }
        //    } while (retorna);
        //    File.WriteAllLines(Caminho, lines);

        //}

    }
}

