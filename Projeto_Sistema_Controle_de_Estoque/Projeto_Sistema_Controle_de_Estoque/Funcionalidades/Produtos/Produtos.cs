using System;
using System.Data.SqlClient;

namespace Projeto_Sistema_Controle_de_Estoque.Funcionalidades.Produtos
{
    internal class Produtos
    {
        // String de conexão com o banco de dados
        private static readonly string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_ControleEstoque;Trusted_Connection=True;";

        public Produtos()
        {
            MenuProdutos();
        }

        public static void MenuProdutos()
        {
            Console.WriteLine("Menu de Produtos:");
            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Listar Produtos");
            Console.WriteLine("3 - Atualizar Produto");
            Console.WriteLine("4 - Deletar Produto");
            Console.WriteLine("5 - Menu Principal");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            int opcao_produtos = int.Parse(Console.ReadLine());

            if (opcao_produtos == 1)
            {
                CadastrarProduto();
            }
            else if (opcao_produtos == 2)
            {
                ListarProdutos();
            }
            else if (opcao_produtos == 3)
            {
                AtualizarProduto();
            }
            else if (opcao_produtos == 4)
            {
                DeletarProduto();
            }
            else if (opcao_produtos == 5)
            {
                Console.WriteLine("Retornando ao Menu Principal.");
                Program.MenuPrincipal();
            }
            else if (opcao_produtos == 0)
            {
                Console.WriteLine("Saindo do sistema. Até logo!");
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                MenuProdutos();
            }
        }

        // 1 - Cadastrar Produto
        [Obsolete]
        private static void CadastrarProduto()
        {
            do
            {
                Console.Write("Nome do Produto: ");
                string nome = Console.ReadLine();
                Console.Write("Categoria: ");
                string categoria = Console.ReadLine();
                Console.Write("Quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());
                Console.Write("Valor do Produto: ");
                decimal valor = decimal.Parse(Console.ReadLine());
                Console.Write("ID do Fornecedor: ");
                int idFornecedor = int.Parse(Console.ReadLine());

                if (!FornecedorExiste(idFornecedor))
                {
                    Console.WriteLine("ID do Fornecedor não existe. Cadastro cancelado.");
                }
                else
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Produtos (nome_produto, categoria, quantidade, valor_produto, id_fornecedor) VALUES (@nome, @categoria, @quantidade, @valor, @idFornecedor)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.Parameters.AddWithValue("@quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    Console.WriteLine("Produto cadastrado com sucesso!");
                }
            } while (ExibirOpcoesPosAcao("cadastrar"));
        }

        // 2 - Listar Produtos
        private static void ListarProdutos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id_produto, nome_produto, categoria, quantidade, valor_produto, id_fornecedor FROM Produtos";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("ID | Nome | Categoria | Quantidade | Valor | ID Fornecedor");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["id_produto"]} | {reader["nome_produto"]} | {reader["categoria"]} | {reader["quantidade"]} | {reader["valor_produto"]} | {reader["id_fornecedor"]}");
                }
                conn.Close();
            }
            ExibirOpcoesPosAcao("listar");
        }

        // 3 - Atualizar Produto
        private static void AtualizarProduto()
        {
            do
            {
                Console.Write("Informe o ID do produto a ser atualizado: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Novo nome do Produto: ");
                string nome = Console.ReadLine();
                Console.Write("Nova categoria: ");
                string categoria = Console.ReadLine();
                Console.Write("Nova quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());
                Console.Write("Novo valor do Produto: ");
                decimal valor = decimal.Parse(Console.ReadLine());
                Console.Write("Novo ID do Fornecedor: ");
                int idFornecedor = int.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Produtos SET nome_produto=@nome, categoria=@categoria, quantidade=@quantidade, valor_produto=@valor, id_fornecedor=@idFornecedor WHERE id_produto=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@categoria", categoria);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                        Console.WriteLine("Produto atualizado com sucesso!");
                    else
                        Console.WriteLine("Produto não encontrado.");
                }
            } while (ExibirOpcoesPosAcao("atualizar"));
        }

        // 4 - Deletar Produto
        private static void DeletarProduto()
        {
            do
            {
                Console.Write("Informe o ID do produto a ser deletado: ");
                int id = int.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Produtos WHERE id_produto=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                        Console.WriteLine("Produto deletado com sucesso!");
                    else
                        Console.WriteLine("Produto não encontrado.");
                }
            } while (ExibirOpcoesPosAcao("deletar"));
        }

        // Método para testar a conexão com o banco de dados
        [Obsolete]
        private static void TestarConexao()
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

        private static bool FornecedorExiste(int idFornecedor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Fornecedores WHERE id_fornecedor = @idFornecedor";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count > 0;
            }
        }

        // Exibe opções após concluir uma ação
        private static bool ExibirOpcoesPosAcao(string acao)
        {
            Console.WriteLine();
            Console.WriteLine("O que deseja fazer agora?");
            Console.WriteLine("1 - Voltar ao menu de produtos");
            Console.WriteLine("2 - Voltar ao menu principal");
            Console.WriteLine("3 - Sair do sistema");
            if (acao == "cadastrar") Console.WriteLine("4 - Cadastrar outro produto");
            if (acao == "atualizar") Console.WriteLine("4 - Atualizar outro produto");
            if (acao == "deletar") Console.WriteLine("4 - Deletar outro produto");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    MenuProdutos();
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
                    Console.WriteLine("Opção inválida. Retornando ao menu de produtos.");
                    MenuProdutos();
                    return false;
            }
        }
    }
}