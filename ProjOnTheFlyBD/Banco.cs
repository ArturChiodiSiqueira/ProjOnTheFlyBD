using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOnTheFlyBD
{
    internal class Banco
    {
        private static string Conexao = "Data Source=localhost;Initial Catalog=OnTheFlyBD;User id=sa;Password=cear2712;";

        private static SqlConnection Conexaosql = new SqlConnection(Conexao);

        public Banco()
        {
        }

        public string Caminho()
        {
            return Conexao;
        }

        public void Insert(string query)
        {
            try
            {
                Conexaosql.Open();

                SqlCommand cmd = new SqlCommand(query, Conexaosql);

                cmd.ExecuteNonQuery();

                Conexaosql.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Conexaosql.Close();
                Console.ReadKey();
            }
        }

        public bool Select(string query, int opcao)
        {
            bool retorna = false;
            try
            {
                Conexaosql.Open();

                SqlCommand cmd = new SqlCommand(query, Conexaosql);

                cmd.ExecuteNonQuery();

                switch (opcao)
                {
                    case 1:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("CPF: {0}", leitor.GetString(0));
                                Console.WriteLine("Nome: {0}", leitor.GetString(1));
                                Console.WriteLine("Data Nascimento: {0}", leitor.GetDateTime(2).ToShortDateString());
                                Console.WriteLine("Sexo: {0}", leitor.GetString(3));
                                Console.WriteLine("Ultima Compra: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Data Cadastro: {0}", leitor.GetDateTime(5).ToShortDateString());
                                Console.WriteLine("Situação: {0}", leitor.GetString(6));
                                retorna = true;
                            }
                        }
                        break;

                    case 2:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.Write(" CPF: {0}", leitor.GetString(0));
                                retorna = true;
                            }
                        }
                        break;

                    case 3:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("CNPJ: {0}", leitor.GetString(0));
                                Console.WriteLine("Razão Social: {0}", leitor.GetString(1));
                                Console.WriteLine("Data Abertura: {0}", leitor.GetDateTime(2).ToShortDateString());
                                Console.WriteLine("Ultimo Voo: {0}", leitor.GetDateTime(3).ToShortDateString());
                                Console.WriteLine("Data Cadastro: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Situação: {0}", leitor.GetString(5));
                                retorna = true;
                            }
                        }
                        break;

                    case 4:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("Inscrição: {0}", leitor.GetString(0));
                                Console.WriteLine("Cnpj: {0}", leitor.GetString(1));
                                Console.WriteLine("Capacidade: {0}", leitor.GetInt32(2));
                                Console.WriteLine("Assentos Ocupados: {0}", leitor.GetString(3));
                                Console.WriteLine("Ultima Venda: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Data Cadastro: {0}", leitor.GetDateTime(5).ToShortDateString());
                                Console.WriteLine("Situação: {0}", leitor.GetString(6));
                                retorna = true;
                            }
                        }
                        break;

                    case 5:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.Write(" CNPJ: {0}", leitor.GetString(0));
                                retorna = true;
                            }
                        }
                        break;

                    case 6:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("ID Voo: {0}", leitor.GetString(0));
                                Console.WriteLine("Inscrição da aeronave operante: {0}", leitor.GetString(1));
                                Console.WriteLine("Destino (iata): {0}", leitor.GetString(2));
                                Console.WriteLine("Data Voo: {0}", leitor.GetString(3));
                                Console.WriteLine("Data Cadastro: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Assentos Ocupados: {0}", leitor.GetInt32(5));
                                Console.WriteLine("Situação: {0}", leitor.GetString(6));
                                retorna = true;
                            }
                        }
                        break;

                    case 7:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("ID Passagem: {0}", leitor.GetString(0));
                                Console.WriteLine("ID Voo: {0}", leitor.GetString(1));
                                Console.WriteLine("Data Ultima Operação: {0}", leitor.GetDateTime(2).ToShortDateString());
                                Console.WriteLine("Valor: {0}", leitor.GetDouble(3));
                                Console.WriteLine("Situação: {0}", leitor.GetString(4));
                                retorna = true;
                            }
                        }
                        break;

                    case 8:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("ID: {0}", leitor.GetInt32(0));
                                Console.WriteLine("ID Venda: {0}", leitor.GetInt32(1));
                                Console.WriteLine("ID Passagem: {0}", leitor.GetString(2));
                                Console.WriteLine("Valor Unitario: {0}", leitor.GetDouble(3));
                                retorna = true;
                            }
                        }
                        break;

                    case 9:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Console.WriteLine("\nRegistro Encontrado:\n");
                                Console.WriteLine("ID: {0}", leitor.GetInt32(0));
                                Console.WriteLine("CPF Vinculado: {0}", leitor.GetString(1));
                                Console.WriteLine("Data Venda: {0}", leitor.GetDateTime(2).ToShortDateString());
                                Console.WriteLine("Total Vendas: {0}", leitor.GetDouble(3));
                                retorna = true;
                            }
                        }
                        break;

                    case 0:
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                Console.WriteLine(" {0} Registro já existente", leitor.GetString(0));
                                retorna = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
                Conexaosql.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Conexaosql.Close();
                Console.ReadKey();
            }
            return retorna;
        }

        public void Update(string query)
        {
            try
            {
                Conexaosql.Open();

                SqlCommand cmd = new SqlCommand(query, Conexaosql);

                cmd.ExecuteNonQuery();

                Conexaosql.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Conexaosql.Close();
                Console.ReadKey();
            }
        }

        public void Delete(string query)
        {
            try
            {
                Conexaosql.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.ExecuteNonQuery();

                Conexaosql.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Conexaosql.Close();
                Console.ReadKey();
            }
        }

        public bool VerificaDados(string dado, string campo, string tabela)
        {
            string queryString = $"SELECT {campo} FROM {tabela} WHERE {campo} = '{dado}'";
            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);
                Conexaosql.Open();
                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    if (leitor.Read())
                    {
                        Conexaosql.Close();
                        return true;
                    }
                    else
                    {
                        Conexaosql.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Conexaosql.Close();
                Console.ReadKey();
                return false;
            }
        }

        public int GetCapacidade(string inscricao)
        {

            string query = $"SELECT Inscricao, Cnpj,Capacidade, UltimaVenda, DataCadastro, Situacao FROM Aeronave WHERE Inscricao = '{inscricao}';";

            int capacidade = 0;

            try
            {
                SqlCommand command = new SqlCommand(query, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        capacidade = leitor.GetInt32(2);
                    }
                }
                Conexaosql.Close();
                return capacidade;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.ReadKey();
                return 0;
            }
        }

        public string GetVoo(string idPassagem)
        {
            string queryString = $"SELECT Inscricao FROM Voo WHERE ID = '{idPassagem}';";

            string valor = "";

            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        valor = leitor.GetString(0);
                    }
                }
                Conexaosql.Close();
                return valor;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return "";
            }
        }

        public double GetValor(string IdPassagemVoo)
        {
            string query = $"SELECT ID, IDVoo, DataUltimaOperacao, Valor, Situacao FROM PassagemVoo WHERE ID = '{IdPassagemVoo}';";

            double valor = 0;

            try
            {
                SqlCommand command = new SqlCommand(query, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        valor = leitor.GetDouble(2);
                    }
                }
                Conexaosql.Close();
                return valor;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.ReadKey();
                return 0;
            }
        }

        public string GetIdAeronave(string idPassagem)
        {
            string query = $"SELECT IDVoo FROM PassagemVoo WHERE ID = '{idPassagem}';";

            string voo = "";
            string aeronave = "";
            try
            {
                SqlCommand command = new SqlCommand(query, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        voo = leitor.GetString(0);
                    }
                }
                Conexaosql.Close();
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.ReadKey();
                return "";
            }

            string queryy = $"SELECT Inscricao FROM Voo WHERE ID = '{voo}';";

            try
            {
                SqlCommand command = new SqlCommand(queryy, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        aeronave = leitor.GetString(0);
                    }
                }
                Conexaosql.Close();
                return aeronave;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.ReadKey();
                return "";
            }
        }

        public int GetAssentos(string voo)
        {
            string queryString = $"SELECT ID, AssentosOcupados FROM Voo WHERE ID = '{voo}';";

            int valor = 0;

            try
            {
                SqlCommand command = new SqlCommand(queryString, Conexaosql);

                Conexaosql.Open();

                using (SqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        valor = leitor.GetInt32(0);
                    }
                }
                Conexaosql.Close();
                return valor;
            }
            catch (Exception e)
            {
                Conexaosql.Close();
                Console.WriteLine("Erro ao comunicar com o banco\n" + e.Message + "\nTecle [ENTER] para continuar.");
                Console.ReadKey();
                return 0;
            }
        }
    }
}
