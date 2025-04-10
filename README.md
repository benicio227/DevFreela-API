# Sobre o projeto
 
O **DevFreela** é uma API de gerenciamento de projetos para freelancers. O sistema permite que clientes criem, administrem e atribuam projetos a freelancers, enquanto os freelancers podem visualizar, comentar e acompanhar o progresso dos projetos. A API ainda facilita o controle de custos, status de projetos e a comunicação entre as partes envolvidas. 

## Estrutura do Projeto

O projeto é composto por uma **Solution** chamada **DevFreela** e contém os seguintes componentes principais:

### Diretórios e Arquivos

- **DevFreela.API**: Contém os controladores e a lógica da API.
  
  - **Controllers**: Responsável pelos controladores de rotas da API.
    - **ProjectsController**: Controla as operações relacionadas aos projetos (CRUD, comentários, iniciar e completar projetos).
    - **SkillsController**: Controla as operações relacionadas às habilidades dos usuários.
    - **UsersController**: Controla as operações relacionadas aos usuários, como upload de foto de perfil.
  
  - **ExceptionHandler**: Contém a classe **ApiExceptionHandler** que gerencia erros e exceções.
    
  - **Models**: Contém os modelos de dados utilizados nas requisições e respostas da API.
    - **CreateProjectInputModel**: Define o modelo para criação de um novo projeto.
    - **CreateProjectCommentInputModel**: Define o modelo para criação de um comentário de projeto.
    - **FreelanceTotalCostConfig**: Define os valores mínimos e máximos de custo de um projeto.
    - **UpdateProjectInputModel**: Define o modelo para atualizar um projeto.
      
  - **Services**: Contém a interface **IConfigService**, que é responsável pela configuração de custos de freelancer.

## Funcionalidades

- **Projetos**: Criar, listar, atualizar, excluir projetos e adicionar comentários.
- **Habilidades**: Adicionar e listar habilidades.
- **Usuários**: Criar usuários e fazer upload de fotos de perfil.

## Endpoints

### Projetos

- **GET** `/api/projects?search={search}`: Retorna a lista de projetos. O parâmetro **search** permite filtrar os projetos por palavras-chave.
- **GET** `/api/projects/{id}`: Retorna os detalhes de um projeto específico.
- **POST** `/api/projects`: Cria um novo projeto. Verifica se o custo total está dentro dos limites configurados.
- **PUT** `/api/projects/{id}`: Atualiza as informações de um projeto.
- **DELETE** `/api/projects/{id}`: Exclui um projeto.
- **PUT** `/api/projects/{id}/start`: Inicia um projeto.
- **PUT** `/api/projects/{id}/complete`: Completa um projeto.
- **POST** `/api/projects/{id}/comments`: Adiciona um comentário a um projeto.

### Habilidades

- **GET** `/api/skills`: Retorna a lista de habilidades disponíveis.
- **POST** `/api/skills`: Adiciona uma nova habilidade.

### Usuários

- **POST** `/api/users`: Cria um novo usuário.
- **PUT** `/api/users/{id}/profile-picture`: Permite que o usuário faça o upload de uma foto de perfil.

## Como Executar o Projeto

### Pré-requisitos

- **.NET 6.0** ou superior
- **SQL Server** (ou outro banco de dados configurado)

### Passos

1. Clone o repositório:
    ```bash
    git clone git@github.com:benicio227/DevFreela-API.git
    ```

2. Navegue até o diretório do projeto:
    ```bash
    cd DevFreela/DevFreela.API
    ```

3. Restaure as dependências:
    ```bash
    dotnet restore
    ```

4. Execute a aplicação:
    ```bash
    dotnet run
    ```

A API estará disponível em `https://localhost:7193`.

## Contribuições

Para contribuir com o projeto DevFreela, siga os seguintes passos abaixo:

1. Faça um fork do projeto.
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`).
3. Commit suas mudanças (`git commit -m 'Adicionando nova feature'`).
4. Push para a branch (`git push origin feature/nova-feature`).
5. Abra um Pull Request.

### Swagger Interface

![Swagger Screenshot](https://github.com/benicio227/DevFreela-API/blob/master/Imagem-DevFreela.png?raw=true)

A imagem acima mostra a interface do Swagger para testar os endpoints da API.

## Licença

Este projeto está licenciado sob a **MIT License**.
