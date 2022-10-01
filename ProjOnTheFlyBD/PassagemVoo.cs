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
        public string DataCadastro { get; set; }
        public string Valor { get; set; }
        public char Situacao { get; set; }

        string Caminho = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\PassagemVoo.dat";
        string CaminhoAeronave = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Aeronave.dat";

        public PassagemVoo()
        {



        }

        public void GerarPassagem(string idAeronave, string idVoo)
        {
            int capacidade = 0;
            float valorPas;

            string caminhoAeronave = CaminhoAeronave;
            foreach (string line in File.ReadLines(caminhoAeronave))
            {
                if (line.Contains(idAeronave))
                {
                    capacidade = int.Parse(line.Substring(5, 3));
                    Thread.Sleep(2000);
                }
            }
            Thread.Sleep(2000);

            // Console.WriteLine(" Digite o codigo do Voo: ");
            // idVoo = Console.ReadLine();

            // Console.WriteLine(" Digite a capacidade do avião: ");
            //  capacidade = int.Parse(Console.ReadLine());

            Console.WriteLine(" Digite o valor da Passagem: ");
            valorPas = float.Parse(Console.ReadLine().Replace(",", ""));



            int i = 0;
            for (i = 1; i <= capacidade; i++)
            {

                //int id = Random(0001, 9999);
                if (i < 10)
                {
                    Id = $"PA000{i}";
                }
                else if (i < 100)
                {
                    Id = $"PA00{i}";

                }
                else if (i < 1000)
                {
                    Id = $"PA0{i}";
                }
                else
                {
                    Id = $"PA{i}";
                }



                IdVoo = $"V{idVoo}";

                Valor = valorPas.ToString("000000");


                DataCadastro = DateTime.Now.ToShortDateString().Replace("/", "");

                Situacao = 'L';

                Console.WriteLine(ToString());
                string caminho = Caminho;
                string texto = $"{ToString()}\n";
                File.AppendAllText(caminho, texto);


                // Console.ReadKey();
            }
        }
        public void GerarMenu()
        {
            Console.WriteLine(" Digite a opção: \n" +
               " 1 - Ver passagens\n" +
               " 2 - Alterar preço da passagem\n" +
               " 3 - Gerar uma passagem");

            int opc = int.Parse(Console.ReadLine());
            switch (opc)
            {
                case 1:
                    NevagarPassagem();
                    break;
                case 2:
                    AlterarPrecoPassagem();
                    break;

                default: break;
            }
        }
        public override string ToString()
        {
            return $"{Id}{IdVoo}{DataCadastro}{Valor}{Situacao}";
        }

        public int Random(int min, int Max)
        {
            // bool repetido =  false;
            Random r = new Random();
            /* do
             {
                 foreach (string linha in File.ReadLines(Caminho))
                 {
                     if (linha.Contains("PassagemVoo") & linha.Contains(r))
                     {
                         repetido = true;
                         break;
                     }
                 }
             } while (repetido);*/
            return r.Next(0001, 9999);

        }
        public void AlterarSituação()
        {
            char situacao;
            do
            {
                Console.WriteLine(" Alterar para Reservada ou Vendida uma passagem:\n L -  Livre \n P - Paga\n R - Reservada");
                situacao = char.Parse(Console.ReadLine().ToUpper());
                if ((situacao != 'V') && (situacao != 'R') && (situacao != 'L'))
                {
                    Console.WriteLine(" Opção invalida!!!");
                }
            } while ((situacao != 'V') && (situacao != 'R') && (situacao != 'L'));
        }

        public void NevagarPassagem()
        {
            string[] lines = File.ReadAllLines(Caminho);
            List<string> passagem = new();

            Console.WriteLine(" Digite o codigo do voo (V000): ");
            string codVoo = Console.ReadLine();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                // Console.WriteLine(lines[i]);
                //Console.ReadKey();
                //Verifica passagens do voo
                if (lines[i].Substring(7, 4).Contains(codVoo))
                    passagem.Add(lines[i]);
            }

            //Laço para navegar nos cadastros das Companhias
            for (int i = 0; i < passagem.Count; i++)
            {
                string op;
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>> Lista de Passagem <<<\nDigite para navegar:\n[1] Próximo Cadasatro\n[2] Cadastro Anterior" +
                        "\n[3] Último cadastro\n[4] Voltar ao Início\n[0] Sair\n");

                    Console.WriteLine($"Cadastro [{i + 1}] de [{passagem.Count}]");
                    //Imprimi o primeiro da lista 
                    LocalPassagem(Caminho, passagem[i].Substring(0, 5));

                    Console.Write("Opção: ");
                    op = Console.ReadLine();

                    if (op != "0" && op != "1" && op != "2" && op != "3" && op != "4")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(2000);
                    }
                    //Sai do método
                    else if (op.Contains("0"))
                        return;

                    //Volta no Cadastro Anterior
                    else if (op.Contains("2"))
                        if (i == 0)
                            i = 0;
                        else
                            i--;

                    //Vai para o fim da lista
                    else if (op.Contains("3"))
                        i = passagem.Count - 1;

                    //Volta para o inicio da lista
                    else if (op.Contains("4"))
                        i = 0;
                    //Vai para o próximo da lista    
                } while (op != "1");

            }
        }
        public void LocalPassagem(string caminho, string idPassagem)
        {
            foreach (string linha in File.ReadLines(caminho))
            {
                if (linha.Contains(idPassagem))
                {
                    Console.WriteLine($"Codigo Passagem: {linha.Substring(0, 6)}");
                    Console.WriteLine($"Codigo do Voo: {linha.Substring(6, 5)}");
                    Console.WriteLine($"Data Emissão da Passagem: {linha.Substring(11, 8)}");
                    Console.WriteLine($"Preço: R${linha.Substring(19, 4)},{linha.Substring(23, 2)}");
                    //Console.WriteLine($"Situação: {linha.Substring(26,1)}");
                    if (linha.Substring(25, 1).Contains('L'))
                    {
                        Console.WriteLine($" Situaçã: Livre");
                    }
                    else if (linha.Substring(25, 1).Contains('P'))
                    {
                        Console.WriteLine($" Situação: Paga");
                    }
                    else if (linha.Substring(25, 1).Contains('R'))
                    {
                        Console.WriteLine($" Situação: Reservada");
                    }
                }
                break;
            }
        }



        public void AlterarPrecoPassagem()
        {

            string[] lines = File.ReadAllLines(Caminho);

            Console.WriteLine($" Digite o novo valor da passagem: ");
            float novoValor = float.Parse(Console.ReadLine().Replace(",", ""));

            Console.WriteLine(" Digite o codigo do voo (V000): ");
            bool retorna = true;
            do
            {
                string codVoo = Console.ReadLine();


                for (int i = 0; i < lines.Length; i++)
                {

                    if (lines[i].Substring(7, 4).Contains(codVoo))
                    {
                        if (lines[i].Substring(25, 1).Contains('L'))
                        {
                            lines[i] = lines[i].Replace(lines[i].Substring(19, 6), novoValor.ToString("000000"));
                            retorna = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Codigo de Voo não encontrado!");
                    }
                }
            } while (retorna);
            File.WriteAllLines(Caminho, lines);

        }

    }
}

