using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class Passageiro
    {
        private string Cpf { get; set; }
        private string Nome { get; set; }
        private DateTime DataNascimento { get; set; }
        private string Sexo { get; set; }
        private DateTime UltimaCompra { get; set; }
        private DateTime DataCadastro { get; set; }
        private string Situacao { get; set; }

        Banco conn = new Banco();

        public Passageiro()
        {
        }

        public override string ToString()
        {
            return $"{Cpf}{Nome}{DataNascimento}{Sexo}{UltimaCompra}{DataCadastro}{Situacao}";
        }

        //Cadastra o CPF
        public bool CadastraCpf()
        {
            do
            {
                Console.Write("Digite seu CPF: ");
                Cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                if (Cpf == "0")
                    return false;
                if (!ValidaCPF(Cpf))
                {
                    Console.WriteLine("Digite um CPF Válido!");
                    Thread.Sleep(2000);
                }

                bool verificacao = conn.VerificaDados(Cpf, "Cpf", "Passageiro");

                if (verificacao)
                {
                    Console.WriteLine("Este CNPJ já está cadastrado!!");
                    Thread.Sleep(2000);
                    Cpf = "";
                }
            } while (!ValidaCPF(Cpf));
            return true;
        }

        //Cadastra o Nome
        public bool CadastraNome()
        {
            do
            {
                Console.Write("Digite seu Nome (Max 50 caracteres): ");
                Nome = Console.ReadLine();
                if (Nome == "0")
                    return false;
                if (Nome.Length > 50)
                {
                    Console.WriteLine("Infome um nome menor que 50 caracteres!!!!");
                    Thread.Sleep(2000);
                }
            } while (Nome.Length > 50);
            return true;
        }

        //Cadastra a data de nascimento
        public bool CadastraDataNasc()
        {
            Console.Write("Digite sua data de nascimento (Mes/Dia/Ano): ");
            DateTime dataNasc;
            while (!DateTime.TryParse(Console.ReadLine(), out dataNasc))
            {
                Console.Write("Digite sua data de nascimento (Mes/Dia/Ano): ");
            }

            DataNascimento = dataNasc;
            return true;
        }

        //Cadastra o sexo do passageiro
        public bool CadastraSexo()
        {
            do
            {
                Console.WriteLine("Digite seu sexo [M] Masculino / [F] Feminino / [N] Prefere não informar: ");
                Sexo = Console.ReadLine().ToUpper();
                if (Sexo == "0")
                    return false;
                if (Sexo != "M" && Sexo != "N" && Sexo != "F")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (Sexo != "M" && Sexo != "N" && Sexo != "F");
            return true;
        }

        //Altera a situação do cadastro do passageiro
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

        //Cadastra um novo passageiro
        public void CadastraPassageiro()
        {
            Console.WriteLine(">>> CADASTRO DE PASSAGEIRO <<<");

            if (!CadastraCpf())
                return;

            if (!CadastraNome())
                return;

            if (!CadastraDataNasc())
                return;

            if (!CadastraSexo())
                return;

            UltimaCompra = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            string query = $"INSERT INTO Passageiro (Cpf,Nome,DataNascimento,Sexo,UltimaCompra,DataCadastro,Situacao)" + $"VALUES('{Cpf}','{Nome}','{DataNascimento}','{Sexo}','{UltimaCompra}','{DataCadastro}','{Situacao}')";

            conn.Insert(query);

            Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();
        }

        //Altera o cadastro do passageiro
        public void AlteraDadoPassageiro()
        {
            bool verificacao;

            Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o CPF do passageiro: ");

            string cpf;
            do
            {
                cpf = Console.ReadLine().Replace(".", "").Replace("-", "");

                if (!ValidaCPF(cpf))
                {
                    Console.WriteLine("CPF inválido!");
                    Thread.Sleep(3000);
                    Console.Write("Digite um CPF do valido: ");
                }
            } while (!ValidaCPF(cpf));

            int opcao = 1;

            string query = $"SELECT Cpf,Nome,DataNascimento,Sexo,UltimaCompra,DataCadastro,Situacao FROM Passageiro WHERE Cpf = '{cpf}'";

            verificacao = conn.Select(query, opcao);

            if (verificacao)
            {
                string num;
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<");
                    Console.Write("Para alterar digite:\n\n[1] Nome\n[2] Sexo\n[3] Situação do Cadastro\n[0] Sair\nOpção: ");
                    num = Console.ReadLine();

                    if (num != "1" && num != "2" && num != "3" && num != "0")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(3000);
                    }
                } while (num != "1" && num != "2" && num != "3" && num != "0");

                if (num.Contains("0"))
                    return;

                //Condição para alterar o dado em específico do passageiro
                switch (num)
                {
                    case "1":
                        if (!CadastraNome())
                            return;
                        query = $"UPDATE Passageiro SET Nome = '{Nome}' where Cpf = '{cpf}'";
                        conn.Update(query);

                        query = $"SELECT Cpf,Nome,DataNascimento,Sexo,UltimaCompra,DataCadastro,Situacao FROM Passageiro WHERE Cpf = '{cpf}'";
                        conn.Select(query, opcao);
                        break;

                    case "2":
                        if (!CadastraSexo())
                            return;
                        query = $"UPDATE Passageiro SET Nome = '{Sexo}' where Cpf = '{cpf}'";
                        conn.Update(query);

                        query = $"SELECT Cpf,Nome,DataNascimento,Sexo,UltimaCompra,DataCadastro,Situacao FROM Passageiro WHERE Cpf = '{cpf}'";
                        conn.Select(query, opcao);
                        break;

                    case "3":
                        if (!AlteraSituacao())
                            return;
                        query = $"UPDATE Passageiro SET Nome = '{Situacao}' where Cpf = '{cpf}'";
                        conn.Update(query);

                        query = $"SELECT Cpf,Nome,DataNascimento,Sexo,UltimaCompra,DataCadastro,Situacao FROM Passageiro WHERE Cpf = '{cpf}'";
                        conn.Select(query, opcao);
                        break;
                }
                Console.WriteLine("Cadastro alterado com sucesso!");
                Console.ReadKey();
            }
        }

        private static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        //Imprime todos os passageiros
        public void ImprimiPassageiro()
        {
            int opcao = 1;
            Console.WriteLine(">>> Passageiros Cadastrados <<<");

            string query = "SELECT * FROM Passageiro";
            conn.Select(query, opcao);
        }
    }
}



