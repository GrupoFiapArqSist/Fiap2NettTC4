# Comanda Pro
Plataforma de controle de comandas de restaurante e gerenciamento de produtos, desenvolvida em .NET 8 com arquitetura de microsserviços e arquitetura limpa.

### 📋 Pré-requisitos

* .NET 8 ([Download .NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0))
* Docker ([Download Docker Desktop](https://www.docker.com/products/docker-desktop/))

## Integrantes

- [João Gasparini](https://github.com/joaogasparini)
- [Victoria Pacheco](https://github.com/vickypacheco)
- [Rafael Araujo](https://github.com/RafAraujo)
- [Cristian Kulessa](https://github.com/Kulessa)

## Build 

Para rodar este projeto, siga estes passos:

* Inicie os microsserviços conforme abaixo:
  * Crie o banco de dados no Docker utilizando o comando abaixo.
  * Execute as migrações para criar o banco de dados de cada microsserviço.
  * Execute o serviço correspondente a cada microsserviço.
  * Utilize o Gateway para acessar os endpoints dos microsserviços.

### Microsserviços e Banco de Dados

Os microsserviços foram desenvolvidos utilizando a tecnologia .NET 8 e integram-se a um banco de dados SQL Server. Abaixo estão os passos para configurar o banco de dados utilizando Docker:

```docker
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1q2w3e4r@#$' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

### Gateway

O Gateway foi implementado utilizando Ocelot para permitir o acesso aos endpoints dos microsserviços.

## Comunicação entre Microsserviços

Os serviços também se comunicam através dos endpoints e do gateway para acessar informações de outros microsserviços. Isso permite realizar validações e obter dados necessários para aplicar a lógica de negócio de forma distribuída, ele pode fazer uma requisição HTTP para o endpoint correspondente no microsserviço alvo, utilizando o gateway como intermediário.

* Detalhes dos Microserviços:

## Autenticação e controle de usuários 

Microsserviço responsável pelo gerenciamento de usuários e autenticação.

Endpoints
![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/56886ded-f8d2-4aea-84a9-9128b107ff0a)

## Command 

Microsserviço responsável pelo gerenciamento de comandas.

Endpoints

![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/4e4a159c-71ed-47e9-afea-93b4c11eac4b)

## Category

Microsserviço responsável pelo gerenciamento de categorias dos produtos.

Endpoints

![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/fd1a86df-f5e8-46f8-b5a7-db281d6ef9bb)

## Product

Microsserviço responsável pelo gerenciamento dos produtos do restaurante.

Endpoints

![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/3e7abf77-52ad-4abb-a927-a28780eb9ef3)

## Order

Microsserviço responsável pelo gerenciamento de pedidos e vinculo na comanda.

Endpoints

![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/db7beeec-0a50-4338-b9f9-4364033cdc04)

## UI

#### FALTA AQUI

### Qualidade de software

Foi desenvolvido testes unitários e de integração nos principais casos de uso(TDD). 
Utilizando tecnologia xUnit e Moq para simular comportamentos de dependências externas, serviços, repositórios, ou qualquer outra interface.

## Testes Unitários

![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/8d7833c3-d192-43a6-907d-98ac647e8243)
![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/d4929632-794b-4878-a865-6d7a55601fe6)
![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/fad7919b-0dcb-49dd-bc79-171f0c5162f7)

## Testes de Integração
![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/c04ff67e-d79b-4038-a0c6-aaca70a4bb37)
![image](https://github.com/GrupoFiapArqSist/Fiap2NettTC4/assets/60990141/ed5c5df2-a5ae-4c2a-b40d-22ef37c0f7ea)





