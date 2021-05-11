# poc-graphql
Prova de conceito, utilizando asp net core com graphql e background service queue para logs com event hub

Nesta validação de conceito, temos uma aplicação em asp net core com o sistemas de rotas substituida pelo roteamento com graphql, onde devemos interceptar todas novas requisições, sejam consultas ou comandos e logar os dados de evento em uma fila em segundo plano, sem que a api seja afetada.

*TODOs*
- Eventos de logs resilientes
- Dispachador de eventos com batch async

Rode o migration e na sequencia rode a aplicação

para criar o banco de dados com efcore
- dotnet update 

para rodar a aplicação
- dotnet run
- acesse https://localhost:PORTA/graphql

https://github.com/ChilliCream/hotchocolate
