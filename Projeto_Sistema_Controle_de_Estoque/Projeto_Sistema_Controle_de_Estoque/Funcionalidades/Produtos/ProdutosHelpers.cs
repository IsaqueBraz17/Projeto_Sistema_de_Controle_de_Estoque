using System.Data.SqlClient;

namespace Projeto_Sistema_Controle_de_Estoque.Funcionalidades.Produtos
{
    internal static class ProdutosHelpers
    {

        // 1 - Cadastrar Produto
        [Obsolete]
        private static void CadastrarProduto()
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
                return;
            }

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
    }
}