using ProjOnTheFlyBD;
using System;

namespace ProjOnTheFlyBD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostrarMenuInicial();
        }

        static void MostrarMenuInicial()
        {
            int opcao = 5;
            Console.Clear();
            Console.WriteLine(" °°°  MENU  INICIAL  °°°");
            Console.WriteLine(" Opção 1 : Menu cadastro");
            Console.WriteLine(" Opção 2 : Menu editar");
            Console.WriteLine(" Opção 3 : Menu imprimir");
            Console.WriteLine(" Opção 4 : Menu bloqueados e restritos");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");

            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.Clear();
                        MostrarMenuCadastrar();
                        break;

                    case 2:
                        Console.Clear();
                        MostrarMenuEditar();
                        break;

                    case 3:
                        Console.Clear();
                        MostrarMenuImprimir();
                        break;

                    case 4:
                        Console.Clear();
                        MenuBloqueadosRestritos();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }
            } while (true);
        }

        static void MostrarMenuCadastrar()
        {
            int opcao = 7;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            Venda venda = new();

            Console.WriteLine(" °°°  MENU  CADASTRO  °°°");
            Console.WriteLine(" Opção 1 : Cadastrar passageiro");
            Console.WriteLine(" Opção 2 : Cadastrar companhia aerea");
            Console.WriteLine(" Opção 3 : Cadastrar aeronave");
            Console.WriteLine(" Opção 4 : Cadastro de voo");
            Console.WriteLine(" Opção 5 : Cadastrar venda de passagem");
            Console.WriteLine(" Opção 6 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");

            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }
                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine(" Cadastrar passageiro");
                        Console.Clear();
                        passageiro.CadastraPassageiro();
                        break;
                    case 2:
                        Console.WriteLine(" Cadastrar companhia aerea");
                        Console.Clear();
                        companhiaAerea.CadCompanhia();
                        break;
                    case 3:
                        Console.WriteLine(" Cadastrar aeronave");
                        Console.Clear();
                        aeronave.CadastraAeronave();
                        break;

                    case 4:
                        Console.WriteLine(" Cadastro de voo");
                        Console.Clear();
                        voo.CadastrarVoo();
                        break;

                    case 5:
                        Console.WriteLine(" Cadastrar venda de passagem");
                        Console.Clear();
                        venda.Cadastrar();
                        break;
                    case 6:
                        Console.WriteLine(" Menu Inicial");
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opção invalida\n Digite novamente:");
                        break;
                }
            } while (true);
        }

        static void MostrarMenuEditar()
        {
            int opcao = 7;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            PassagemVoo passagemVoo = new();

            Console.WriteLine(" °°°  MENU  EDITAR  °°°");
            Console.WriteLine(" Opção 1 : Editar passageiro");
            Console.WriteLine(" Opção 2 : Editar companhia aerea");
            Console.WriteLine(" Opção 3 : Editar aeronave");
            Console.WriteLine(" Opção 4 : Editar voo");
            Console.WriteLine(" Opção 5 : Editar passagem");
            Console.WriteLine(" Opção 6 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");
            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Editar passageiro");
                        Console.Clear();
                        passageiro.AlteraDadoPassageiro();
                        break;

                    case 2:
                        Console.WriteLine("Editar companhia aerea");
                        Console.Clear();
                        companhiaAerea.AlteraCompanhia();
                        break;

                    case 3:
                        Console.WriteLine("Editar aeronave");
                        Console.Clear();
                        aeronave.AlteraDadoAeronave();
                        break;

                    case 4:
                        Console.WriteLine("Editar voo");
                        Console.Clear();
                        voo.AlteraDadoVoo();
                        break;

                    case 5:
                        Console.WriteLine("Editar passagem");
                        Console.Clear();
                        passagemVoo.AlteraDadoPassagem();
                        break;

                    case 6:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }

        static void MostrarMenuImprimir()
        {
            int opcao = 8;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            Voo voo = new();
            PassagemVoo passagemVoo = new();
            Venda venda = new();

            Console.WriteLine(" °°°  MENU  IMPRIMIR  °°°");
            Console.WriteLine(" Opção 1 : Imprime passageiros");
            Console.WriteLine(" Opção 2 : Imprime companhias aereas");
            Console.WriteLine(" Opção 3 : Imprime aeronaves");
            Console.WriteLine(" Opção 4 : Imprime voos");
            Console.WriteLine(" Opção 5 : Imprime passagens");
            Console.WriteLine(" Opção 6 : Imprime venda de passagens");
            Console.WriteLine(" Opção 7 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");

            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Imprime passageiro");
                        Console.Clear();
                        passageiro.ImprimiPassageiro();
                        break;

                    case 2:
                        Console.WriteLine("Imprime companhia aerea");
                        Console.Clear();
                        companhiaAerea.ImprCompanhia();
                        break;

                    case 3:
                        Console.WriteLine("Imprime aeronave");
                        Console.Clear();
                        aeronave.ImprimeAeronave();
                        break;

                    case 4:
                        Console.WriteLine("Imprime voo");
                        Console.Clear();
                        voo.ImprimeVoo();
                        break;

                    case 5:
                        Console.WriteLine("Imprime passagem");
                        Console.Clear();
                        passagemVoo.imprimePassagem();
                        break;

                    case 6:
                        Console.WriteLine("Imprime venda de passagem");
                        Console.Clear();
                        venda.Imprimir();
                        break;

                    case 7:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }

        static void MenuBloqueadosRestritos()
        {
            int opcao = 4;

            Bloqueado bloq = new();
            Restrito rest = new();

            Console.WriteLine(" °°°  MENU  BLOQUEADOS E RESTRITOS  °°°");
            Console.WriteLine(" Opção 1 : Restrição CPF");
            Console.WriteLine(" Opção 2 : Bloqueio CNPJ");
            Console.WriteLine(" Opção 3 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");
            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Restringir CPF");
                        Console.Clear();
                        rest.GerarMenu();
                        break;

                    case 2:
                        Console.WriteLine("Bloquear CNPJ");
                        Console.Clear();
                        bloq.GerarMenu();
                        break;

                    case 3:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }
    }
}
