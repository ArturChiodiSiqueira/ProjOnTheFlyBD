using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoOnTheFlyBD
{
    internal class CompanhiaAerea
    {
        private string Cnpj { get; set; }
        private string RazaoSocial { get; set; }
        private DateTime DataAbertura { get; set; }
        private DateTime UltimoVoo { get; set; }
        private DateTime DataCadastro { get; set; }
        private string Situacao { get; set; }

        Banco conn = new Banco();

        public CompanhiaAerea()
        {
        }

        public override string ToString()
        {
            return $"{Cnpj}{RazaoSocial}{DataAbertura}{UltimoVoo}{DataCadastro}{Situacao}";
        }

        //Cadastra CNPJ
        public bool CadCNPJ()
        {
            do
            {
                Console.Write("Digite o CNPJ: ");
                Cnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
                if (Cnpj == "0")
                    return false; ;
                if (!ValidaCNPJ(Cnpj))
                {
                    Console.WriteLine("Digite um CNPJ Válido!");
                    Thread.Sleep(2000);
                }
                else
                {
                    if (conn.VerificaDados(Cnpj, "Cnpj", "CompanhiaAerea"))
                    {
                        Console.WriteLine("Este CNPJ já está cadastrado!!");
                        Thread.Sleep(2000);
                        Cnpj = "";
                    }
                }
            } while (!ValidaCNPJ(Cnpj));
            return true;
        }

        //Cadastra Data de Abertura
        public bool CadDataAbertura()
        {
            Console.Write("Digite a data de abertura (Dia/Mes/Ano): ");
            DateTime dataAbertura;
            while (!DateTime.TryParse(Console.ReadLine(), out dataAbertura))
            {
                Console.Write("Digite a data de abertura (Dia/Mes/Ano): ");
            }

            DateTime verData = dataAbertura;
            if (verData > DateTime.Now.AddMonths(-6))
            {
                Console.WriteLine("Não é possível cadastrar empresas com menos de 6 meses!!!");
                Thread.Sleep(2000);
                return false;
            }

            DataAbertura = dataAbertura;
            return true;
        }

        //Cadastra a Razão Social
        public bool CadRazao()
        {
            Console.Write("Digite a Razão Social:  (Max 50 caracteres): ");
            RazaoSocial = Console.ReadLine();
            if (RazaoSocial == "0")
                return false;
            if (RazaoSocial.Length > 50)
            {
                Console.WriteLine("Infome uma Razão Social menor que 50 caracteres!!!!");
                Thread.Sleep(2000);
            }
            while (RazaoSocial.Length > 50) ;

            for (int i = RazaoSocial.Length; i <= 50; i++)
                RazaoSocial += " ";
            return true;
        }

        //Altera a situação da Companhia 
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

        //Cadastra uma nova Companhia Aerea
        public void CadCompanhia()
        {
            Console.WriteLine(">>> CADSTRO DE COMPANHIA AEREA <<<");
            Console.WriteLine("Para cancelar o cadastro digite 0:\n");

            if (!CadCNPJ())
                return;

            if (!CadDataAbertura())
                return;

            if (!CadRazao())
                return;

            UltimoVoo = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            string query = $"INSERT INTO CompanhiaAerea (Cnpj,RazaoSocial,DataAbertura,UltimoVoo,DataCadastro,Situacao)" + $"VALUES('{Cnpj}','{RazaoSocial}','{DataAbertura}','{UltimoVoo}','{DataCadastro}','{Situacao}')";

            conn.Insert(query);

            Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();
        }

        public void AlteraCompanhia()
        {
            bool verificacao;

            Console.WriteLine(">>> ALTERAR DADOS DA COMPANHIA <<<\nPara sair digite 's'.\n");
            Console.Write("Digite o CNPJ da Companhia: ");

            string cnpj;
            do
            {
                cnpj = Console.ReadLine().Replace(".", "").Replace("-", "");
                if (!ValidaCNPJ(cnpj))
                {
                    Console.WriteLine("CNPJ inválido!");
                    Thread.Sleep(3000);
                }
            } while (!ValidaCNPJ(cnpj));

            int opcao = 3;

            string query = $"SELECT Cnpj,RazaoSocial,DataAbertura,UltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea WHERE Cnpj = '{cnpj}'";

            verificacao = conn.Select(query, opcao);

            if (verificacao)
            {
                string num;
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>> ALTERAR DADOS DA COMPANHIA <<<");
                    Console.Write("Para alterar digite:\n\n[1] Razão Social\n[2] Situação do Cadastro\n[0] Sair\nOpção: ");
                    num = Console.ReadLine();

                    if (num != "1" && num != "2" && num != "0")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(3000);
                    }
                } while (num != "1" && num != "2" && num != "3" && num != "0");

                if (num.Contains("0"))
                    return;

                //Condição para alterar o dado em específico da Companhia
                switch (num)
                {
                    case "1":
                        if (!CadRazao())
                            return;
                        query = $"UPDATE CompanhiaAerea SET RazaoSocial = '{RazaoSocial}' where Cnpj = '{cnpj}'";
                        conn.Update(query);

                        query = $"SELECT Cnpj,RazaoSocial,DataAbertura,UltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea WHERE Cnpj = '{cnpj}'";
                        conn.Select(query, opcao);
                        break;

                    case "2":
                        if (!AlteraSituacao())
                            return;
                        query = $"UPDATE CompanhiaAerea SET Situacao = '{Situacao}' where Cnpj = '{cnpj}'";
                        conn.Update(query);

                        query = $"SELECT Cnpj,RazaoSocial,DataAbertura,UltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea WHERE Cnpj = '{cnpj}'";
                        conn.Select(query, opcao);
                        break;
                }
                Console.WriteLine("Cadastro alterado com sucesso!");
                Thread.Sleep(3000);
            }
        }

        //Imprime todas as companhias
        public void ImprCompanhia()
        {
            int opcao = 3;
            Console.WriteLine(">>> Companhias Cadastradas <<<");

            string query = "SELECT * FROM CompanhiaAerea";
            conn.Select(query, opcao);
        }

        ////Verifica O CNPJ se é válido
        public bool ValidaCNPJ(string vrCNPJ)
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

        ////Imprimi as Companhias Cadastradas e Ativas
        //public void ImprCompanhia()
        //{
        //    string[] lines = File.ReadAllLines(caminho);
        //    List<string> companhia = new();

        //    for (int i = 1; i < lines.Length; i++)
        //    {
        //        //Verifica se o cadastro esta ativo
        //        if (lines[i].Substring(89, 1).Contains("A"))
        //            companhia.Add(lines[i]);
        //    }

        //    //Laço para navegar nos cadastros das Companhias
        //    for (int i = 0; i < companhia.Count; i++)
        //    {
        //        string op;
        //        do
        //        {
        //            Console.Clear();
        //            Console.WriteLine(">>> Cadastro Passageiros <<<\nDigite para navegar:\n[1] Próximo Cadasatro\n[2] Cadastro Anterior" +
        //                "\n[3] Último cadastro\n[4] Voltar ao Início\n[s] Sair\n");

        //            Console.WriteLine($"Cadastro [{i + 1}] de [{companhia.Count}]");
        //            //Imprimi o primeiro da lista 
        //            LocalCompanhia(caminho, companhia[i].Substring(0, 14));

        //            Console.Write("Opção: ");
        //            op = Console.ReadLine().ToLower();

        //            if (op != "s" && op != "1" && op != "2" && op != "3" && op != "4")
        //            {
        //                Console.WriteLine("Opção inválida!");
        //                Thread.Sleep(2000);
        //            }
        //            //Sai do método
        //            else if (op.Contains("s"))
        //                return;

        //            //Volta no Cadastro Anterior
        //            else if (op.Contains("2"))
        //                if (i == 0)
        //                    i = 0;
        //                else
        //                    i--;

        //            //Vai para o fim da lista
        //            else if (op.Contains("3"))
        //                i = companhia.Count - 1;

        //            //Volta para o inicio da lista
        //            else if (op.Contains("4"))
        //                i = 0;
        //            //Vai para o próximo da lista    
        //        } while (op != "1");
        //        if (i == companhia.Count - 1)
        //            i--;
        //    }
        //}
    }
}
