# Feedback do Instrutor

#### 23/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Demonstrou conhecimento em Identity e JWT.
- Unificou corretamente o papel do Autor com o User (identity)
- Mostrou entendimento do ecossistema de desenvolvimento em .NET

## Pontos Negativos:

- Duplicação de código é uma das piores falhas em qualquer tipo de projeto, esta arquitetura está carente de uma camada extra (core) para concentrar codigo comum entra os dois projetos.
- Pegar o usuário logado é muito mais simples do que o meio utilizado nesse projeto, poderia encapsular isto numa controle base ao invés de usar o código em varios lugares
- Não encontrei validação se um user pode alterar um post caso ele seja o dono ou admin.

## Sugestões:

- Criar uma camada Core e retirar toda duplicação de código (negocios, dados, etc) dos projetos.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
