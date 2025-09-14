-- Banco de dados para o controle de estoque --

--/* Criando database DB_ControleEstoque */--

create database DB_ControleEstoque
GO

Use DB_ControleEstoque
GO

--/* Criando as tabelas de Clientes, Forncedores e Produtos*/--

-- Tabela contendo informações dos Fornecedores
CREATE TABLE Fornecedores
(
    id_fornecedor INT PRIMARY KEY IDENTITY(1,1),
    nome_fornecedor VARCHAR(100) NOT NULL,
    cnpj CHAR(14) UNIQUE NOT NULL,
    pais VARCHAR(50),
    endereco VARCHAR(150),
    telefone VARCHAR(15),
    categoria VARCHAR(50)
);


-- Tabela contendo informações dos Clientes
CREATE TABLE Clientes
(
    id_cliente INT PRIMARY KEY IDENTITY(1,1),
    nome_cliente VARCHAR(100) NOT NULL,
    cpf CHAR(11) UNIQUE NOT NULL,
    rg CHAR(9),
    telefone VARCHAR(15),
    cep CHAR(8)
)


-- Tabela contendo informações dos Produtos
CREATE TABLE Produtos
(
    id_produto INT PRIMARY KEY IDENTITY(1,1),
    nome_produto VARCHAR(100) NOT NULL,
    categoria VARCHAR(50),
    quantidade INT DEFAULT 0,
    valor_produto DECIMAL(10,2) NOT NULL,
    id_fornecedor INT,
    FOREIGN KEY (id_fornecedor) REFERENCES Fornecedores(id_fornecedor)
);

--/* Alteração para telefone VARCHAR(20)*/--
ALTER TABLE Fornecedores
ALTER COLUMN telefone VARCHAR(20);

ALTER TABLE Clientes
ALTER COLUMN telefone VARCHAR(20);


-- /* Inserindo dados ficticios nas tabelas */ --
INSERT INTO Fornecedores (nome_fornecedor, cnpj, pais, endereco, telefone, categoria)
VALUES
('Tech Supply LTDA', '12345678000199', 'Brasil', 'Av. Paulista, 1000 - São Paulo', '(11)99999-1111', 'Hardware'),
('Global Chips SA', '98765432000155', 'EUA', 'Silicon Valley, CA', '+1(408)555-1234', 'Componentes'),
('CodeParts BR', '11223344000177', 'Brasil', 'Rua das Inovações, 500 - Campinas', '(19)98888-2222', 'Peças'),
('DataWorld Imports', '55443322000166', 'China', 'Shenzhen, Guangdong', '+86 755 8888-9999', 'Periféricos'),
('NextGen Solutions', '99887766000122', 'Alemanha', 'Berlim Tech Park, 45', '+49 30 5555-7777', 'Software');


INSERT INTO Clientes (nome_cliente, cpf, rg, telefone, cep)
VALUES
('Lucas Almeida', '12345678901', '123456789', '(11) 98888-1111', '01311000'),
('Mariana Santos', '98765432100', '987654321', '(21) 97777-2222', '20040002'),
('João Ferreira', '11122233344', '111222333', '(31) 96666-3333', '30140071'),
('Carla Mendes', '55566677788', '555666777', '(41) 95555-4444', '80010020'),
('Felipe Rocha', '99988877766', '999888777', '(48) 94444-5555', '88015040');


