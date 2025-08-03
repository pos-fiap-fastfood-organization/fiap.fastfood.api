# 🍔 FIAP Postech Fast Food

Projeto desenvolvido para o Tech Challenge da FIAP Pós-Tech, com foco na construção de um sistema de autoatendimento de fast food.

---

## 📌 Sobre o projeto

Este sistema tem como objetivo gerenciar o processo de pedidos em uma lanchonete que opera por meio de totens de autoatendimento (*kiosks*), oferecendo:

- Cadastro e identificação de clientes
- Visualização e montagem de pedidos via menu digital
- Integração com gateway de pagamento (Mercado Pago - QR Code)
- Acompanhamento do pedido até a retirada

---

## 📄 Documentação

- [Diagramas - Event Storming | Story Telling | Bounded Context](https://drive.google.com/drive/folders/1xNRAZfIqpomhRkz2gcdYtUZLtypkbk99)
- [Evidências de Desenvolvimento ](https://drive.google.com/drive/folders/1ptX92zr9ImXOPE8CUBSneuTZCRWBGyF4)
- [Documentos](https://drive.google.com/drive/folders/1EetNjhhsiNHsdST1Y8xH1hMnI4fPpDNv)


## ⚙️ Tecnologias utilizadas

- [.NET 8.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- **MongoDB** (utilizado como banco principal para todos os módulos)
- **Docker** e **Docker Compose**
- **Swagger** para documentação da API
- **mongo-express** para visualização e consulta da base de dados

---

## 🚀 Como executar o projeto - Ambiente Docker | docker-compose

### Pré-requisitos

- Docker instalado

### Passos

1. Abrir terminal na raiz do projeto

2. Subir o ambiente completo (API + MongoDB + mongo-express):

   Comando:

   ```bash
	docker-compose up --build -d
   ```

   Isso irá:  
   - Subir todos os containers  
   - Executar automaticamente o seed de dados via `db/init/init.js`
   - Inicializar o Swagger na API  
   - Disponibilizar o banco de dados com nome `fiap_fastfood`

3. Encerrar os serviços:

   Comando:
   ```bash
   docker-compose down -v
   ```

## 🧪 Acessos úteis | docker-compose

- Swagger UI: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)
- Mongo Express: [http://localhost:8081](http://localhost:8081)
  - Banco: `fiap_fastfood`

---

## 🚀 Como executar o projeto - Ambiente Docker + kubernates 

### Pré-requisitos

- Docker instalado
- Habilitar o kubernetes no Docker Desktop

### Passos

1. Abrir terminal na raiz do projeto

2. navegar até a pasta k8s

2. Na pasta, subir o ambiente completo no cluster (API + mongo):

   Comando:

   ```bash
	kubectl apply -f .
   ```

   Isso irá:  
   - Criar as secrets necessárias para a aplicação API 
   - Criar as secrets necessárias para o banco de dados MongoDB
   - Criar os configmaps necessários para a aplicação API
   - Criar os configmaps necessários para o banco de dados MongoDB contendo o seed de dados.
   - Criar os deployments para a aplicação API e o banco de dados MongoDB
   - Criar os services para a aplicação API e o banco de dados MongoDB serem expostos externamente
   - Criar os HPA (Horizontal Pod Autoscaler) para a aplicação API 

3. Encerrar os resources:

   Comando:
   ```bash
   kubectl delete all --all
   ```

## 🧪 Acessos úteis | kubernetes

- Swagger UI: [http://localhost:30007/swagger/index.html](http://localhost:30007/swagger/index.html)
- Mongo Compass : mongodb://fastfood_user:Fastfood2025@localhost:30017/fiap_fastfood
  - Banco: `fiap_fastfood`


---

## 📂 Estrutura de Pastas (Arquitetura Limpa)

src/  
├── Drivers/  
│       └── Api/  
│       └── Infrastructure/  
├── Core/  
├── Adapters/  
db/

---

## 🔗 Principais Endpoints

| Método | Rota                          | Descrição                                    |
|--------|-------------------------------|----------------------------------------------|
| GET    | /Menu                         | Obtém os itens disponíveis no cardápio       |
| POST   | /Menu                         | Criação de um novo menu                      |
| GET    | /Menu/[id]                    | Consultar item de menu pelo ID               |
| PUT    | /Menu/[id]                    | Atualizar item de menu pelo ID               |
| DELETE | /Menu/[id]                    | deletar item de menu pelo ID                 |
| POST   | /SelfService/customer         | Cadastra um novo cliente                     |
| GET    | /SelfService/customer/[cpf]   | Busca cliente por CPF                        |
| GET    | /Order                        | Consultar pedido paginado                    |
| GET    | /Order/pending				 | Consultar todos os pedidos pendentes			|
| POST   | /Order                        | Criar pedido                                 |
| GET    | /Order/[id]                   | Consultar pedido por ID                      |
| GET    | /Order/[id]/paymentstatus     | Consultar status do pedido por ID            |
| DELETE | /Order/[id]                   | Deletar pedido por ID                        |
| PATCH  | /Order/[id]                   | Atualizar itens do pedido por ID             |
| POST   | /Order/[id]/checkout          | obtém dados para pagamento                   |
| GET    | /payment/webhook              | Processar status do pagamento				|


ℹ️ Para mais detalhes, acesse o Swagger
	[Swagger-local](http://localhost:5291/swagger/index.html).
	[Swagger-docker-compose](http://localhost:8080/swagger/index.html).
	[Swagger-kubernetes](http://localhost:30007/swagger/index.html).

---

## 🧠 Arquitetura

A aplicação segue princípios de Arquitetura limpa, separando claramente:

Adapter
	- Controller para orquestração dos casos de uso e adaptar a entrada e saída de dados
	- Gateways para comunicação com serviços externos (ex: Mercado Pago | MongoDB)
	- Presenters para formatação da requisição e requição da API
Core
	- Casos de uso que implementam a lógica de negócio se comunicando com outros casos de uso e manipulando entidades
	- Entidades que representam os modelos de domínio
Infrastructure
	- Implementação das injeções de dependência (ex: MongoDB)

---

## 📥 Seed de dados

O script `db/init/init.js` popula o banco com os itens de menu iniciais automaticamente ao subir os containers.

---

## 🧑‍💻 Autores

Projeto desenvolvido como parte da Pós-Tech em Arquitetura de Software - FIAP por:
- Leandro Grando - [lfgrando](https://github.com/lfgrando)
- Victor Montenegro - [Victor-Montenegro](https://github.com/Victor-Montenegro)
- José Elias - [eliasjay](https://github.com/eliasjay)