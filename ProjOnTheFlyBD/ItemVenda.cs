using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class ItemVenda
    {
        public int Id { get; set; }
        public string IdPassagem { get; set; }
        public double ValorUnitario { get; set; }
        public int IdVenda { get; set; }

        Banco conn = new Banco();

        public ItemVenda()
        {
        }

        public string CadastrarItemVenda()
        {
            string pass;
            int cont = 0;
            float valortotal = 0;
            do
            {
                cont++;
                Console.Clear();

                pass = ListarPassagens();

                if (pass != "0")
                    IdPassagem = pass.Substring(0, 5);

                ValorUnitario = pass.Substring(5, 6);

                valortotal += float.Parse(ValorUnitario);

                Console.WriteLine("Compra realizada com sucesso!");
                Console.WriteLine("Voce comprou " + cont + " Passagens");
                //valortotal = valortotal + float.Parse(ValorUnitario);
                Console.WriteLine("Deseja comprar novamente (s/n)?");
                string op = Console.ReadLine().ToLower();
                if (op != "s" && op != "n")
                {
                    Console.WriteLine("Insira uma resposta valida! (s/n)");
                }
                if (op == "n")
                {
                    break;
                }
                // PassagensCompradas = cont;
            } while (cont < 4);
            if (cont == 4)
            {
                Console.WriteLine("Limite de passagens atingindo!\nPressione enter para continuar");
                Console.ReadKey();
            }
            return valortotal.ToString("0000000");
        }
        public string ListarPassagens()
        {
            Console.WriteLine("VOOS DISPONIVEIS:");
            string[] voos = File.ReadAllLines(caminhoVoo);
            List<string> voo = new();
            for (int i = 0; i < voos.Length; i++)
            {
                if (voos[i].Substring(33, 1).Contains("A"))
                {
                    voo.Add(voos[i]);
                }
            }
            for (int i = 0; i < voo.Count; i++)
            {
                string op;
                do
                {
                    Console.WriteLine("\nDigite para navegar:\n[1] Próximo voo\n[2] Voo Anterior" +
                        "\n[3] Último voo\n[4] Voltar ao Início\n[5] Para comprar passagens para esse voo \n[0] Sair\n");
                    Console.WriteLine($"Cadastro [{i + 1}] de [{voo.Count}]");
                    //Imprimi o primeiro da lista
                    ImprimeVoo(caminhoVoo, voo[i].Substring(0, 5)); //Imprime os voos disponiveis
                    Console.Write("Opção: ");
                    op = Console.ReadLine().ToLower();
                    if (op != "0" && op != "1" && op != "2" && op != "3" && op != "4" && op != "5")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(2000);
                    }
                    else if (op == "5")
                    {
                        foreach (string passagem in File.ReadLines(caminhoPassagemVoo))
                        {
                            //Console.WriteLine(passagem.Substring(7, 5));
                            //Console.ReadKey();
                            if (passagem.Substring(6, 5).Contains(voo[i].Substring(0, 5)))
                            {
                                return passagem.Substring(6, 5) + passagem.Substring(20, 7);
                            }
                        }
                    }
                    //Sai do método
                    else if (op.Contains("0"))
                        return "0";
                    //Volta no Cadastro Anterior
                    else if (op.Contains("2"))
                        if (i == 0)
                            i = 0;
                        else
                            i--;
                    //Vai para o fim da lista
                    else if (op.Contains("3"))
                        i = voo.Count - 1;
                    //Volta para o inicio da lista
                    else if (op.Contains("4"))
                        i = 0;
                    //Vai para o próximo da lista
                } while (op != "1");
                i = 0;
            }
            return "0";
        }
        public void ImprimeVoo(string caminho, string IdVoo)
        {
            foreach (string passagem in File.ReadLines(caminho))
            {
                if (passagem.Contains(IdVoo))
                {
                    Console.WriteLine($"Id do voo: {passagem.Substring(0, 5)} ");
                    Console.WriteLine($"Destino do voo: {passagem.Substring(5, 3)}");
                    Console.WriteLine($"Id da aronave: {passagem.Substring(8, 2)}-{passagem.Substring(10, 3)}");
                    Console.WriteLine($"Data do voo: {passagem.Substring(13, 2)}/{passagem.Substring(15, 2)}/" +
                        $"{passagem.Substring(17, 4)}\nHora do voo: {passagem.Substring(21, 2)}:{passagem.Substring(23, 2)}");
                    if (passagem.Substring(33, 1).Contains("A"))
                        Console.WriteLine($"Situação do Voo: Ativo");
                    else
                        Console.WriteLine($"Situação do Voo: Cancelado");
                }
            }
        }
        public override string ToString()
        {
            return $"{Id}{IdPassagem}{ValorUnitario}";
        }
    }
}