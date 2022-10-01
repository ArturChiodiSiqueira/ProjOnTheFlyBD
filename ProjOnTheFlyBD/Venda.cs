using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoOnTheFlyBD
{
    internal class Venda
    {

        public int Id { get; set; }
        public string DataVenda { get; set; }
        public string Passageiro { get; set; }
        public string ValorTotal { get; set; }
        //public decimal ValorUnitario { get; set; }
        //Colocar id item venda?

        string caminhoitemvenda = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\ItemVenda.dat";
        string caminhoPassageiro = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Passageiro.dat";
        string caminhoVenda = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Venda.dat";
        string caminhorestrito = $"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Restritos.dat";

        public Venda()
        {

        }





        public Venda(int id, string dataVenda, string passageiro, string valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            Passageiro = passageiro;
            ValorTotal = valorTotal;

        }

        public bool BuscarPassageiro()
        {

            Console.WriteLine("Digite o cpf:");
            string cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
            foreach (string line in File.ReadLines(caminhorestrito))
            {

                if (line.Contains(cpf))
                {
                    Console.WriteLine("CPF Restrito. Contate a Receita Federal para maiores informações");
                    return false;
                }


            }
            foreach (string line in File.ReadLines(caminhoPassageiro))
            {
                if (line.Contains(cpf))
                {
                    DateTime verificadata = DateTime.Parse(line.Substring(62, 2) + "/" + line.Substring(64, 2) + "/" + line.Substring(66, 4));
                    if (verificadata > DateTime.Now.AddYears(-18))
                    {
                        Console.WriteLine("Não é possivel efetuar uma venda para menores de 18 anos!!\nPressione enter para continuar...");
                        Console.ReadKey();
                        return false;
                    }


                    Console.WriteLine($"CPF: {line.Substring(0, 11)}");
                    Console.WriteLine($"Nome: {line.Substring(11, 50)}");
                    Console.WriteLine($"Data de Nascimento: {line.Substring(62, 2)}/{line.Substring(64, 2)}/{line.Substring(66, 4)}");

                    Console.WriteLine("Confirma os dados? (s/n)");
                    char resp = char.Parse(Console.ReadLine().ToLower());

                    if (resp != 's' && resp != 'n')
                    {
                        Console.WriteLine("Insira uma resposta valida! (s/n)");
                    }

                    if (resp == 's')
                    {
                        Passageiro = line.Substring(0, 11); //insere o cpf do passageiro comprador
                        return true;
                    }

                    else
                    {
                        return false;
                    }


                }

            }

            return false;

        }

        protected int GetID()
        {
            string[] contador = System.IO.File.ReadAllLines($"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Id.dat");
            string[] num = new string[1];
            foreach (string cont in contador)
                num[0] = cont;
            int id = int.Parse(num[0]);
            return id;
        }
        //Método para devolver o Id no arquivo
        protected void SaveID(int id)
        {
            StreamWriter idPessoa = new StreamWriter($"C:\\Users\\artur\\source\\repos\\ProjetoOnTheFly\\ProjetoOnTheFly\\Dados\\Id.dat");
            idPessoa.WriteLine(id.ToString("00000"));
            idPessoa.Close();
        }




        public void Cadastrar()
        //Metodo para cadastrar venda ou reserva, quando realizar uma venda/reserva, esse metodo é chamado
        {

            Id = GetID();

            if (!BuscarPassageiro())
            {
                return;
            }
            DataVenda = DateTime.Now.ToString("ddMMyyyy");

            ItemVenda item = new(Id);


            ValorTotal = item.CadastrarItemVenda();

            string texto = $"{ToString()}\n";
            File.AppendAllText(caminhoVenda, texto);
            Id++;
            SaveID(Id);
            Console.WriteLine("Venda realizada com sucesso!\nPressione qualquer tecla para continuar...");
            Console.ReadKey();


        }



        public void Imprimir()

        {
            string[] lines = File.ReadAllLines(caminhoVenda);
            List<string> venda = new();
            for (int i = 1; i < lines.Length; i++)
            {
                //Verifica se o cadastro esta ativo
                //if (lines[i].Substring(89, 1).Contains("A"))
                venda.Add(lines[i]);
            }
            //Laço para navegar nos cadastros das vendas
            for (int i = 0; i < venda.Count; i++)
            {
                string op;
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>> Vendas  <<<\nDigite para navegar:\n[1] Próxima venda\n[2] venda Anterior" +
                        "\n[3] Última venda\n[4] Voltar ao Início\n[s] Sair\n");
                    Console.WriteLine($"Cadastro [{i + 1}] de [{venda.Count}]");
                    //Imprimi o primeiro da lista
                    string[] vendas = File.ReadAllLines(caminhoVenda);
                    string[] itemVenda = File.ReadAllLines(caminhoitemvenda);


                    for (int j = 1; j < vendas.Length; j++)
                    {


                        Console.WriteLine($"\nID DA VENDA: {vendas[j].Substring(0, 5)} ");
                        Console.WriteLine($"CPF DO PASSAGEIRO COMPRADOR : {vendas[j].Substring(5, 11)}");
                        Console.WriteLine($"DATA DA VENDA: {vendas[j].Substring(16, 2)}/{vendas[j].Substring(18, 2)}/ {vendas[j].Substring(20, 4)}");
                        Console.WriteLine($"VALOR TOTAL: R${vendas[j].Substring(24, 7)}\n");

                        for (int l = 0; l <= itemVenda.Length; l++)
                        {
                            try
                            {


                                if (itemVenda[l].Substring(1, 5).Contains(vendas[l].Substring(0, 5)))
                                {

                                    Console.WriteLine($"\nID item venda: {itemVenda[l].Substring(0, 5)} ");
                                    Console.WriteLine($"ID da passagem : {itemVenda[l].Substring(5, 6)}");
                                    Console.WriteLine($"Valor unitario: R${itemVenda[l].Substring(11, 6)}");

                                }
                            }
                            catch
                            {
                                break;
                            }


                        }

                    }

                    Console.Write("Opção: ");
                    op = Console.ReadLine().ToLower();
                    if (op != "s" && op != "1" && op != "2" && op != "3" && op != "4")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(2000);
                    }
                    //Sai do método
                    else if (op.Contains("s"))
                        return;
                    //Volta no Cadastro Anterior
                    else if (op.Contains("2"))
                        if (i == 0)
                            i = 0;
                        else
                            i--;
                    //Vai para o fim da lista
                    else if (op.Contains("3"))
                        i = venda.Count - 1;
                    //Volta para o inicio da lista
                    else if (op.Contains("4"))
                        i = 0;
                    //Vai para o próximo da lista
                } while (op != "1");
                if (i == venda.Count - 1)
                    i--;
            }

        }

        public void Localizar()
        //metodo para imprimir todas as passagens
        {
            Console.Clear();
            Console.WriteLine("Digite o cpf do comprador para gerar a nota fiscal");
            string cpf = Console.ReadLine();
            string[] venda = File.ReadAllLines(caminhoVenda);
            string[] itemVenda = File.ReadAllLines(caminhoitemvenda);


            for (int i = 1; i < venda.Length; i++)
            {
                if (venda[i].Substring(5, 11).Contains(cpf))
                {
                    Console.WriteLine($"\nID DA VENDA: {venda[i].Substring(0, 5)} ");
                    Console.WriteLine($"CPF DO PASSAGEIRO COMPRADOR : {venda[i].Substring(5, 11)}");
                    Console.WriteLine($"DATA DA VENDA: {venda[i].Substring(16, 2)}/{venda[i].Substring(18, 2)}/ {venda[i].Substring(20, 4)}");
                    Console.WriteLine($"VALOR TOTAL: R${venda[i].Substring(24, 7)}\n");

                    for (int j = 0; j <= itemVenda.Length; j++)
                    {
                        try
                        {


                            if (itemVenda[j].Substring(1, 5).Contains(venda[i].Substring(0, 5)))
                            {

                                Console.WriteLine($"\nID item venda: {itemVenda[i].Substring(0, 5)} ");
                                Console.WriteLine($"ID da passagem : {itemVenda[i].Substring(5, 6)}");
                                Console.WriteLine($"Valor unitario: R${itemVenda[i].Substring(11, 6)}");

                            }
                        }
                        catch
                        {
                            break;
                        }


                    }


                }
            }




        }

        public void Cancelar()
        //metodo para cancelar as reservas
        {

        }

        public override string ToString()
        {
            return $"{Id.ToString("00000")}{Passageiro}{DataVenda}{ValorTotal}";


        }



    }
}
