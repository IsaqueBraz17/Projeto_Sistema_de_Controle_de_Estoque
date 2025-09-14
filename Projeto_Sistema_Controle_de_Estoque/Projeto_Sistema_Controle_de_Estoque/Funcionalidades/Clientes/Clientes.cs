using System;
using System.Data.SqlClient;

namespace Projeto_Sistema_Controle_de_Estoque.Funcionalidades.Clientes
{
    internal class Clientes
    {
        // String de conexão com o banco de dados
        private static readonly string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_ControleEstoque;Trusted_Connection=True;";

        public Clientes()
        {
            MenuClientes();
        }

        public static void MenuClientes()
        {
            Console.WriteLine("Menu de Clientes:");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Atualizar Cliente");
            Console.WriteLine("4 - Deletar Cliente");
            Console.WriteLine("5 - Menu Principal");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            int opcao_clientes = int.Parse(Console.ReadLine());
            if (opcao_clientes == 1)
            {
                CadastrarCliente();
            }
            else if (opcao_clientes == 2)
            {
                ListarClientes();
            }
            else if (opcao_clientes == 3)
            {
                AtualizarCliente();
            }
            else if (opcao_clientes == 4)
            {
                DeletarCliente();
            }
            else if (opcao_clientes == 5)
            {
                Console.WriteLine("Retornando ao Menu Principal.");
                Program.MenuPrincipal();
            }
            else if (opcao_clientes == 0)
            {
                Console.WriteLine("Saindo do sistema. Até logo!");
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                MenuClientes();
            }
        }

        // Verifica a conexão com o banco de dados
        public static void TestarConexao()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possível conectar ao banco de dados.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        // 1 - Cadastrar Cliente
        private static void CadastrarCliente()
        {
            do
            {
                Console.Write("Nome do Cliente: ");
                string nome = Console.ReadLine();
                Console.Write("CPF (somente números): ");
                string cpf = Console.ReadLine();
                Console.Write("RG: ");
                string rg = Console.ReadLine();
                Console.Write("Telefone: ");
                string telefone = Console.ReadLine();
                Console.Write("CEP: ");
                string cep = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Clientes (nome_cliente, cpf, rg, telefone, cep) VALUES (@nome, @cpf, @rg, @telefone, @cep)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@rg", rg);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@cep", cep);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Cliente cadastrado com sucesso!");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Violação de chave única (CPF duplicado)
                            Console.WriteLine("Erro: CPF já cadastrado.");
                        else
                            Console.WriteLine("Erro ao cadastrar cliente: " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            } while (ExibirOpcoesPosAcao("cadastrar"));
        }

        // 2 - Listar Clientes
        private static void ListarClientes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id_cliente, nome_cliente, cpf, rg, telefone, cep FROM Clientes";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("ID | Nome | CPF | RG | Telefone | CEP");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["id_cliente"]} | {reader["nome_cliente"]} | {reader["cpf"]} | {reader["rg"]} | {reader["telefone"]} | {reader["cep"]}");
                }
                conn.Close();
            }
            ExibirOpcoesPosAcao("listar");
        }

        // 3 - Atualizar Cliente
        private static void AtualizarCliente()
        {
            do
            {
                Console.Write("Informe o ID do cliente a ser atualizado: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Novo nome do Cliente: ");
                string nome = Console.ReadLine();
                Console.Write("Novo CPF (somente números): ");
                string cpf = Console.ReadLine();
                Console.Write("Novo RG: ");
                string rg = Console.ReadLine();
                Console.Write("Novo Telefone: ");
                string telefone = Console.ReadLine();
                Console.Write("Novo CEP: ");
                string cep = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Clientes SET nome_cliente=@nome, cpf=@cpf, rg=@rg, telefone=@telefone, cep=@cep WHERE id_cliente=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@rg", rg);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@cep", cep);
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            Console.WriteLine("Cliente atualizado com sucesso!");
                        else
                            Console.WriteLine("Cliente não encontrado.");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Violação de chave única (CPF duplicado)
                            Console.WriteLine("Erro: CPF já cadastrado para outro cliente.");
                        else
                            Console.WriteLine("Erro ao atualizar cliente: " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            } while (ExibirOpcoesPosAcao("atualizar"));
        }

        // 4 - Deletar Cliente
        private static void DeletarCliente()
        {
            do
            {
                Console.Write("Informe o ID do cliente a ser deletado: ");
                int id = int.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Clientes WHERE id_cliente=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                        Console.WriteLine("Cliente deletado com sucesso!");
                    else
                        Console.WriteLine("Cliente não encontrado.");
                }
            } while (ExibirOpcoesPosAcao("deletar"));
        }

        // Exibe opções após concluir uma ação
        private static bool ExibirOpcoesPosAcao(string acao)
        {
            Console.WriteLine();
            Console.WriteLine("O que deseja fazer agora?");
            Console.WriteLine("1 - Voltar ao menu de clientes");
            Console.WriteLine("2 - Voltar ao menu principal");
            Console.WriteLine("3 - Sair do sistema");
            if (acao == "cadastrar") Console.WriteLine("4 - Cadastrar outro cliente");
            if (acao == "atualizar") Console.WriteLine("4 - Atualizar outro cliente");
            if (acao == "deletar") Console.WriteLine("4 - Deletar outro cliente");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    MenuClientes();
                    return false;
                case "2":
                    Program.MenuPrincipal();
                    return false;
                case "3":
                    Console.WriteLine("Saindo do sistema. Até logo!");
                    Environment.Exit(0);
                    return false;
                case "4":
                    return true;
                default:
                    Console.WriteLine("Opção inválida. Retornando ao menu de clientes.");
                    MenuClientes();
                    return false;
            }
        }
    }
}