# Projeto Sistema de Controle de Estoque

Este projeto é um sistema de controle de estoque desenvolvido em C# (.NET 9), utilizando SQL Server como banco de dados. O sistema é executado em modo console e permite o gerenciamento de Produtos, Clientes e Fornecedores, com operações de cadastro, listagem, atualização e exclusão.

## Funcionalidades

- **Clientes**
  - Cadastrar novo cliente
  - Listar clientes cadastrados
  - Atualizar dados de um cliente
  - Deletar cliente

- **Produtos**
  - Cadastrar novo produto (com verificação de fornecedor existente)
  - Listar produtos cadastrados
  - Atualizar dados de um produto
  - Deletar produto

- **Fornecedores**
  - Cadastrar novo fornecedor
  - Listar fornecedores cadastrados
  - Atualizar dados de um fornecedor
  - Deletar fornecedor

- **Validações**
  - Verificação de existência de fornecedor ao cadastrar produto
  - Verificação de duplicidade de CPF (Clientes) e CNPJ (Fornecedores)
  - Mensagens de erro e sucesso para todas as operações

- **Navegação**
  - Menus interativos para cada módulo
  - Opções após cada operação: voltar ao menu, menu principal, sair ou repetir a operação

## Estrutura do Projeto
Projeto_Sistema_Controle_de_Estoque/ │ ├── Funcionalidades/ │   ├── Clientes/ │   │   └── Clientes.cs │   ├── Produtos/ │   │   └── Produtos.cs │   └── Fornecedores/ │    └── Fornecedores.cs │ ├── Program.cs └── README.md


## Banco de Dados

O sistema utiliza o SQL Server. As principais tabelas são:


> **Atenção:** Altere a string de conexão em cada classe para refletir o nome do seu servidor SQL, se necessário.

## Como Executar

1. **Clone o repositório:**

2. **Abra o projeto no Visual Studio 2022 ou superior.**

3. **Configure o banco de dados:**
- Crie o banco `DB_ControleEstoque` no SQL Server.
- Execute os scripts SQL acima para criar as tabelas.

4. **Ajuste a string de conexão** se necessário, nos arquivos `Clientes.cs`, `Produtos.cs` e `Fornecedores.cs`.

5. **Compile e execute o projeto.**

## Tecnologias Utilizadas

- C# 13.0 (.NET 9)
- SQL Server
- Visual Studio 2022

## Observações

- O sistema é totalmente em modo console.
- O código está organizado por funcionalidades, facilitando manutenção e expansão.
- O projeto pode ser expandido para interface gráfica (WinForms, WPF, Blazor) se desejado.

## Autor

Isaque Braz  
[GitHub](https://github.com/IsaqueBraz17)

---

Se precisar de mais informações ou encontrar algum problema, abra uma issue no repositório.
