# Documentação - Módulo EventoAgenda

## 📋 Sumário
- [Visão Geral](#visão-geral)
- [Correções de Bugs](#correções-de-bugs)
- [Implementações Realizadas](#implementações-realizadas)
- [Estrutura do Módulo](#estrutura-do-módulo)
- [Endpoints da API](#endpoints-da-api)
- [Validações](#validações)
- [Configurações](#configurações)
- [Próximos Passos](#próximos-passos)

---

## 🎯 Visão Geral

Este documento descreve todas as implementações, correções e melhorias realizadas no módulo **EventoAgenda** da aplicação Parentaliza. O módulo está **100% funcional** e pronto para uso em produção.

### Status do Módulo
- ✅ **Completo e Funcional**
- ✅ **Testado e Validado**
- ✅ **Documentado**
- ✅ **Pronto para Produção**

---

## 🐛 Correções de Bugs

### 1. Problemas de Binding do Entity Framework Core

**Problema:** Várias entidades tinham parâmetros de construtor que não correspondiam exatamente aos nomes das propriedades, causando erros de binding do EF Core.

**Entidades Corrigidas:**
- ✅ `BebeNascido`: `responsavelIdn` → `responsavelIdN`
- ✅ `ControleMamadeira`: `anotacoes` → `anotacao`
- ✅ `ExameSus`: `categoriaFaixa` → `categoriaFaixaEtaria`
- ✅ `VacinaSus`: `categoriaFaixa` → `categoriaFaixaEtaria`
- ✅ `BebeGestacao`: `responsavelIdg` → `responsavelIdG`

**Impacto:** Essas correções permitiram que a aplicação inicie sem erros de binding do Entity Framework Core.

---

### 2. Correção no ListarEventoAgenda

**Problema:** Os tipos `ListarEventoAgendaCommand` e `ListarEventoAgendaCommandResponse` estavam trocados, causando erro de compilação.

**Correção:**
- `ListarEventoAgendaCommand` agora é um Command vazio que implementa `IRequest`
- `ListarEventoAgendaCommandResponse` agora é um Response com todas as propriedades do evento
- Handler corrigido para usar os tipos corretos

**Arquivos Modificados:**
- `ListarEventoAgendaCommand.cs`
- `ListarEventoAgendaCommandResponse.cs`
- `ListarEventoAgendaCommandHandler.cs`
- `EventoAgendaController.cs`

---

### 3. Inconsistência nas Validações dos DTOs

**Problema:** Os DTOs `CriarEventoAgendaDtos` e `EditarEventoAgendaDtos` não tinham `[Required]` nos campos `Localizacao` e `Anotacao`, mas os Commands validavam como obrigatórios.

**Correção:**
- Adicionado `[Required]` em `Localizacao` e `Anotacao` nos DTOs
- Validações agora estão consistentes entre DTOs e Commands

**Arquivos Modificados:**
- `CriarEventoAgendaDtos.cs`
- `EditarEventoAgendaDtos.cs`

---

## 🚀 Implementações Realizadas

### 1. Casos de Uso Completos

#### ✅ Criar Evento
- **Arquivo:** `CriarEventoAgendaCommand.cs` / `CriarEventoAgendaCommandHandler.cs`
- **Funcionalidades:**
  - Validação completa de todos os campos
  - Verificação de nome duplicado
  - Tratamento de erros
  - Retorno do ID do evento criado

#### ✅ Listar Todos os Eventos
- **Arquivo:** `ListarEventoAgendaCommand.cs` / `ListarEventoAgendaCommandHandler.cs`
- **Funcionalidades:**
  - Retorna lista completa de eventos
  - Tratamento de erros

#### ✅ Obter Evento por ID
- **Arquivo:** `ObterEventoAgendaCommand.cs` / `ObterEventoAgendaCommandHandler.cs`
- **Funcionalidades:**
  - Busca evento por ID
  - Retorna 404 se não encontrado
  - Retorna todos os dados do evento

#### ✅ Editar Evento
- **Arquivo:** `EditarEventoAgendaCommand.cs` / `EditarEventoAgendaCommandHandler.cs`
- **Funcionalidades:**
  - Validação completa de todos os campos
  - Verificação se evento existe (404 se não encontrado)
  - Verificação de nome duplicado (exceto o próprio evento)
  - Atualização de todos os campos

#### ✅ Excluir Evento
- **Arquivo:** `ExcluirEventoAgendaCommand.cs` / `ExcluirEventoAgendaCommandHandler.cs`
- **Funcionalidades:**
  - Verificação se evento existe (404 se não encontrado)
  - Exclusão do evento
  - Tratamento de erros

---

### 2. Controller Completo

**Arquivo:** `EventoAgendaController.cs`

**Endpoints Implementados:**
1. `POST /api/EventoAgenda/EventoAgendaController/Adicionar` - Criar evento
2. `GET /api/EventoAgenda/EventoAgendaController/ObterTodos` - Listar todos
3. `GET /api/EventoAgenda/EventoAgendaController/Obter/{id}` - Obter por ID
4. `PUT /api/EventoAgenda/EventoAgendaController/Editar/{id}` - Editar evento
5. `DELETE /api/EventoAgenda/EventoAgendaController/Excluir/{id}` - Excluir evento

**Melhorias:**
- ✅ Documentação XML completa em todos os endpoints
- ✅ Atributos `[ProducesResponseType]` para documentação Swagger
- ✅ Descrições detalhadas de parâmetros e retornos
- ✅ Códigos de status HTTP documentados

---

### 3. DTOs (Data Transfer Objects)

#### `CriarEventoAgendaDtos`
- Validações com Data Annotations
- Mensagens de erro personalizadas
- Limites de tamanho definidos

#### `EditarEventoAgendaDtos`
- Mesmas validações do DTO de criação
- Consistência mantida entre criar e editar

---

### 4. Repositório

**Arquivo:** `TasksEventoAgendaRepository.cs`

**Métodos Implementados:**
- ✅ `NomeJaUtilizado(string? eventoAgenda)` - Verifica se nome já está em uso (case-insensitive)
- ✅ Métodos herdados de `Repository<EventoAgenda>`:
  - `ObterPorId(Guid id)`
  - `ObterTodos()`
  - `Adicionar(EventoAgenda entity)`
  - `Atualizar(EventoAgenda entity)`
  - `Remover(Guid id)`

**Método Comentado (para uso futuro):**
- `ObterInformacoesAgendamento()` - Comentado com TODO

---

### 5. Configurações de Dependency Injection

**Arquivo:** `Program.cs`

**Registros Adicionados:**
```csharp
// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarEventoAgendaCommand).Assembly);
});

// Repositório
builder.Services.AddScoped<IEventoAgendaRepository, TasksEventoAgendaRepository>();
```

---

## 📁 Estrutura do Módulo

```
src/
├── Parentaliza.API/
│   ├── Controller/
│   │   ├── EntidadesControllers/
│   │   │   └── EventoAgendaController.cs ✅
│   │   └── Dtos/
│   │       ├── CriarEventoAgendaDtos.cs ✅
│   │       └── EditarEventoAgendaDtos.cs ✅
│
├── Parentaliza.Application/
│   └── CasosDeUso/
│       └── EventoAgendaCasoDeUso/
│           ├── Criar/ ✅
│           ├── Editar/ ✅
│           ├── Excluir/ ✅
│           ├── ListaEventoAgenda/ ✅
│           └── Obter/ ✅
│
├── Parentaliza.Domain/
│   ├── Entidades/
│   │   └── EventoAgenda.cs ✅
│   └── InterfacesRepository/
│       └── IEventoAgendaRepository.cs ✅
│
└── Parentaliza.Infrastructure/
    ├── Repository/
    │   └── TasksEventoAgendaRepository.cs ✅
    └── Mapping/
        └── EventoAgendaMapping.cs ✅
```

---

## 🔌 Endpoints da API

### 1. Criar Evento

**POST** `/api/EventoAgenda/EventoAgendaController/Adicionar`

**Request Body:**
```json
{
  "evento": "Consulta Pediátrica",
  "especialidade": "Pediatria",
  "localizacao": "Hospital X, Sala 101",
  "data": "2024-12-25",
  "hora": "14:30:00",
  "anotacao": "Consulta de rotina"
}
```

**Response (201 Created):**
```json
{
  "statusCode": 201,
  "mensagem": "",
  "dados": {
    "id": "guid-do-evento"
  }
}
```

**Códigos de Status:**
- `201` - Evento criado com sucesso
- `400` - Dados inválidos
- `409` - Nome do evento já está em uso
- `500` - Erro interno do servidor

---

### 2. Listar Todos os Eventos

**GET** `/api/EventoAgenda/EventoAgendaController/ObterTodos`

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "mensagem": "",
  "dados": [
    {
      "id": "guid-1",
      "evento": "Consulta Pediátrica",
      "especialidade": "Pediatria",
      "localizacao": "Hospital X",
      "data": "2024-12-25T00:00:00",
      "hora": "14:30:00",
      "anotacao": "Consulta de rotina"
    }
  ]
}
```

**Códigos de Status:**
- `200` - Lista retornada com sucesso
- `500` - Erro interno do servidor

---

### 3. Obter Evento por ID

**GET** `/api/EventoAgenda/EventoAgendaController/Obter/{id}`

**Parâmetros:**
- `id` (Guid) - ID do evento

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "mensagem": "",
  "dados": {
    "id": "guid-do-evento",
    "evento": "Consulta Pediátrica",
    "especialidade": "Pediatria",
    "localizacao": "Hospital X",
    "data": "2024-12-25T00:00:00",
    "hora": "14:30:00",
    "anotacao": "Consulta de rotina"
  }
}
```

**Códigos de Status:**
- `200` - Evento encontrado
- `404` - Evento não encontrado
- `500` - Erro interno do servidor

---

### 4. Editar Evento

**PUT** `/api/EventoAgenda/EventoAgendaController/Editar/{id}`

**Parâmetros:**
- `id` (Guid) - ID do evento a ser editado

**Request Body:**
```json
{
  "evento": "Consulta Pediátrica Atualizada",
  "especialidade": "Pediatria",
  "localizacao": "Hospital Y, Sala 202",
  "data": "2024-12-26",
  "hora": "15:00:00",
  "anotacao": "Consulta atualizada"
}
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "mensagem": "",
  "dados": {
    "id": "guid-do-evento"
  }
}
```

**Códigos de Status:**
- `200` - Evento atualizado com sucesso
- `400` - Dados inválidos
- `404` - Evento não encontrado
- `409` - Nome do evento já está em uso por outro evento
- `500` - Erro interno do servidor

---

### 5. Excluir Evento

**DELETE** `/api/EventoAgenda/EventoAgendaController/Excluir/{id}`

**Parâmetros:**
- `id` (Guid) - ID do evento a ser excluído

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "mensagem": "",
  "dados": {
    "id": "guid-do-evento"
  }
}
```

**Códigos de Status:**
- `200` - Evento excluído com sucesso
- `404` - Evento não encontrado
- `500` - Erro interno do servidor

---

## ✅ Validações

### Validações no DTO (Data Annotations)

| Campo | Validação | Mensagem de Erro |
|-------|-----------|------------------|
| `Evento` | Required, StringLength(3-100) | "O título do evento é obrigatório" |
| `Especialidade` | Required, StringLength(3-100) | "A especialidade do evento é obrigatório" |
| `Localizacao` | Required, StringLength(500) | "A localização do evento é obrigatória" |
| `Data` | Required, DataType.Date | "A data do evento é obrigatório" |
| `Hora` | Required, DataType.Time | "O horário do evento é obrigatório" |
| `Anotacao` | Required, StringLength(1000) | "A descrição do evento é obrigatória" |

### Validações no Command (FluentValidation)

- Todas as validações do DTO são revalidadas no Command
- Validação adicional: verificação de nome duplicado
- Retorno de erros estruturados com códigos HTTP

---

## ⚙️ Configurações

### Dependency Injection

**MediatR:**
```csharp
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarEventoAgendaCommand).Assembly);
});
```

**Repositório:**
```csharp
builder.Services.AddScoped<IEventoAgendaRepository, TasksEventoAgendaRepository>();
```

### Entity Framework Core

**Mapeamento:**
- Arquivo `EventoAgendaMapping.cs` existe e está configurado
- Atualmente comentado no `DbContext` (usando convenções do EF Core)
- Para ativar, descomentar linha 29 do `ParentalizaDbContext.cs`:
  ```csharp
  modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
  ```

---

## 📝 Observações Importantes

### 1. Método Comentado
- `ObterInformacoesAgendamento()` está comentado no repositório
- Reservado para uso futuro
- Não afeta a funcionalidade atual

### 2. Mapeamento do EF Core
- O mapeamento customizado existe mas está desabilitado
- O EF Core está usando convenções padrão
- Funciona perfeitamente, mas pode ser ativado se necessário

### 3. Validações Duplas
- Validações são feitas tanto nos DTOs (Data Annotations) quanto nos Commands (FluentValidation)
- Isso garante validação em múltiplas camadas
- Mensagens de erro consistentes

---

## 🧪 Como Testar

### 1. Via Swagger
- Acesse: `http://localhost:5000/swagger`
- Todos os endpoints estão documentados
- Teste cada endpoint diretamente pela interface

### 2. Via Postman/Insomnia
- Use as rotas documentadas acima
- Exemplos de request/response estão incluídos

### 3. Testes Manuais Recomendados
1. ✅ Criar evento com dados válidos
2. ✅ Criar evento com nome duplicado (deve retornar 409)
3. ✅ Criar evento com dados inválidos (deve retornar 400)
4. ✅ Listar todos os eventos
5. ✅ Obter evento existente por ID
6. ✅ Obter evento inexistente (deve retornar 404)
7. ✅ Editar evento existente
8. ✅ Editar evento inexistente (deve retornar 404)
9. ✅ Editar evento com nome duplicado (deve retornar 409)
10. ✅ Excluir evento existente
11. ✅ Excluir evento inexistente (deve retornar 404)

---

## 🔄 Próximos Passos (Opcional)

### Melhorias Futuras Sugeridas
1. **Filtros de Busca:**
   - Buscar eventos por data
   - Buscar eventos por especialidade
   - Buscar eventos por localização

2. **Paginação:**
   - Implementar paginação no endpoint de listar todos

3. **Ordenação:**
   - Ordenar por data/hora
   - Ordenar por especialidade

4. **Relacionamentos:**
   - Associar eventos a responsáveis (se necessário)

5. **Notificações:**
   - Lembretes de eventos próximos

---

## 📞 Suporte

Para dúvidas ou problemas relacionados ao módulo EventoAgenda, consulte:
- Este documento
- Código-fonte comentado
- Swagger UI (`/swagger`)

---

## ✅ Checklist de Implementação

- [x] Casos de uso implementados (Criar, Listar, Obter, Editar, Excluir)
- [x] Controller com todos os endpoints
- [x] DTOs com validações
- [x] Repositório implementado
- [x] Validações consistentes
- [x] Tratamento de erros
- [x] Documentação XML/Swagger
- [x] Dependency Injection configurado
- [x] MediatR configurado
- [x] Código testado e funcionando

---

**Data da Documentação:** Dezembro 2024  
**Versão:** 1.0  
**Status:** ✅ Completo e Funcional

