using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class Restrito
    {
        public string Cpf { get; set; }
        Banco conn = new Banco();

        public void GerarMenu()
        {
            int opc = 6;
            bool retorna = true;

            Console.WriteLine(" Digite a opção: \n" +
                "1 - Adicionar CPF\n" +
                "2 - Verificar CPF\n" +
                "3 - Remover CPF\n" +
                "4 - Lista de Restritos\n" +
                "5 - Voltar ao Menu Restritos\n" +
                "0 - Sair");

            do
            {
                try
                {
                    opc = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opc)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        AddRestricaoCpf();
                        Console.Clear();
                        GerarMenu();
                        break;
                    case 2:
                        VerificarCpf();
                        Console.Clear();
                        GerarMenu();
                        break;
                    case 3:
                        RetirarRestricaoCpf();
                        Console.Clear();
                        GerarMenu();
                        break;
                    case 4:
                        ListarCpf();
                        Console.Clear();
                        GerarMenu();
                        break;
                    case 5:
                        retorna = false;
                        GerarMenu();
                        break;
                    default: break;
                }
            } while (retorna);
        }

        public void VerificarCpf()
        {
            int opc = 2;
            bool verificacao;
            {
                bool valida;
                do
                {
                    Console.Write("Digite o CPF para verificar: ");
                    string cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                    valida = ValidarCPF(cpf);
                    if (valida)
                    {
                        string query = $"SELECT Cpf FROM CadastroRestritos WHERE Cpf ={cpf}";
                        verificacao = conn.Select(query, opc);
                        if (verificacao)
                        {
                            Console.WriteLine($"CPF Restrito!");
                        }
                        else
                        {
                            Console.WriteLine($"\n '{cpf}' não restrito!");
                        }
                        Console.ReadKey();
                    }
                    else if (!valida)
                    {
                        Console.WriteLine(" CPF invalido");
                        Thread.Sleep(2000);
                    }
                } while (valida == false);
            }
        }

        public void AddRestricaoCpf()
        {
            bool valida;
            do
            {
                Console.Write("Digite o CPF que será restringido: ");
                string cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                valida = ValidarCPF(cpf);
                if (valida)
                {

                    string query = $"INSERT INTO CadastroRestritos (Cpf) VALUES('{cpf}')";
                    conn.Insert(query);
                    Console.WriteLine("CPF Restringido com sucesso!");
                    Console.ReadKey();
                }
                else if (valida == false)
                {
                    Console.WriteLine("CPF invalido");
                    Thread.Sleep(2000);
                }
            } while (valida == false);
        }

        public void ListarCpf()
        {
            int opc = 2;
            Console.Clear();
            Console.WriteLine("Cpf Bloqueados:");
            string query = $"SELECT Cpf FROM CadastroRestritos ";
            conn.Select(query, opc);
            Console.ReadKey();
        }

        public void RetirarRestricaoCpf()
        {
            bool valida;
            do
            {
                Console.Write("Digite o CPF que será liberado: ");
                string cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                valida = ValidarCPF(cpf);
                if (valida)
                {
                    string query = $"DELETE FROM CadastroRestritos WHERE Cpf=('{cpf}')";
                    conn.Insert(query);
                    Console.WriteLine($"\n'{cpf}' Removido da lista de restritos.");
                    Console.ReadKey();
                }
                else if (valida == false)
                {
                    Console.WriteLine("CPF invalido");
                    Thread.Sleep(2000);
                }
            } while (valida == false);
        }

        public override string ToString()
        {
            return $"{Cpf}";
        }

        private static bool ValidarCPF(string vrCPF)
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
    }
}
