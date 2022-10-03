using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class Bloqueado
    {
        public string Cnpj { get; set; }

        Banco conn = new Banco();

        public void GerarMenu()
        {
            int opc = 6;
            bool retorna = true;

            //BloquearCnpj();
            Console.WriteLine("Digite a opção: \n" +
                "1 - Adicionar CNPJ\n" +
                "2 - Verificar CNPJ\n" +
                "3 - Remover CNPJ\n" +
                "4 - Lista de Bloqueados\n" +
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
                        BloquearCnpj();
                        break;
                    case 2:
                        VerificarCnpj();
                        break;
                    case 3:
                        DesbloquearCnpj();
                        break;
                    case 4:
                        ListarCnpj();
                        break;
                    case 5:
                        retorna = false;
                        break;
                    default: break;
                }
            } while (retorna);
        }

        public void VerificarCnpj()
        {
            int opc = 3;
            bool verificacao;
            bool valida;
            do
            {
                Console.Write("Digite o CNPJ para verificação: ");
                string cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");
                valida = ValidarCNPJ(cnpj);
                if (valida)
                {
                    string query = $"SELECT Cnpj FROM Bloqueado WHERE Cnpj ={cnpj}";
                    verificacao = conn.Select(query, opc);
                    if (verificacao)
                    {
                        Console.WriteLine("CNPJ Bloqurado!");
                    }
                    else
                    {
                        Console.WriteLine($"\n '{cnpj}' não bloqueado!");
                    }
                }
                else if (!valida)
                {
                    Console.WriteLine("CNPJ invalido!");
                }
            } while (valida == false);
        }

        public void BloquearCnpj()
        {
            bool valida;
            do
            {
                Console.Write("Digite o CNPJ que será bloqueado: ");
                string cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");
                valida = ValidarCNPJ(cnpj);

                if (valida)
                {
                    string query = $"INSERT INTO Bloqueado (Cnpj) VALUES('{cnpj}')";
                    conn.Insert(query);
                    Console.WriteLine("\nCNPJ Bloqueado com sucesso!");
                    Console.ReadKey();
                }
                else if (!valida)
                {
                    Console.WriteLine("CNPJ invalido");
                }
            } while (valida == false);
        }

        public void ListarCnpj()
        {
            int opc = 5;
            Console.Clear();
            Console.WriteLine("Cnpj Bloqueados:");
            string query = $"SELECT Cnpj FROM Bloqueado ";
            conn.Select(query, opc);
        }

        public void DesbloquearCnpj()
        {
            bool valida;
            do
            {
                Console.Write("Digite o CNPJ que será desbloqueado: ");
                string cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");

                valida = ValidarCNPJ(cnpj);

                if (valida)
                {
                    string query = $"DELETE FROM Bloqueado WHERE Cnpj=('{cnpj}')";
                    conn.Delete(query);
                    Console.WriteLine($"\n'{cnpj}' Removido da lista de bloqueados.");
                    Console.ReadKey();
                }
                else if (valida == false)
                {
                    Console.WriteLine("CNPJ invalido");
                }
            } while (valida == false);
        }

        public override string ToString()
        {
            return $"{Cnpj}";
        }
        public static bool ValidarCNPJ(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;

            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
    }
}
