using Projeto_Sistema_Controle_de_Estoque.Funcionalidades.Clientes;
using Projeto_Sistema_Controle_de_Estoque.Funcionalidades.Produtos;
using System;

namespace Projeto_Sistema_Controle_de_Estoque
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Sistema de Controle de Estoque!");
            MenuPrincipal();
        }

        // método separado dentro da classe Program
        public static void MenuPrincipal()
        {
            Console.WriteLine("Menu Principal:");
            Console.WriteLine("1 - Produtos");
            Console.WriteLine("2 - Fornecedores");
            Console.WriteLine("3 - Clientes");
            Console.WriteLine("4 - Relatorios");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string input = Console.ReadLine();
            int opcao_principal = 0;
            if (!int.TryParse(input, out opcao_principal))
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                MenuPrincipal();
                return;
            }

            if (opcao_principal == 1)
            {
                Console.WriteLine("Opção Produtos selecionada.");
                var produtos = new Produtos();
            }
            else if (opcao_principal == 2)
            {
                Console.WriteLine("Opção Fornecedores selecionada.");
            }
            else if (opcao_principal == 3)
            {
                Console.WriteLine("Opção Clientes selecionada.");
                var Clientes = new Clientes();
            }
            else if (opcao_principal == 4)
            {
                Console.WriteLine("Opção Relatorios selecionada.");
            }
            else if (opcao_principal == 0)
            {
                Console.WriteLine("Saindo do sistema. Até logo!");
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                MenuPrincipal(); 
            }
        }
    }
}