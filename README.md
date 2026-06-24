# MentoriaDDD 🎓

Projeto educacional que demonstra a aplicação de **padrões de arquitetura** e **boas práticas** em desenvolvimento .NET com **Domain-Driven Design (DDD)**.

## 📋 Sobre o Projeto

MentoriaDDD é uma API REST desenvolvida em **.NET 10** que implementa técnicas modernas de desenvolvimento, com foco em arquitetura limpa, separação de responsabilidades e padrões de design consolidados.

---

## 🛠️ Tecnologias & Técnicas Utilizadas

### **Framework & Linguagem**
- **.NET 10** - Framework moderno e de alto desempenho
- **C# 13** - Linguagem com recursos avançados e null-safety habilitado
- **ASP.NET Core** - Framework web escalável

### **Padrões de Arquitetura**
- **Domain-Driven Design (DDD)** - Organização por domínios de negócio
- **Repository Pattern** - Abstração de acesso a dados
- **Dependency Injection** - Injeção de dependências nativa do ASP.NET Core
- **SOLID Principles** - Princípios de design orientado a objetos

### **Persistência de Dados**
- **Entity Framework Core 10** - ORM para mapeamento objeto-relacional
- **SQLite** - Banco de dados relacional leve
- **Migrations** - Controle de versão de schema de banco de dados
- **DbContext** - Context para gerenciar entidades e estado

### **API REST**
- **Swagger/OpenAPI 10** - Documentação interativa de API
- **Swashbuckle.AspNetCore** - Geração automática de documentação
- **RESTful Design** - Endpoints seguindo padrões REST

### **Recursos de Produção**
- **Nullable Reference Types** - Type-safety rigoroso
- **Implicit Usings** - Redução de boilerplate
- **SonarLint** - Análise estática de código

---

## 📁 Estrutura do Projeto

```
MentoriaDDD/
├── Controllers/          # Camada de apresentação - Endpoints da API
│   └── ClientesController.cs
├── Services/             # Camada de aplicação - Lógica de negócio
│   ├── IClienteService.cs
│   ├── ClienteService.cs
│   └── Resultado.cs      # Wrapper para resultados com tratamento de erros
├── Repositories/         # Camada de acesso a dados
│   ├── Interfaces/
│   │   └── IClienteRepository.cs
│   └── ClienteRepository.cs
├── Models/               # Camada de domínio - Entidades
│   └── Cliente.cs
├── Dtos/                 # DTOs para requisições e respostas
│   ├── CriarClienteRequest.cs
│   ├── AtualizarClienteRequest.cs
│   └── ClienteResponse.cs
├── Data/                 # Contexto de banco de dados
│   └── AppDbContext.cs
├── Migrations/           # Histórico de alterações de schema
└── Program.cs            # Configuração da aplicação
```

---

## 🏗️ Padrões Implementados

### **1. Repository Pattern**
Abstrai a lógica de acesso a dados através de interfaces, permitindo fácil substituição de implementações e testes.

```csharp
public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodosAsync();
    Task<Cliente?> ObterPorIdAsync(int id);
}
```

### **2. Service Layer**
Encapsula a lógica de negócio, validações e transformações de dados entre Models e DTOs.

### **3. DTO (Data Transfer Object)**
Separação entre o modelo de domínio e os contratos de API, proporcionando flexibilidade e segurança.

### **4. Dependency Injection**
Configuração nativa do ASP.NET Core para gerenciar dependências:

```csharp
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
```

### **5. Resultado Pattern**
Padrão customizado para encapsular resultados com sucesso ou erro:

```csharp
public class Resultado<T>
{
    public bool EhSucesso { get; set; }
    public T? Dados { get; set; }
    public string? Mensagem { get; set; }
}
```

---

## 🚀 Como Executar

### **Pré-requisitos**
- .NET 10 SDK instalado
- Visual Studio 2026 ou superior (ou VS Code)
- Git

### **Passos**

1. **Clone o repositório**
   ```bash
   git clone https://github.com/niltonsilva001/MentoriaDDD.git
   cd MentoriaDDD
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Aplique as migrations**
   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse a documentação da API**
   - Swagger: `https://localhost:7089/swagger/index.html`

---

## 📚 Endpoints Principais

### **Clientes**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/clientes/ObterTodos` | Obtém todos os clientes |
| GET | `/api/clientes/ObterPorId/{id}` | Obtém um cliente por ID |
| GET | `/api/clientes/ObterPorCPF/{cpf}` | Obtém um cliente por CPF |
| POST | `/api/clientes/Criar` | Cria um novo cliente |
| PUT | `/api/clientes/Atualizar/{id}` | Atualiza um cliente |
| DELETE | `/api/clientes/Deletar/{id}` | Deleta um cliente |

---

## 🔍 Validações Implementadas

- ✅ Nome obrigatório e com comprimento validado
- ✅ Email em formato válido (RFC)
- ✅ CPF com validação de dígitos verificadores
- ✅ Verificação de duplicidade de CPF
- ✅ Tratamento de erros com mensagens descritivas

---

## 📦 Dependências

```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.8" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.9" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.9" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="10.2.3" />
```

---

## 💡 Conceitos Aplicados

### **SOLID Principles**
- **S** (Single Responsibility): Cada classe tem uma única responsabilidade
- **O** (Open/Closed): Aberto para extensão, fechado para modificação
- **L** (Liskov Substitution): Interfaces garantem contratação
- **I** (Interface Segregation): Interfaces específicas por domínio
- **D** (Dependency Inversion): Depende de abstrações, não de implementações

### **Clean Code**
- Nomes descritivos e em português (nomenclatura do domínio)
- Métodos pequenos e focados
- Tratamento apropriado de exceções
- Logging estruturado de operações

### **Async/Await**
- Operações assíncronas para melhor performance
- Não-bloqueio de threads
- Escalabilidade aumentada

---

## 🎯 Objetivos Educacionais

Este projeto foi desenvolvido com o propósito de:

1. ✅ Demonstrar a aplicação prática de **DDD** em C#
2. ✅ Ilustrar padrões de design **SOLID**
3. ✅ Mostrar **boas práticas** de arquitetura
4. ✅ Exemplo de **ASP.NET Core** moderno
5. ✅ Implementação de **Repository Pattern**
6. ✅ Uso de **Dependency Injection** nativo
7. ✅ **Entity Framework Core** com migrations
8. ✅ **API REST** bem estruturada

---

## 📝 Licença

Este projeto é de código aberto sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Se você tem sugestões, encontrou um bug ou quer melhorar algo:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas alterações (`git commit -am 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

---

## 📧 Contato & Suporte

Para dúvidas, sugestões ou reportar problemas, abra uma [Issue](https://github.com/niltonsilva001/MentoriaDDD/issues) no repositório.

---

**Desenvolvido com ❤️ para fins educacionais**

*Última atualização: 2025*
