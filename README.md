[![Solution Build and Test](https://github.com/alissonsolitto/teste-conhecimentos/actions/workflows/dotnet.yml/badge.svg)](https://github.com/alissonsolitto/teste-conhecimentos/actions/workflows/dotnet.yml)

# Arquitetura do Projeto

![Arquitetura do Projeto](https://github.com/alissonsolitto/teste-conhecimentos/blob/main/docs/arquitetura.jpg?raw=true"Arquitetura")

A arquitetura do projeto em composta por quatro aplicações:
- Api.One
- Api.Two
- Gateway.Ocelot
- Web.App

## Api.One
O microserviço Api.One é responsável por retornar a taxa de juros fixa de 1% atráves do endpoint **/taxajuros**

## Api.Two
O microserviço Api.Two é responsável por consulta a taxa de juros comunicando-se com o microserviço Api.One e calcular o montante final de acordo com o valor inicial e a quantidade de meses atráves do endpoint **/calcularjuros**
O segundo endpoint **/showmethecode** retorna a url deste diretório do github.

## Gateway.Ocelot
O Gateway é uma camada responsável por unificar o ponto de entrada na comunicação entre o Web.App e os microserviços.

## Web.App

![Web.App](https://github.com/alissonsolitto/teste-conhecimentos/blob/main/docs/web-app.png?raw=true"Arquitetura")

Aplicação criada em Asp.Net Core com o objetivo de implementar as funcionalidades dos microserviços criados.

# Arquitetura da Solução

![Arquitetura da Solução](https://github.com/alissonsolitto/teste-conhecimentos/blob/main/docs/estrutura-codigo-fonte.png?raw=true"Arquitetura")
## Api.One e Api.Two

Os microserviços **Api.One** e **Api.Two** possuem algumas caracteristicas em comum em suas implementações:
- Uso de swagger com versionamento
- Estrutura para versionamento de controladores e rotas
- Uso de FluentValidation
- Middlewares para geração de log e controle de exeções
- Modelo de retorno para padronizar a comunicação entre os serviços (WebApiResultModel)
- Todos os controladores utilizam a classe **ApiControllerBase** para customizar e padronizar o retorno dos endpoints

## Gateway.Ocelot

O Gateway utiliza a biblioteca para unificar a rota de acesso aos serviços. Todas as rotas são configuradas no arquivo **configuration.json**

## Web.App

Aplicação Web para implementar as funcionalidades dos microserviços criados, utiliza FluentValidation para validação dos dados de entrada da aplicação.
Essa aplicação comunica diretamente com o Gateway.Ocelot e suas configuração são definidas no appsettings.json.

## Domain.One e Domain.Two

Separação de serviços, modelos e validações utilizados nos microserviços

## Useful

Biblioteca para compartilhamento comum de implementações utilizadas nos microserviços:
- Controlador base
- Exceções
- Middlewares
- Result padrão
- Serviço para efetuar requisições HTTP

## Tests

![Testes OK](https://github.com/alissonsolitto/teste-conhecimentos/blob/main/docs/testes-ok.png?raw=true"Arquitetura")

Os projetos de testes foram criados utilizam XUnit e possuem testes de integração e de unidade de acordo com as aplicações criadas.
Todos os projetos agrupados em **Application** e **Domain** possuem testes de suas respectivas funcionalidades.

*Obs.: Para execução do **Api.Two.Tests** deve ser iniciado no ambiente o serviço Api.One.*

# Docker
Todos os projetos agrupados em **Application** possuem o arquivo Dockerfile para geração de suas respectivas imagens.
Na raiz da solução existe o arquivo **docker-compose.yml** que configura todos os containers para a execução. 

*Ao iniciar o ambiente verificar as configurações de endereçamento de cada aplicações no appsettings.json.*
