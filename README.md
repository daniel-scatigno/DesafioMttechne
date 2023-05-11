# Desafio da Mttechne

### DESCRITIVO DA SOLUÇÃO
Um comerciante precisa controlar o seu fluxo de caixa diário com os lançamentos
(débitos e créditos), também precisa de um relatório que disponibilize o saldo
diário consolidado.

### REQUISITOS DE NEGÓCIO:
- Serviço que faça o controle de lançamentos;
- Serviço do consolidado diário.

### REQUISITOS TÉCNICOS:
- Desenho da solução, e explicação não técnica do funcionamento da arquitetura;
- Pode ser feito na linguagem que você domina;
- Boas práticas são bem-vindas (Design Patterns, Padrões de Arquitetura,
SOLID e etc);
- Readme com instruções de como subir a aplicação local, container e utilização
dos serviços;
- Hospedar em repositório público (GitHub).

<br/>
<br/>

# SOLUÇÃO PROPOSTA
Uma aplicação Web/Mobile onde o usuário comerciante possa acessar e informar os débitos e créditos

## Arquitetura baseada em Domain Driven Design
![Alt text](/Diagrams/arquitecture.png "Arquitetura")

1) Um Cliente (podendo ser uma aplicação Web, Mobile ou outro consumidor via RESTfull APIs)
se comunicam com a camada de serviços, trafegando entre eles objetos(ViewModels)
2) A camada de serviços se encarrega de transformar os objetos recebidos, em consultas, inserção, exclusão e alteração de dados
3) A camada de serviços utiliza uma camada de infraestrutura utilizando um repositório genérico (Uso de Interfaces)
permitindo a criação de outros mecanismos de repositórios futuros (escalabilidade) caso haja a necessidade do uso 
de diferentes banco de dados / frameworks de persistência
4) Este tipo de desenho permite o <b>crescimento da aplicação</b>, possibilitanto o <b>uso de diferentes frameworks e armazenagem de dados</b>


## Casos de Uso
![Alt text](/Diagrams/usecase.png "Casos de Uso")

## Diagrama de Atividade
![Alt text](/Diagrams/activity.png "Diagram de Atividade")

## Diagrama de Classes (Entidades)
![Alt text](/Diagrams/classes.png "Diagrama de Classes")


## Sobre o  que foi desenvolvido como solução
- Duas aplicações são entregues:
   1) a Desafio.API expondo os serviços através das APIs (pode ser visualizada através da url http://localhost:8080/swagger)
   2) a Desafio.Web sendo uma interface Web simples construida em Blazor Server

<br/>

## Requisitos para rodar a aplicação
- Sistema operacional (qualquer) com Docker instalado
- Ou Qualquer Sistema Operacional com dotnet SDK 7 instalado

### Build do Docker
- Rodar este comando na pasta raiz do Projeto
```
docker build . -f DockerfileInterface --tag desafiowebimage:latest
docker build . -f Dockerfile --tag desafioimage:latest
```

- Para rodar a Interface Web (pode ser rodada sem a API) http://localhost:8090/
```
docker run -it --rm -p 8090:5020 -e ASPNETCORE_URLS=http://+:5020 --name desafiowebapp desafiowebimage
```

- Para rodar a API http://localhost:8080/swagger
```
docker run -it --rm -p 8080:5000 -e ASPNETCORE_URLS=http://+:5000 --name desafioapp desafioimage
```


# Melhorias Futuras / Considerações
- Foi utilizado o <b>Sqlite</b> por ser mais simples de implementar em um projeto demonstrativo, mas neste Escopo o ideal seria utilizar um banco de dados relacional como PostgreSql ou Sql Server
- Foi adicionado uma Autenticação do tipo Jwt Bearer como exemplo, porém não foi implementada a Autorização (usando um usuário fixo). Pode-se implementar a autorização, exigindo um Papel (Roles) ou uma Politica / Claim especifica, permitindo personalizar
as ações que o usuário pode realizar dentro do sistema
- Foi utilizado o FluentValidation framework para realizar as validações, as APIs automaticamente retornam os erros de validação quando houver
- Não foi feito o tratamento de erros na camada de Serviços, mas poderia ser feito retornando um objeto com as informações dos erros de validação