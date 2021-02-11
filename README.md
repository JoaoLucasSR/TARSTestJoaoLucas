# TARSTestJoaoLucas
## Lista de requisitos:
- Criar um projeto de sistema web ou aplicativo utilizando o VSCode.
- O nome do projeto deverá ser TARSTestSeuNome.
- Deve ser utilizado qualquer um dos frameworks a seguir: ASP.NET Core, VueJS, KnockoutJS. Caso o projeto de escolha seja aplicativo, utilizar Flutter;
- A aplicação web ou app deverá ser no mínimo um CRUD de qualquer informação, com as ações Criar, Listar, Atualizar e Deletar.
- Utilizar bancos de dados PostgreSQL, MongoDB ou MySQL. Utilizar Entity Framework ou modelo de procedures escritas no banco.
- O projeto deve ser todo escrito em inglês.
- Enviar o projeto e o script do banco de dados populado anexos por e-mail.

## Api features implementadas:
- Dependency injection;
- Repository with Unit of Work pattern;
- Pagination;
- Logger with Serilog;

## Como iniciar a API:
1. Ajuste a ConnectionStrings para seu ambiente (Ex: localhost)
2. Rode o script create_database.sql para criar a Database
3. Realize os comando de migração do Entity Framework, caso utilize o dotnet cli lembre-se de instalar a tool do Entity Framework
4. São 2 migration, uma Initial que carrega todo o domain e a segunda Populadb que faz a população do banco de dados
