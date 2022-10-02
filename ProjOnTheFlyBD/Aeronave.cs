﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoOnTheFlyBD
{
    internal class Aeronave
    {
        public string Inscricao { get; set; }
        public string Capacidade { get; set; }
        public string AssentosOcupados { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }
        public string Cnpj { get; set; }

        Banco conn = new Banco();

        public Aeronave()
        {
        }

        public override string ToString()
        {
            return $"{Inscricao}{Cnpj}{Capacidade}{AssentosOcupados}{UltimaVenda}{DataCadastro}{Situacao}";
        }

        public bool CadastroCnpj()
        {
            do
            {
                Console.Write("Informe o CNPJ da qual a aeronave pertence: ");
                Cnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
                if (conn.VerificaDados(Cnpj, "Cnpj", "CompanhiaAerea"))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("CNPJ NÃO ENCONTRADO!");
                    Cnpj = "";
                }
            } while (Cnpj.Length == 0);
            return false;
        }

        public void CadastraAeronave()
        {
            Console.WriteLine(">>> CADASTRO DE AERONAVE <<<");

            CadastroCnpj();

            if (!CadastraIdAeronave())
                return;

            CadastraQtdPassageiros();

            AssentosOcupados = "000";

            UltimaVenda = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            string query = $"INSERT INTO Aeronave (Inscricao,Cnpj,Capacidade,AssentosOcupados,UltimaVenda,DataCadastro,Situacao)" + $"VALUES('{Inscricao}','{Cnpj}','{Capacidade}','{AssentosOcupados}','{UltimaVenda}','{DataCadastro}','{Situacao}')";

            conn.Insert(query);

            Console.WriteLine("\n CADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();

            Console.ReadKey();
        }

        public bool CadastraIdAeronave()
        {
            do
            {
                Console.Write("Informe o código de identificação da aeronave seguindo o padrão definido pela ANAC (XX-XXX): ");
                Inscricao = Console.ReadLine().ToUpper().Trim().Replace("-", "");
            } while (Inscricao.Length != 5);
            
            return true;
        }

        public bool CadastraQtdPassageiros()
        {
            int capacidade;
            do
            {
                Console.Write("Informe a capacidade de pessoas que a aeronave comporta: ");

                while (!int.TryParse(Console.ReadLine(), out capacidade))
                {
                    Console.WriteLine("Formato incorreto!");
                    break;
                }

                if (capacidade == 0)
                    return false;
                if (capacidade < 0 || capacidade > 999)
                {
                    Console.WriteLine("Capacidade inválida!");
                    Thread.Sleep(2000);
                }
            } while (capacidade < 0 || capacidade > 999);

            Capacidade = capacidade.ToString();
            return true;
        }

        public bool AlteraSituacao()
        {
            string num;
            do
            {
                Console.Write("Alterar Situação [A] Ativo / [I] Inativo / [0] Cancelar: ");
                num = Console.ReadLine().ToUpper();
                if (num != "A" && num != "I" && num != "0")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (num != "A" && num != "I" && num != "0");

            if (num.Contains("0"))
                return false;
            Situacao = num;
            return true;
        }

        public void AlteraDadoAeronave()
        {
            bool verificacao;
            string inscricao;

            Console.WriteLine(">>> ALTERAR DADOS DE AERONAVE <<<\nPara sair digite 's'.\n");

            do
            {
                Console.Write("Digite a inscrição da aeronave: ");
                inscricao = Console.ReadLine().ToUpper().Trim().Replace("-", "");
            } while (inscricao.Length != 5);

            int opcao = 4;

            string query = $"SELECT Inscricao,Cnpj,Capacidade,AssentosOcupados,UltimaVenda,DataCadastro,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";

            verificacao = conn.Select(query, opcao);

            if (verificacao)
            {
                string num;
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>> ALTERAR DADOS DE AERONAVE <<<");
                    Console.Write("Para alterar digite:\n\n[1] Capacidade\n[2] Situação do Cadastro\n[0] Sair\nOpção: ");
                    num = Console.ReadLine();

                    if (num != "1" && num != "2" && num != "0")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(3000);
                    }

                } while (num != "1" && num != "2" && num != "0");

                if (num.Contains("0"))
                    return;

                switch (num)
                {
                    case "1":
                        if (!CadastraQtdPassageiros())
                            return;
                        query = $"UPDATE Aeronave SET Capacidade = '{Capacidade}' where Inscricao = '{inscricao}'";
                        conn.Update(query);

                        query = $"SELECT Inscricao,Cnpj,Capacidade,AssentosOcupados,UltimaVenda,DataCadastro,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";
                        conn.Select(query, opcao);
                        break;

                    case "2":
                        if (!AlteraSituacao())
                            return;
                        query = $"UPDATE Aeronave SET Situacao = '{Situacao}' where Inscricao = '{inscricao}'";
                        conn.Update(query);

                        query = $"SELECT Inscricao,Cnpj,Capacidade,AssentosOcupados,UltimaVenda,DataCadastro,Situacao FROM Aeronave WHERE Inscricao = '{inscricao}'";
                        conn.Select(query, opcao);
                        break;
                }
                Console.WriteLine("Cadastro alterado com sucesso!");
                Thread.Sleep(3000);
            }
        }

        public void ImprimeAeronave()
        {
            int opcao = 4;
            Console.WriteLine(">>> Aeronaves Cadastradas <<<");

            string query = "SELECT * FROM Aeronave";
            conn.Select(query, opcao);
        }

        //public void ImprimeAeronaves()
        //{
        //    string[] lines = File.ReadAllLines(Caminho);
        //    List<string> aeronaves = new();
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        //Verifica se o cadastro esta ativo
        //        if (lines[i].Substring(27, 1).Contains("A"))
        //            aeronaves.Add(lines[i]);
        //    }
        //    //Laço para navegar nos cadastros de aeronaves
        //    for (int i = 0; i < aeronaves.Count; i++)
        //    {
        //        string op;
        //        do
        //        {
        //            Console.Clear();
        //            Console.WriteLine(">>> Cadastro Aeronaves <<<\nDigite para navegar:\n[1] Próximo Cadasatro\n[2] Cadastro Anterior" +
        //                "\n[3] Último cadastro\n[4] Voltar ao Início\n[0] Sair\n");
        //            Console.WriteLine($"Cadastro [{i + 1}] de [{aeronaves.Count}]");
        //            //Imprimi o primeiro da lista
        //            ImprimeAeronave(Caminho, aeronaves[i].Substring(0, 5));
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
        //                i = aeronaves.Count - 1;
        //            //Volta para o inicio da lista
        //            else if (op.Contains("4"))
        //                i = 0;
        //            //Vai para o próximo da lista
        //        } while (op != "1");
        //    }
        //}
    }
}