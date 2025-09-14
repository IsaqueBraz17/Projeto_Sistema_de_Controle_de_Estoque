using System;
using System.Data.SqlClient;

namespace Projeto_Sistema_Controle_de_Estoque.Funcionalidades.Fornecedores
{
    internal class Fornecedores
    {
        // String de conexão com o banco de dados
        private static readonly string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_ControleEstoque;Trusted_Connection=True;";

        public Fornecedores()
        {
            MenuFornecedores();
        }

        public static void MenuFornecedores()
        {
            Console.WriteLine("Menu de Fornecedores:");
            Console.WriteLine("1 - Cadastrar Fornecedor");
            Console.WriteLine("2 - Listar Fornecedores");
            Console.WriteLine("3 - Atualizar Fornecedor");
            Console.WriteLine("4 - Deletar Fornecedor");
            Console.WriteLine("5 - Menu Principal");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            int opcao_fornecedores = int.Parse(Console.ReadLine());
            if (opcao_fornecedores == 1)
            {
                CadastrarFornecedor();
            }
            else if (opcao_fornecedores == 2)
            {
                ListarFornecedores();
            }
            else if (opcao_fornecedores == 3)
            {
                AtualizarFornecedor();
            }
            else if (opcao_fornecedores == 4)
            {
                DeletarFornecedor();
            }
            else if (opcao_fornecedores == 5)
            {
                Console.WriteLine("Retornando ao Menu Principal.");
                Program.MenuPrincipal();
            }
            else if (opcao_fornecedores == 0)
            {
                Console.WriteLine("Saindo do sistema. Até logo!");
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                MenuFornecedores();
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

        // 1 - Cadastrar Fornecedor
        private static void CadastrarFornecedor()
        {
            do
            {
                Console.Write("Nome do Fornecedor: ");
                string nome = Console.ReadLine();
                Console.Write("CNPJ (somente números): ");
                string cnpj = Console.ReadLine();
                Console.Write("País: ");
                string pais = Console.ReadLine();
                Console.Write("Endereço: ");
                string endereco = Console.ReadLine();
                Console.Write("Telefone: ");
                string telefone = Console.ReadLine();
                Console.Write("Categoria: ");
                string categoria = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Fornecedores (nome_fornecedor, cnpj, pais, endereco, telefone, categoria) VALUES (@nome, @cnpj, @pais, @endereco, @telefone, @categoria)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cnpj", cnpj);
                    cmd.Parameters.AddWithValue("@pais", pais);
                    cmd.Parameters.AddWithValue("@endereco", endereco);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@categoria", categoria);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Fornecedor cadastrado com sucesso!");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Violação de chave única (CNPJ duplicado)
                            Console.WriteLine("Erro: CNPJ já cadastrado.");
                        else
                            Console.WriteLine("Erro ao cadastrar fornecedor: " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            } while (ExibirOpcoesPosAcao("cadastrar"));
        }

        // 2 - Listar Fornecedores
        private static void ListarFornecedores()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id_fornecedor, nome_fornecedor, cnpj, pais, endereco, telefone, categoria FROM Fornecedores";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("ID | Nome | CNPJ | País | Endereço | Telefone | Categoria");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["id_fornecedor"]} | {reader["nome_fornecedor"]} | {reader["cnpj"]} | {reader["pais"]} | {reader["endereco"]} | {reader["telefone"]} | {reader["categoria"]}");
                }
                conn.Close();
            }
            ExibirOpcoesPosAcao("listar");
        }

        // 3 - Atualizar Fornecedor
        private static void AtualizarFornecedor()
        {
            do
            {
                Console.Write("Informe o ID do fornecedor a ser atualizado: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Novo nome do Fornecedor: ");
                string nome = Console.ReadLine();
                Console.Write("Novo CNPJ (somente números): ");
                string cnpj = Console.ReadLine();
                Console.Write("Novo País: ");
                string pais = Console.ReadLine();
                Console.Write("Novo Endereço: ");
                string endereco = Console.ReadLine();
                Console.Write("Novo Telefone: ");
                string telefone = Console.ReadLine();
                Console.Write("Nova Categoria: ");
                string categoria = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Fornecedores SET nome_fornecedor=@nome, cnpj=@cnpj, pais=@pais, endereco=@endereco, telefone=@telefone, categoria=@categoria WHERE id_fornecedor=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cnpj", cnpj);
                    cmd.Parameters.AddWithValue("@pais", pais);
                    cmd.Parameters.AddWithValue("@endereco", endereco);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@categoria", categoria);
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            Console.WriteLine("Fornecedor atualizado com sucesso!");
                        else
                            Console.WriteLine("Fornecedor não encontrado.");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Violação de chave única (CNPJ duplicado)
                            Console.WriteLine("Erro: CNPJ já cadastrado para outro fornecedor.");
                        else
                            Console.WriteLine("Erro ao atualizar fornecedor: " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            } while (ExibirOpcoesPosAcao("atualizar"));
        }

        // 4 - Deletar Fornecedor
        private static void DeletarFornecedor()
        {
            do
            {
                Console.Write("Informe o ID do fornecedor a ser deletado: ");
                int id = int.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Fornecedores WHERE id_fornecedor=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                        Console.WriteLine("Fornecedor deletado com sucesso!");
                    else
                        Console.WriteLine("Fornecedor não encontrado.");
                }
            } while (ExibirOpcoesPosAcao("deletar"));
        }

        // Exibe opções após concluir uma ação
        private static bool ExibirOpcoesPosAcao(string acao)
        {
            Console.WriteLine();
            Console.WriteLine("O que deseja fazer agora?");
            Console.WriteLine("1 - Voltar ao menu de fornecedores");
            Console.WriteLine("2 - Voltar ao menu principal");
            Console.WriteLine("3 - Sair do sistema");
            if (acao == "cadastrar") Console.WriteLine("4 - Cadastrar outro fornecedor");
            if (acao == "atualizar") Console.WriteLine("4 - Atualizar outro fornecedor");
            if (acao == "deletar") Console.WriteLine("4 - Deletar outro fornecedor");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    MenuFornecedores();
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
                    Console.WriteLine("Opção inválida. Retornando ao menu de fornecedores.");
                    MenuFornecedores();
                    return false;
            }
        }
    }
}