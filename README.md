# Desafio da Mttechne
Desafio proposto pela Mttechne

<br/>

### DESCRITIVO DA SOLUÇÃO
Um comerciante precisa controlar o seu fluxo de caixa diário com os lançamentos
(débitos e créditos), também precisa de um relatório que disponibilize o saldo
diário consolidado.

<br/>

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

![Alt text](/Diagrams/usecase.png "Casos de Uso")
![Alt text](/Diagrams/activity.png "Casos de Uso")
![Alt text](/Diagrams/classes.png "Casos de Uso")


<br/>

### Build do Docker: rodar na Pasta do Projeto
docker build . -f DockerfileInterface --tag desafiowebimage:latest

docker build . -f Dockerfile --tag desafioimage:latest

Para rodar a API

docker run -it --rm -p 8080:5000 -e ASPNETCORE_URLS=http://+:5000 --name desafioapp desafioimage

acessar http://localhost:8080/swagger

Para rodar a Interface Web (pode ser rodada sem a API)

docker run -it --rm -p 8090:5020 -e ASPNETCORE_URLS=http://+:5020 --name desafiowebapp desafiowebimage

acessar http://localhost:8090/