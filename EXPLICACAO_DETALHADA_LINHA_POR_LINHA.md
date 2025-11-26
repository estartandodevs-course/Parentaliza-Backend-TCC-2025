# 📖 Explicação Detalhada - Linha por Linha da Migration

## 🎯 Introdução

Este documento explica **CADA LINHA** do código da migration `ParentalizaDbContext.cs`, detalhando o que cada comando faz, por que existe, e como funciona.

---

## 📋 PARTE 1: Cabeçalho e Namespace

```csharp
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentaliza.Infrastructure.Migrations
```

### **Explicação linha por linha:**

#### **Linha 1: `using System;`**
- **O que faz:** Importa o namespace `System` do .NET
- **Por que precisa:** Contém tipos básicos como `DateTime`, `Guid`, `String`
- **Exemplo de uso:** `DateTime.Now`, `Guid.NewGuid()`

#### **Linha 2: `using Microsoft.EntityFrameworkCore.Migrations;`**
- **O que faz:** Importa classes do Entity Framework Core para migrations
- **Por que precisa:** Contém `Migration`, `MigrationBuilder` que usamos no código
- **Classes principais:**
  - `Migration` = Classe base que herdamos
  - `MigrationBuilder` = Ferramenta para criar tabelas, colunas, etc.

#### **Linha 3: `#nullable disable`**
- **O que faz:** Desabilita avisos de nullable reference types
- **Por que precisa:** Evita warnings sobre valores que podem ser null
- **Contexto:** C# 8+ tem verificação de null, mas migrations antigas não usam

#### **Linha 4: `namespace Parentaliza.Infrastructure.Migrations`**
- **O que faz:** Define o namespace (organização) do arquivo
- **Por que precisa:** Agrupa classes relacionadas
- **Estrutura:** `Projeto.Camada.Pasta`

---

## 📋 PARTE 2: Declaração da Classe

```csharp
/// <inheritdoc />
public partial class ParentalizaDbContext : Migration
{
```

### **Explicação linha por linha:**

#### **Linha 1: `/// <inheritdoc />`**
- **O que faz:** Comentário XML que indica herdar documentação da classe pai
- **Por que precisa:** Mantém documentação consistente
- **Resultado:** Gera documentação automática

#### **Linha 2: `public partial class ParentalizaDbContext : Migration`**
- **`public`:** A classe pode ser acessada de qualquer lugar
- **`partial`:** A classe pode ser dividida em múltiplos arquivos
  - **Por que:** EF Core gera outro arquivo `.Designer.cs` com metadados
- **`class ParentalizaDbContext`:** Nome da classe
- **`: Migration`:** Herda da classe `Migration` do EF Core
  - **O que ganha:** Métodos `Up()` e `Down()` obrigatórios
  - **Funcionalidade:** Pode ser aplicada/revertida pelo EF Core

---

## 📋 PARTE 3: Método Up() - Configuração do Banco

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AlterDatabase()
        .Annotation("MySql:CharSet", "utf8mb4");
```

### **Explicação linha por linha:**

#### **Linha 1: `protected override void Up(MigrationBuilder migrationBuilder)`**
- **`protected`:** Só pode ser acessado pela classe ou classes filhas
- **`override`:** Sobrescreve o método da classe pai (`Migration`)
- **`void`:** Não retorna valor
- **`Up`:** Nome do método (executado ao aplicar migration)
- **`MigrationBuilder migrationBuilder`:** Objeto que permite criar/modificar banco
  - **O que é:** Ferramenta do EF Core para construir SQL
  - **Métodos disponíveis:** `CreateTable()`, `AlterTable()`, `Sql()`, etc.

#### **Linha 2: `migrationBuilder.AlterDatabase()`**
- **O que faz:** Altera configurações do banco de dados inteiro
- **Quando usar:** Para mudanças globais (charset, collation, etc.)
- **Retorna:** Objeto que permite adicionar anotações

#### **Linha 3: `.Annotation("MySql:CharSet", "utf8mb4")`**
- **O que faz:** Adiciona uma anotação específica do MySQL
- **`"MySql:CharSet"`:** Chave da anotação (diz ao MySQL qual charset usar)
- **`"utf8mb4"`:** Valor = charset UTF-8 completo
  - **UTF-8:** Suporta todos os caracteres Unicode
  - **utf8mb4:** Versão completa (suporta emojis, caracteres especiais)
  - **Diferença:** `utf8` antigo não suporta emojis, `utf8mb4` sim
- **Resultado SQL:**
  ```sql
  ALTER DATABASE nome_do_banco CHARACTER SET utf8mb4;
  ```

---

## 📋 PARTE 4: Criando a Tabela Conteudos

```csharp
migrationBuilder.CreateTable(
    name: "Conteudos",
    columns: table => new
    {
        Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
        Titulo = table.Column<string>(type: "varchar(80)", nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        Categoria = table.Column<string>(type: "varchar(80)", nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        DataPublicacao = table.Column<DateTime>(type: "datetime", nullable: false),
        Descricao = table.Column<string>(type: "varchar(1000)", nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
        UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Conteudos", x => x.Id);
    })
    .Annotation("MySql:CharSet", "utf8mb4");
```

### **Explicação linha por linha:**

#### **Linha 1: `migrationBuilder.CreateTable(`**
- **O que faz:** Inicia criação de uma nova tabela
- **Parâmetros:** Nome da tabela, colunas, constraints
- **Resultado:** Gera comando `CREATE TABLE`

#### **Linha 2: `name: "Conteudos",`**
- **O que faz:** Define o nome da tabela no banco
- **`name:`:** Nome do parâmetro (torna código mais legível)
- **`"Conteudos"`:** Nome exato que aparecerá no MySQL
- **Convenção:** Plural em português

#### **Linha 3: `columns: table => new`**
- **O que faz:** Define as colunas da tabela usando lambda expression
- **`columns:`:** Nome do parâmetro
- **`table =>`:** Lambda que recebe objeto `table` (MigrationBuilder)
- **`new { }`:** Cria objeto anônimo com as colunas

#### **Linha 4: `Id = table.Column<Guid>(...)`**
- **`Id`:** Nome da coluna
- **`table.Column<Guid>`:** Cria coluna do tipo `Guid` (GUID = identificador único)
- **`<Guid>`:** Tipo genérico (tipo C# que será convertido para SQL)
- **Por que Guid?:** Identificador único global (não se repete nunca)

#### **Linha 4 (continuação): `type: "char(36)",`**
- **O que faz:** Define tipo SQL no MySQL
- **`type:`:** Nome do parâmetro
- **`"char(36)"`:** Tipo CHAR com 36 caracteres
  - **Por que 36?:** GUID formatado tem exatamente 36 caracteres
  - **Exemplo:** `"11111111-1111-1111-1111-111111111111"` = 36 chars
- **CHAR vs VARCHAR:** CHAR é fixo (sempre 36), VARCHAR é variável

#### **Linha 4 (continuação): `nullable: false,`**
- **O que faz:** Define se a coluna pode ser NULL
- **`nullable:`:** Nome do parâmetro
- **`false`:** NÃO pode ser NULL (obrigatório)
- **Por que obrigatório?:** ID sempre deve existir (chave primária)

#### **Linha 4 (continuação): `collation: "ascii_general_ci"`**
- **O que faz:** Define como ordenar/comparar caracteres
- **`collation:`:** Nome do parâmetro
- **`"ascii_general_ci"`:** Colação ASCII, case-insensitive
  - **ASCII:** Apenas caracteres básicos (0-9, A-Z, hífen)
  - **general_ci:** Case-insensitive (A = a)
- **Por que ASCII?:** GUIDs só usam caracteres ASCII (0-9, A-F, hífen)

#### **Linha 5: `Titulo = table.Column<string>(...)`**
- **`Titulo`:** Nome da coluna (título do conteúdo)
- **`table.Column<string>`:** Coluna do tipo string (texto)
- **`<string>`:** Tipo C# que será convertido para VARCHAR no MySQL

#### **Linha 5 (continuação): `type: "varchar(80)",`**
- **`"varchar(80)"`:** Tipo VARCHAR com máximo de 80 caracteres
- **VARCHAR:** String variável (usa só o espaço necessário)
- **80:** Limite máximo de caracteres
- **Por que limitar?:** Evita textos muito longos, economiza espaço

#### **Linha 5 (continuação): `nullable: false`**
- **O que faz:** Título é obrigatório
- **Por que:** Todo conteúdo precisa de título

#### **Linha 6: `.Annotation("MySql:CharSet", "utf8mb4")`**
- **O que faz:** Define charset UTF-8 para esta coluna
- **Por que:** Título pode ter acentos, emojis, caracteres especiais
- **Diferença:** Coluna Id usa ASCII (só GUID), esta usa UTF-8 (texto)

#### **Linha 7-8: `Categoria = ...`**
- **Mesma estrutura:** VARCHAR(80), obrigatório, UTF-8
- **Propósito:** Categoria do conteúdo (ex: "Saúde", "Alimentação")

#### **Linha 9: `DataPublicacao = table.Column<DateTime>(...)`**
- **`DataPublicacao`:** Nome da coluna
- **`table.Column<DateTime>`:** Coluna do tipo data/hora
- **`type: "datetime"`:** Tipo SQL DATETIME
  - **Formato:** `YYYY-MM-DD HH:MM:SS`
  - **Exemplo:** `2024-11-25 14:30:00`
- **`nullable: false`:** Data é obrigatória

#### **Linha 10-11: `Descricao = ...`**
- **`type: "varchar(1000)"`:** Até 1000 caracteres (maior que título)
- **Por que maior:** Descrição é mais longa que título
- **UTF-8:** Suporta acentos e caracteres especiais

#### **Linha 12: `CreatedAt = table.Column<DateTime>(...)`**
- **`type: "datetime(6)"`:** DATETIME com 6 dígitos de microsegundos
  - **Formato:** `YYYY-MM-DD HH:MM:SS.ffffff`
  - **Exemplo:** `2024-11-25 14:30:00.123456`
- **Por que microsegundos:** Precisão maior para auditoria
- **Obrigatório:** Sempre deve ter data de criação

#### **Linha 13: `UpdatedAt = table.Column<DateTime>(...)`**
- **`nullable: true`:** Pode ser NULL
- **Por que NULL?:** Quando cria, ainda não foi atualizado
- **Quando preenche:** Quando registro é modificado

#### **Linha 14: `},`**
- **O que faz:** Fecha o objeto de colunas
- **Fim da definição:** Todas as colunas foram definidas

#### **Linha 15: `constraints: table =>`**
- **O que faz:** Define restrições da tabela (chaves, índices, etc.)
- **`constraints:`:** Nome do parâmetro
- **`table =>`:** Lambda expression para definir constraints

#### **Linha 16: `table.PrimaryKey("PK_Conteudos", x => x.Id);`**
- **O que faz:** Define chave primária da tabela
- **`table.PrimaryKey()`:** Método para criar chave primária
- **`"PK_Conteudos"`:** Nome da constraint (identificador único)
  - **Convenção:** `PK_` + nome da tabela
- **`x => x.Id`:** Lambda que indica qual coluna é a chave
  - **`x`:** Representa a tabela
  - **`x.Id`:** A coluna Id será a chave primária
- **Resultado SQL:**
  ```sql
  ALTER TABLE Conteudos ADD CONSTRAINT PK_Conteudos PRIMARY KEY (Id);
  ```

#### **Linha 17: `})`**
- **O que faz:** Fecha o bloco de constraints
- **Fim das constraints:** Só tem chave primária nesta tabela

#### **Linha 18: `.Annotation("MySql:CharSet", "utf8mb4");`**
- **O que faz:** Define charset padrão da tabela
- **Aplica a:** Todas as colunas que não especificaram charset
- **Garante:** Tabela toda usa UTF-8

### **SQL Gerado (resultado final):**

```sql
CREATE TABLE Conteudos (
    Id CHAR(36) NOT NULL,
    Titulo VARCHAR(80) NOT NULL,
    Categoria VARCHAR(80) NOT NULL,
    DataPublicacao DATETIME NOT NULL,
    Descricao VARCHAR(1000) NOT NULL,
    CreatedAt DATETIME(6) NOT NULL,
    UpdatedAt DATETIME(6) NULL,
    CONSTRAINT PK_Conteudos PRIMARY KEY (Id)
) CHARACTER SET utf8mb4;
```

---

## 📋 PARTE 5: Criando Tabela com Relacionamento (Foreign Key)

```csharp
migrationBuilder.CreateTable(
    name: "BebeNascido",
    columns: table => new
    {
        Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
        ResponsavelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
        Nome = table.Column<string>(type: "varchar(80)", nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        // ... outras colunas
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_BebeNascido", x => x.Id);
        table.ForeignKey(
            name: "FK_BebeNascido_Responsaveis_ResponsavelId",
            column: x => x.ResponsavelId,
            principalTable: "Responsaveis",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    })
    .Annotation("MySql:CharSet", "utf8mb4");
```

### **Explicação das novas partes:**

#### **Linha: `ResponsavelId = table.Column<Guid>(...)`**
- **O que faz:** Cria coluna que armazena ID do responsável
- **Tipo:** GUID (mesmo tipo do Id do Responsavel)
- **Obrigatório:** Todo bebê deve ter um responsável
- **Propósito:** Liga BebeNascido a Responsavel

#### **Linha: `table.ForeignKey(`**
- **O que faz:** Cria relacionamento (Foreign Key) entre tabelas
- **Por que precisa:** Garante que ResponsavelId existe na tabela Responsaveis
- **Benefício:** Integridade referencial (não permite dados inválidos)

#### **Linha: `name: "FK_BebeNascido_Responsaveis_ResponsavelId",`**
- **O que faz:** Define nome da constraint
- **Convenção:** `FK_TabelaFilha_TabelaPai_Coluna`
- **Exemplo:** `FK_BebeNascido_Responsaveis_ResponsavelId`
- **Por que nomear:** Facilita identificar e remover depois

#### **Linha: `column: x => x.ResponsavelId,`**
- **O que faz:** Define qual coluna desta tabela é a Foreign Key
- **`x =>`:** Lambda que representa a tabela atual (BebeNascido)
- **`x.ResponsavelId`:** A coluna ResponsavelId será a Foreign Key

#### **Linha: `principalTable: "Responsaveis",`**
- **O que faz:** Define tabela referenciada (tabela pai)
- **`"Responsaveis"`:** Nome da tabela que contém o registro principal
- **Relacionamento:** BebeNascido → Responsaveis (muitos para um)

#### **Linha: `principalColumn: "Id",`**
- **O que faz:** Define qual coluna da tabela pai é referenciada
- **`"Id"`:** Coluna Id da tabela Responsaveis
- **Lógica:** ResponsavelId aponta para Responsaveis.Id

#### **Linha: `onDelete: ReferentialAction.Restrict);`**
- **O que faz:** Define o que acontece ao tentar deletar registro pai
- **`ReferentialAction.Restrict`:** **NÃO PERMITE** deletar se tiver filhos
- **Outras opções:**
  - **`Cascade`:** Deleta filhos automaticamente (perigoso!)
  - **`SetNull`:** Define Foreign Key como NULL (se permitir)
  - **`NoAction`:** Não faz nada (pode causar erro)
- **Por que Restrict:** Protege dados (não pode deletar responsável com bebês)

### **SQL Gerado:**

```sql
CREATE TABLE BebeNascido (
    Id CHAR(36) NOT NULL,
    ResponsavelId CHAR(36) NOT NULL,
    Nome VARCHAR(80) NOT NULL,
    -- ... outras colunas
    CONSTRAINT PK_BebeNascido PRIMARY KEY (Id),
    CONSTRAINT FK_BebeNascido_Responsaveis_ResponsavelId
        FOREIGN KEY (ResponsavelId) REFERENCES Responsaveis(Id)
        ON DELETE RESTRICT
) CHARACTER SET utf8mb4;
```

---

## 📋 PARTE 6: Criando Índices

```csharp
migrationBuilder.CreateIndex(
    name: "IX_BebeNascido_ResponsavelId",
    table: "BebeNascido",
    column: "ResponsavelId");
```

### **Explicação linha por linha:**

#### **Linha 1: `migrationBuilder.CreateIndex(`**
- **O que faz:** Cria um índice na tabela
- **Índice:** Estrutura que acelera buscas
- **Analogia:** Como índice de livro (encontra página rapidamente)

#### **Linha 2: `name: "IX_BebeNascido_ResponsavelId",`**
- **O que faz:** Define nome do índice
- **Convenção:** `IX_Tabela_Coluna`
- **`IX_`:** Prefixo para índices (diferencia de constraints)
- **Por que nomear:** Facilita identificar e remover

#### **Linha 3: `table: "BebeNascido",`**
- **O que faz:** Define em qual tabela criar o índice
- **`"BebeNascido"`:** Nome da tabela

#### **Linha 4: `column: "ResponsavelId");`**
- **O que faz:** Define qual coluna será indexada
- **`"ResponsavelId"`:** Coluna Foreign Key
- **Por que indexar:** Acelera buscas por responsável
  - **Exemplo:** "Buscar todos bebês do responsável X" fica muito mais rápido

### **SQL Gerado:**

```sql
CREATE INDEX IX_BebeNascido_ResponsavelId
ON BebeNascido(ResponsavelId);
```

---

## 📋 PARTE 7: Criando Índice ÚNICO

```csharp
migrationBuilder.CreateIndex(
    name: "IX_ExamesRealizados_BebeNascidoId_ExameSusId",
    table: "ExamesRealizados",
    columns: new[] { "BebeNascidoId", "ExameSusId" },
    unique: true);
```

### **Explicação das diferenças:**

#### **Linha: `columns: new[] { "BebeNascidoId", "ExameSusId" },`**
- **O que faz:** Cria índice em **múltiplas colunas**
- **`new[] { }`:** Array com nomes das colunas
- **Índice composto:** Combinação de duas colunas
- **Por que:** Garante que a combinação seja única

#### **Linha: `unique: true`**
- **O que faz:** Torna o índice **único**
- **Efeito:** Não permite valores duplicados
- **Exemplo:** Um bebê não pode ter o mesmo exame registrado duas vezes
- **Validação:** MySQL bloqueia inserção de duplicata

### **SQL Gerado:**

```sql
CREATE UNIQUE INDEX IX_ExamesRealizados_BebeNascidoId_ExameSusId
ON ExamesRealizados(BebeNascidoId, ExameSusId);
```

**Exemplo prático:**
- ✅ Permitido: Bebê A + Exame 1, Bebê A + Exame 2
- ❌ Bloqueado: Bebê A + Exame 1 (duplicata!)

---

## 📋 PARTE 8: Método Down() - Reverter Migration

```csharp
protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable(name: "BebeGestacao");
    migrationBuilder.DropTable(name: "Conteudos");
    // ... remove outras tabelas
    migrationBuilder.DropTable(name: "Responsaveis");
}
```

### **Explicação linha por linha:**

#### **Linha 1: `protected override void Down(...)`**
- **O que faz:** Método executado ao **reverter** migration
- **Quando:** Ao executar `dotnet ef database update MigrationAnterior`
- **Propósito:** Desfazer mudanças do método `Up()`

#### **Linha 2: `migrationBuilder.DropTable(name: "BebeGestacao");`**
- **O que faz:** Remove tabela do banco
- **`DropTable()`:** Método para deletar tabela
- **`name: "BebeGestacao"`:** Nome da tabela a remover
- **SQL gerado:**
  ```sql
  DROP TABLE BebeGestacao;
  ```

#### **Por que ordem inversa?**
- **Primeiro:** Remove tabelas que dependem de outras (filhas)
- **Depois:** Remove tabelas independentes (pais)
- **Razão:** Evita erro de Foreign Key (não pode deletar pai com filhos)

**Ordem correta:**
1. Remove `ExamesRealizados` (depende de BebeNascido e ExameSus)
2. Remove `BebeNascido` (depende de Responsaveis)
3. Remove `Responsaveis` (independente)

---

## 🎯 RESUMO: Fluxo Completo

### **1. Aplicar Migration (Up):**
```
EF Core executa Up()
    ↓
Cria banco com charset UTF-8
    ↓
Cria tabela Responsaveis
    ↓
Cria tabela BebeNascido (com FK para Responsaveis)
    ↓
Cria índices
    ↓
Banco pronto!
```

### **2. Reverter Migration (Down):**
```
EF Core executa Down()
    ↓
Remove índices
    ↓
Remove tabela BebeNascido
    ↓
Remove tabela Responsaveis
    ↓
Banco vazio!
```

---

## 💡 CONCEITOS AVANÇADOS

### **1. Por que `char(36)` para GUID?**
- GUID formatado: `xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx`
- Conta os caracteres: 8-4-4-4-12 = 36 caracteres
- CHAR é fixo (sempre 36), mais rápido que VARCHAR

### **2. Por que `datetime(6)` para timestamps?**
- `datetime`: Precisão de segundos
- `datetime(6)`: Precisão de microsegundos (6 dígitos)
- **Vantagem:** Pode ter múltiplos registros no mesmo segundo

### **3. Por que `ReferentialAction.Restrict`?**
- **Segurança:** Protege dados de exclusão acidental
- **Exemplo:** Não pode deletar responsável que tem bebês
- **Alternativa:** `Cascade` deletaria bebês também (perigoso!)

### **4. Por que índices em Foreign Keys?**
- **Performance:** Acelera JOINs e buscas
- **Exemplo:** "Buscar todos bebês do responsável X" fica instantâneo
- **Custo:** Pouco espaço, muito ganho de velocidade

---

## ✅ CHECKLIST: Entender Cada Parte

- [ ] Entender imports e namespace
- [ ] Entender declaração da classe
- [ ] Entender método `Up()` e `Down()`
- [ ] Entender criação de tabelas
- [ ] Entender tipos de dados (GUID, VARCHAR, DATETIME)
- [ ] Entender nullable vs not null
- [ ] Entender charset e collation
- [ ] Entender chaves primárias
- [ ] Entender Foreign Keys
- [ ] Entender índices simples
- [ ] Entender índices únicos compostos
- [ ] Entender ordem de remoção no `Down()`

---

---

## 📋 PARTE 9: Exemplo Completo - Tabela BebeNascido com Foreign Key

Vamos analisar a criação completa da tabela `BebeNascido` que tem relacionamento:

```csharp
migrationBuilder.CreateTable(
    name: "BebeNascido",
    columns: table => new
    {
        Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
        ResponsavelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
        Nome = table.Column<string>(type: "varchar(80)", nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        DataNascimento = table.Column<DateTime>(type: "datetime", nullable: false),
        Sexo = table.Column<int>(type: "int", nullable: false),
        TipoSanguineo = table.Column<int>(type: "int", nullable: false),
        IdadeMeses = table.Column<int>(type: "int", nullable: false),
        Peso = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
        Altura = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
        CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
        UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_BebeNascido", x => x.Id);
        table.ForeignKey(
            name: "FK_BebeNascido_Responsaveis_ResponsavelId",
            column: x => x.ResponsavelId,
            principalTable: "Responsaveis",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    })
    .Annotation("MySql:CharSet", "utf8mb4");
```

### **Análise detalhada de cada coluna:**

#### **1. Coluna Id:**
```csharp
Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
```
- **Tipo C#:** `Guid` (Global Unique Identifier)
- **Tipo SQL:** `char(36)` - String fixa de 36 caracteres
- **Por que 36?** GUID formatado: `8-4-4-4-12` = 36 chars
- **Exemplo real:** `a1b2c3d4-e5f6-7890-abcd-ef1234567890`
- **Collation ASCII:** GUIDs só usam 0-9, A-F, hífen (caracteres ASCII)
- **Obrigatório:** Sempre deve existir (chave primária)

#### **2. Coluna ResponsavelId:**
```csharp
ResponsavelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
```
- **Tipo:** Mesmo do Id (GUID)
- **Propósito:** Armazena referência ao responsável
- **Obrigatório:** Todo bebê deve ter responsável
- **Será Foreign Key:** Liga com tabela Responsaveis

#### **3. Coluna Nome:**
```csharp
Nome = table.Column<string>(type: "varchar(80)", nullable: false)
    .Annotation("MySql:CharSet", "utf8mb4")
```
- **Tipo C#:** `string` (texto)
- **Tipo SQL:** `varchar(80)` - String variável até 80 caracteres
- **UTF-8:** Suporta acentos, emojis, caracteres especiais
- **Exemplo:** "Maria Silva", "João Pedro", "Ana Clara"
- **Limite 80:** Nomes geralmente não passam de 80 caracteres

#### **4. Coluna DataNascimento:**
```csharp
DataNascimento = table.Column<DateTime>(type: "datetime", nullable: false)
```
- **Tipo C#:** `DateTime` (data e hora)
- **Tipo SQL:** `datetime` - Data e hora sem microsegundos
- **Formato:** `YYYY-MM-DD HH:MM:SS`
- **Exemplo:** `2024-01-15 14:30:00`
- **Por que datetime e não date?** Pode precisar da hora exata do nascimento
- **Obrigatório:** Data de nascimento sempre deve existir

#### **5. Coluna Sexo:**
```csharp
Sexo = table.Column<int>(type: "int", nullable: false)
```
- **Tipo C#:** `int` (número inteiro)
- **Tipo SQL:** `int` - Número inteiro de 32 bits
- **Por que int e não string?** Armazena enum (1=Masculino, 2=Feminino, 3=Outro)
- **Vantagem:** Menos espaço, mais rápido, validação fácil
- **Exemplo:** `1` = Masculino, `2` = Feminino
- **Obrigatório:** Sempre deve ter um valor

#### **6. Coluna TipoSanguineo:**
```csharp
TipoSanguineo = table.Column<int>(type: "int", nullable: false)
```
- **Mesma estrutura:** Enum armazenado como int
- **Valores:** 1=A+, 2=A-, 3=B+, 4=B-, 5=AB+, 6=AB-, 7=O+, 8=O-
- **Por que enum?** Valores fixos, não muda

#### **7. Coluna IdadeMeses:**
```csharp
IdadeMeses = table.Column<int>(type: "int", nullable: false)
```
- **Tipo:** Inteiro (número de meses)
- **Exemplo:** `0` = recém-nascido, `6` = 6 meses, `24` = 2 anos
- **Por que meses?** Mais preciso que anos para bebês
- **Cálculo:** Pode ser calculado a partir de DataNascimento

#### **8. Coluna Peso:**
```csharp
Peso = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
```
- **Tipo C#:** `decimal` (número decimal preciso)
- **Tipo SQL:** `decimal(10,2)` - Número com 10 dígitos totais, 2 decimais
- **Formato:** `NNNNNNNN.DD`
- **Exemplo:** `3.50` kg, `10.25` kg, `99999999.99` kg (máximo)
- **Por que decimal?** Precisão exata (não arredonda como float)
- **10,2:** Até 99.999.999,99 kg (suficiente para qualquer peso)

#### **9. Coluna Altura:**
```csharp
Altura = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
```
- **Mesma estrutura:** decimal(10,2)
- **Exemplo:** `0.50` m (50cm), `1.20` m (120cm)
- **Formato:** Em metros com 2 casas decimais

#### **10. Coluna CreatedAt:**
```csharp
CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
```
- **Tipo SQL:** `datetime(6)` - Com microsegundos
- **Formato:** `YYYY-MM-DD HH:MM:SS.ffffff`
- **Exemplo:** `2024-11-25 14:30:00.123456`
- **Por que microsegundos?** Pode criar múltiplos registros no mesmo segundo
- **Auditoria:** Registra quando foi criado

#### **11. Coluna UpdatedAt:**
```csharp
UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
```
- **Nullable:** Pode ser NULL
- **Por que NULL?** Quando cria, ainda não foi atualizado
- **Quando preenche:** Automaticamente quando registro é modificado
- **Auditoria:** Registra última modificação

### **Foreign Key - Explicação Completa:**

```csharp
table.ForeignKey(
    name: "FK_BebeNascido_Responsaveis_ResponsavelId",
    column: x => x.ResponsavelId,
    principalTable: "Responsaveis",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);
```

#### **O que acontece no banco:**

1. **Cria constraint:**
   ```sql
   ALTER TABLE BebeNascido
   ADD CONSTRAINT FK_BebeNascido_Responsaveis_ResponsavelId
   FOREIGN KEY (ResponsavelId) REFERENCES Responsaveis(Id);
   ```

2. **Validação automática:**
   - ✅ Permite: Inserir BebeNascido com ResponsavelId que existe
   - ❌ Bloqueia: Inserir BebeNascido com ResponsavelId que NÃO existe
   - ❌ Bloqueia: Deletar Responsavel que tem BebeNascido

3. **Exemplo prático:**
   ```sql
   -- ✅ PERMITIDO: Responsavel existe
   INSERT INTO BebeNascido (Id, ResponsavelId, ...) 
   VALUES ('bebê-id', 'responsavel-id-existente', ...);
   
   -- ❌ ERRO: Responsavel não existe
   INSERT INTO BebeNascido (Id, ResponsavelId, ...) 
   VALUES ('bebê-id', 'responsavel-id-inexistente', ...);
   -- Erro: Cannot add or update a child row: foreign key constraint fails
   
   -- ❌ ERRO: Tentar deletar responsável com bebês
   DELETE FROM Responsaveis WHERE Id = 'responsavel-com-bebes';
   -- Erro: Cannot delete or update a parent row: foreign key constraint fails
   ```

---

## 📋 PARTE 10: Índice Único Composto - ExamesRealizados

```csharp
migrationBuilder.CreateIndex(
    name: "IX_ExamesRealizados_BebeNascidoId_ExameSusId",
    table: "ExamesRealizados",
    columns: new[] { "BebeNascidoId", "ExameSusId" },
    unique: true);
```

### **Explicação detalhada:**

#### **Por que índice composto?**
- **Problema:** Um bebê não pode ter o mesmo exame registrado duas vezes
- **Solução:** Índice único na combinação (BebeNascidoId, ExameSusId)

#### **Como funciona:**

**Cenário 1 - Permitido:**
```
BebeNascidoId: bebê-A
ExameSusId: exame-1
✅ PERMITIDO (primeira vez)
```

**Cenário 2 - Permitido:**
```
BebeNascidoId: bebê-A
ExameSusId: exame-2
✅ PERMITIDO (exame diferente)
```

**Cenário 3 - Bloqueado:**
```
BebeNascidoId: bebê-A
ExameSusId: exame-1
❌ BLOQUEADO (duplicata!)
```

#### **SQL Gerado:**
```sql
CREATE UNIQUE INDEX IX_ExamesRealizados_BebeNascidoId_ExameSusId
ON ExamesRealizados(BebeNascidoId, ExameSusId);
```

#### **Benefícios:**
1. **Integridade:** Garante que não há duplicatas
2. **Performance:** Acelera buscas por bebê + exame
3. **Validação:** MySQL valida automaticamente

---

## 📋 PARTE 11: Tipos de Dados - Comparação Completa

### **GUID (char(36)):**
```csharp
Id = table.Column<Guid>(type: "char(36)", ...)
```
- **Tamanho:** 36 caracteres fixos
- **Exemplo:** `a1b2c3d4-e5f6-7890-abcd-ef1234567890`
- **Vantagem:** Único globalmente
- **Desvantagem:** Maior que int (36 bytes vs 4 bytes)

### **VARCHAR(n):**
```csharp
Nome = table.Column<string>(type: "varchar(80)", ...)
```
- **Tamanho:** Variável até n caracteres
- **Exemplo:** `"Maria"` usa 5 bytes, `"João Pedro"` usa 10 bytes
- **Vantagem:** Economiza espaço (só usa o necessário)
- **Desvantagem:** Mais lento que CHAR para tamanhos fixos

### **INT:**
```csharp
IdadeMeses = table.Column<int>(type: "int", ...)
```
- **Tamanho:** 4 bytes (32 bits)
- **Range:** -2.147.483.648 a 2.147.483.647
- **Uso:** Números inteiros, enums
- **Vantagem:** Muito rápido, pouco espaço

### **DECIMAL(10,2):**
```csharp
Peso = table.Column<decimal>(type: "decimal(10,2)", ...)
```
- **Tamanho:** Variável (depende da precisão)
- **Formato:** `NNNNNNNN.DD` (8 inteiros, 2 decimais)
- **Exemplo:** `3.50`, `10.25`, `99999999.99`
- **Vantagem:** Precisão exata (sem arredondamento)
- **Uso:** Valores monetários, medidas precisas

### **DATETIME:**
```csharp
DataNascimento = table.Column<DateTime>(type: "datetime", ...)
```
- **Tamanho:** 8 bytes
- **Formato:** `YYYY-MM-DD HH:MM:SS`
- **Range:** 1000-01-01 a 9999-12-31
- **Precisão:** Segundos

### **DATETIME(6):**
```csharp
CreatedAt = table.Column<DateTime>(type: "datetime(6)", ...)
```
- **Tamanho:** 8 bytes
- **Formato:** `YYYY-MM-DD HH:MM:SS.ffffff`
- **Precisão:** Microsegundos (6 dígitos)
- **Vantagem:** Pode ter múltiplos registros no mesmo segundo

---

## 📋 PARTE 12: Ordem de Criação das Tabelas

### **Por que ordem importa?**

A migration cria tabelas nesta ordem:

1. **Conteudos** (independente)
2. **ExameSus** (independente)
3. **Responsaveis** (independente) ← **CRÍTICO: Deve vir antes!**
4. **VacinaSus** (independente)
5. **BebeGestacao** (depende de Responsaveis)
6. **BebeNascido** (depende de Responsaveis)
7. **EventoAgenda** (depende de Responsaveis)
8. **ControlesFralda** (depende de BebeNascido)
9. **ControlesLeiteMaterno** (depende de BebeNascido)
10. **ControlesMamadeira** (depende de BebeNascido)
11. **ExamesRealizados** (depende de BebeNascido E ExameSus)
12. **VacinasAplicadas** (depende de BebeNascido E VacinaSus)

### **Regra:**
- **Primeiro:** Tabelas independentes (sem Foreign Keys)
- **Depois:** Tabelas que dependem de outras
- **Último:** Tabelas que dependem de múltiplas outras

### **O que acontece se inverter?**
```sql
-- ❌ ERRO: Tenta criar BebeNascido antes de Responsaveis
CREATE TABLE BebeNascido (
    ResponsavelId CHAR(36) REFERENCES Responsaveis(Id)  -- Responsaveis não existe ainda!
);
-- Erro: Table 'Responsaveis' doesn't exist
```

---

## 📋 PARTE 13: Ordem de Remoção no Down()

```csharp
protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable(name: "BebeGestacao");
    migrationBuilder.DropTable(name: "Conteudos");
    migrationBuilder.DropTable(name: "ControlesFralda");
    // ... outras
    migrationBuilder.DropTable(name: "Responsaveis");
}
```

### **Ordem correta (inversa da criação):**

1. **Remove primeiro:** Tabelas que dependem de outras (filhas)
2. **Remove depois:** Tabelas independentes (pais)

### **Por que ordem inversa?**

**Cenário errado:**
```sql
-- ❌ Tenta deletar Responsaveis primeiro
DROP TABLE Responsaveis;
-- Erro: Cannot delete table 'Responsaveis' because it is referenced by foreign key
-- BebeNascido ainda existe e referencia Responsaveis!
```

**Cenário correto:**
```sql
-- ✅ Remove BebeNascido primeiro
DROP TABLE BebeNascido;
-- ✅ Agora pode remover Responsaveis
DROP TABLE Responsaveis;
```

### **Ordem no código:**
1. Remove `ExamesRealizados` (depende de 2 tabelas)
2. Remove `VacinasAplicadas` (depende de 2 tabelas)
3. Remove `Controles*` (dependem de BebeNascido)
4. Remove `BebeNascido` (depende de Responsaveis)
5. Remove `BebeGestacao` (depende de Responsaveis)
6. Remove `EventoAgenda` (depende de Responsaveis)
7. Remove `ExameSus`, `VacinaSus` (independentes)
8. Remove `Conteudos` (independente)
9. Remove `Responsaveis` (independente, por último)

---

## 🎯 RESUMO FINAL: Entendendo Tudo

### **1. Estrutura do Arquivo:**
- **Imports:** O que precisa importar
- **Classe:** Herda de Migration
- **Método Up():** Cria tudo
- **Método Down():** Remove tudo

### **2. Criar Tabela:**
- **Nome:** Define nome no banco
- **Colunas:** Define cada coluna com tipo, tamanho, nullable
- **Constraints:** Chaves primárias, Foreign Keys
- **Anotações:** Charset, collation

### **3. Tipos de Dados:**
- **GUID:** Identificador único (char(36))
- **VARCHAR:** Texto variável
- **INT:** Números inteiros
- **DECIMAL:** Números decimais precisos
- **DATETIME:** Data e hora

### **4. Relacionamentos:**
- **Foreign Key:** Liga tabelas
- **Restrict:** Protege dados
- **Ordem:** Criar pais antes de filhos

### **5. Índices:**
- **Simples:** Acelera buscas
- **Único:** Evita duplicatas
- **Composto:** Combinação de colunas

---

---

## 📋 PARTE 14: Exemplos Práticos - SQL Gerado vs Código C#

### **Exemplo 1: Criar Tabela Simples**

**Código C#:**
```csharp
migrationBuilder.CreateTable(
    name: "Conteudos",
    columns: table => new
    {
        Id = table.Column<Guid>(type: "char(36)", nullable: false),
        Titulo = table.Column<string>(type: "varchar(80)", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Conteudos", x => x.Id);
    });
```

**SQL Gerado (o que realmente executa no banco):**
```sql
CREATE TABLE Conteudos (
    Id CHAR(36) NOT NULL,
    Titulo VARCHAR(80) NOT NULL,
    CONSTRAINT PK_Conteudos PRIMARY KEY (Id)
) CHARACTER SET utf8mb4;
```

**Comparação lado a lado:**
| Código C# | SQL Gerado | Explicação |
|-----------|------------|------------|
| `CreateTable(name: "Conteudos")` | `CREATE TABLE Conteudos` | Cria tabela |
| `type: "char(36)"` | `CHAR(36)` | Tipo fixo 36 chars |
| `nullable: false` | `NOT NULL` | Obrigatório |
| `PrimaryKey("PK_Conteudos", x => x.Id)` | `PRIMARY KEY (Id)` | Chave primária |

---

### **Exemplo 2: Foreign Key Completo**

**Código C#:**
```csharp
table.ForeignKey(
    name: "FK_BebeNascido_Responsaveis_ResponsavelId",
    column: x => x.ResponsavelId,
    principalTable: "Responsaveis",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);
```

**SQL Gerado:**
```sql
ALTER TABLE BebeNascido
ADD CONSTRAINT FK_BebeNascido_Responsaveis_ResponsavelId
FOREIGN KEY (ResponsavelId) 
REFERENCES Responsaveis(Id)
ON DELETE RESTRICT;
```

**Passo a passo do que acontece:**
1. **`ALTER TABLE BebeNascido`** - Modifica tabela BebeNascido
2. **`ADD CONSTRAINT FK_...`** - Adiciona constraint com nome
3. **`FOREIGN KEY (ResponsavelId)`** - Define coluna que será FK
4. **`REFERENCES Responsaveis(Id)`** - Aponta para tabela/coluna pai
5. **`ON DELETE RESTRICT`** - Ação ao tentar deletar pai

**Teste prático:**
```sql
-- ✅ PERMITIDO: Responsavel existe
INSERT INTO Responsaveis (Id, Nome, Email, ...) 
VALUES ('resp-1', 'Maria', 'maria@email.com', ...);

INSERT INTO BebeNascido (Id, ResponsavelId, Nome, ...) 
VALUES ('bebe-1', 'resp-1', 'João', ...);
-- ✅ Sucesso!

-- ❌ ERRO: Responsavel não existe
INSERT INTO BebeNascido (Id, ResponsavelId, Nome, ...) 
VALUES ('bebe-2', 'resp-inexistente', 'Pedro', ...);
-- ❌ Erro: Cannot add or update a child row: 
--    foreign key constraint fails (ResponsavelId não existe)

-- ❌ ERRO: Tentar deletar responsável com bebês
DELETE FROM Responsaveis WHERE Id = 'resp-1';
-- ❌ Erro: Cannot delete or update a parent row:
--    foreign key constraint fails (BebeNascido ainda referencia)
```

---

### **Exemplo 3: Índice Único Composto**

**Código C#:**
```csharp
migrationBuilder.CreateIndex(
    name: "IX_ExamesRealizados_BebeNascidoId_ExameSusId",
    table: "ExamesRealizados",
    columns: new[] { "BebeNascidoId", "ExameSusId" },
    unique: true);
```

**SQL Gerado:**
```sql
CREATE UNIQUE INDEX IX_ExamesRealizados_BebeNascidoId_ExameSusId
ON ExamesRealizados(BebeNascidoId, ExameSusId);
```

**Teste prático:**
```sql
-- ✅ PERMITIDO: Primeira inserção
INSERT INTO ExamesRealizados (Id, BebeNascidoId, ExameSusId, ...) 
VALUES ('exame-1', 'bebe-A', 'exame-1', ...);
-- ✅ Sucesso!

-- ✅ PERMITIDO: Mesmo bebê, exame diferente
INSERT INTO ExamesRealizados (Id, BebeNascidoId, ExameSusId, ...) 
VALUES ('exame-2', 'bebe-A', 'exame-2', ...);
-- ✅ Sucesso! (combinação diferente)

-- ✅ PERMITIDO: Bebê diferente, mesmo exame
INSERT INTO ExamesRealizados (Id, BebeNascidoId, ExameSusId, ...) 
VALUES ('exame-3', 'bebe-B', 'exame-1', ...);
-- ✅ Sucesso! (combinação diferente)

-- ❌ ERRO: Duplicata (mesma combinação)
INSERT INTO ExamesRealizados (Id, BebeNascidoId, ExameSusId, ...) 
VALUES ('exame-4', 'bebe-A', 'exame-1', ...);
-- ❌ Erro: Duplicate entry 'bebe-A-exame-1' for key 
--    'IX_ExamesRealizados_BebeNascidoId_ExameSusId'
```

---

## 📋 PARTE 15: Diferenças entre Tipos de Dados - Guia Completo

### **CHAR vs VARCHAR**

#### **CHAR(36) - Usado em GUIDs:**
```csharp
Id = table.Column<Guid>(type: "char(36)", ...)
```
- **Tamanho:** Sempre 36 bytes (fixo)
- **Exemplo:** `"a1b2c3d4-e5f6-7890-abcd-ef1234567890"` = 36 bytes
- **Vantagem:** Mais rápido (tamanho fixo conhecido)
- **Desvantagem:** Sempre usa 36 bytes, mesmo se menor
- **Uso:** GUIDs, códigos fixos

#### **VARCHAR(80) - Usado em textos:**
```csharp
Nome = table.Column<string>(type: "varchar(80)", ...)
```
- **Tamanho:** Variável até 80 bytes
- **Exemplo:** `"Maria"` = 5 bytes, `"João Pedro Silva"` = 17 bytes
- **Vantagem:** Economiza espaço (só usa o necessário)
- **Desvantagem:** Mais lento (precisa calcular tamanho)
- **Uso:** Textos variáveis (nomes, descrições)

**Comparação prática:**
```
CHAR(36):
"a1b2c3d4-e5f6-7890-abcd-ef1234567890" = 36 bytes
"abc" = 36 bytes (preenche com espaços) ← Desperdício!

VARCHAR(80):
"Maria" = 5 bytes
"João Pedro Silva" = 17 bytes
"abc" = 3 bytes ← Economiza!
```

---

### **DATETIME vs DATETIME(6)**

#### **DATETIME - Sem microsegundos:**
```csharp
DataNascimento = table.Column<DateTime>(type: "datetime", ...)
```
- **Formato:** `YYYY-MM-DD HH:MM:SS`
- **Exemplo:** `2024-11-25 14:30:00`
- **Precisão:** Segundos
- **Tamanho:** 8 bytes
- **Uso:** Datas que não precisam de precisão extrema

#### **DATETIME(6) - Com microsegundos:**
```csharp
CreatedAt = table.Column<DateTime>(type: "datetime(6)", ...)
```
- **Formato:** `YYYY-MM-DD HH:MM:SS.ffffff`
- **Exemplo:** `2024-11-25 14:30:00.123456`
- **Precisão:** Microsegundos (6 dígitos)
- **Tamanho:** 8 bytes (mesmo tamanho!)
- **Uso:** Timestamps de auditoria (pode criar múltiplos no mesmo segundo)

**Por que microsegundos?**
```
Sem microsegundos:
Registro 1: 2024-11-25 14:30:00
Registro 2: 2024-11-25 14:30:00  ← Mesmo timestamp!
Registro 3: 2024-11-25 14:30:00  ← Mesmo timestamp!

Com microsegundos:
Registro 1: 2024-11-25 14:30:00.123456
Registro 2: 2024-11-25 14:30:00.234567  ← Diferente!
Registro 3: 2024-11-25 14:30:00.345678  ← Diferente!
```

---

### **INT - Números Inteiros**

```csharp
IdadeMeses = table.Column<int>(type: "int", ...)
TipoResponsavel = table.Column<int>(type: "int", ...)
```

**Características:**
- **Tamanho:** 4 bytes (32 bits)
- **Range:** -2.147.483.648 a 2.147.483.647
- **Uso:** Números inteiros, enums

**Exemplo com Enum:**
```csharp
// No código C#:
public enum TipoResponsavel
{
    Mae = 1,
    Pai = 2,
    Parente = 3
}

// No banco (armazenado como INT):
TipoResponsavel = 1  // = Mae
TipoResponsavel = 2  // = Pai
TipoResponsavel = 3  // = Parente
```

**Por que INT e não VARCHAR?**
```
INT (4 bytes):
TipoResponsavel = 1  ← 4 bytes

VARCHAR (variável):
TipoResponsavel = "Mae"  ← 3 bytes + overhead
TipoResponsavel = "Parente"  ← 8 bytes + overhead
```

**Vantagens do INT:**
- ✅ Menos espaço
- ✅ Mais rápido (comparação numérica)
- ✅ Validação fácil (só aceita 1, 2, 3)
- ✅ Não depende de idioma

---

### **DECIMAL(10,2) - Números Decimais Precisos**

```csharp
Peso = table.Column<decimal>(type: "decimal(10,2)", ...)
```

**Formato:**
- **10:** Total de dígitos
- **2:** Dígitos após a vírgula
- **Resultado:** `NNNNNNNN.DD` (8 inteiros, 2 decimais)

**Exemplos:**
```
3.50     ← ✅ Válido (3 inteiros, 2 decimais)
10.25    ← ✅ Válido
99999999.99  ← ✅ Válido (máximo)
100000000.00 ← ❌ Erro (9 inteiros, excede 10)
3.500    ← ❌ Erro (3 decimais, máximo é 2)
```

**Por que DECIMAL e não FLOAT?**
```
DECIMAL (preciso):
Peso = 3.50  ← Exatamente 3.50
Peso = 10.25 ← Exatamente 10.25

FLOAT (aproximado):
Peso = 3.50  ← Pode ser 3.4999999 ou 3.5000001
Peso = 10.25 ← Pode ser 10.249999 ou 10.250001
```

**Uso:** Valores monetários, medidas precisas (peso, altura)

---

## 📋 PARTE 16: Collation (Colação) - Explicação Completa

### **O que é Collation?**

**Collation** define como o banco de dados:
- Ordena caracteres (A vem antes de B?)
- Compara caracteres (A = a?)
- Busca caracteres (case-sensitive?)

### **ASCII_GENERAL_CI - Para GUIDs:**

```csharp
Id = table.Column<Guid>(..., collation: "ascii_general_ci")
```

**Características:**
- **ASCII:** Apenas caracteres básicos (0-9, A-Z, hífen)
- **general_ci:** Case-insensitive (maiúscula = minúscula)
- **Exemplo:** `A` = `a` (mesmo valor)

**Por que ASCII para GUID?**
```
GUID: "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
      ↑ Apenas: 0-9, A-F, hífen (-)
      ← Não precisa de UTF-8!
```

**Comparação:**
```sql
-- Com ASCII:
'A' = 'a'  ← TRUE (case-insensitive)

-- Com UTF-8:
'A' = 'a'  ← Depende da collation
```

### **UTF8MB4 - Para Textos:**

```csharp
Nome = table.Column<string>(...)
    .Annotation("MySql:CharSet", "utf8mb4")
```

**Características:**
- **UTF-8:** Suporta todos os caracteres Unicode
- **MB4:** Versão completa (suporta emojis)
- **Exemplo:** `"João"`, `"María"`, `"😊"`

**Diferença UTF-8 vs UTF8MB4:**
```
UTF-8 (antigo):
"João"     ← ✅ Suporta
"😊"       ← ❌ Não suporta (emoji)

UTF8MB4 (novo):
"João"     ← ✅ Suporta
"😊"       ← ✅ Suporta (emoji)
"María"    ← ✅ Suporta (acentos)
```

---

## 📋 PARTE 17: ReferentialAction - Ações de Foreign Key

### **Restrict (Usado no projeto):**

```csharp
onDelete: ReferentialAction.Restrict
```

**O que faz:**
- **NÃO PERMITE** deletar registro pai se tiver filhos
- **Protege dados:** Evita exclusão acidental

**Exemplo:**
```sql
-- Tentar deletar responsável com bebês
DELETE FROM Responsaveis WHERE Id = 'resp-1';
-- ❌ ERRO: Cannot delete or update a parent row
--    BebeNascido ainda referencia este responsável!

-- Solução: Deletar bebês primeiro
DELETE FROM BebeNascido WHERE ResponsavelId = 'resp-1';
DELETE FROM Responsaveis WHERE Id = 'resp-1';
-- ✅ Agora funciona!
```

### **Cascade (Não usado, mas existe):**

```csharp
onDelete: ReferentialAction.Cascade
```

**O que faz:**
- **DELETA AUTOMATICAMENTE** filhos quando deleta pai
- **Perigoso:** Pode deletar dados sem querer!

**Exemplo:**
```sql
-- Com CASCADE:
DELETE FROM Responsaveis WHERE Id = 'resp-1';
-- ✅ Deleta responsável
-- ⚠️ DELETA TODOS OS BEBÊS AUTOMATICAMENTE!
--    (BebeNascido, BebeGestacao, EventoAgenda)
-- ← MUITO PERIGOSO!
```

**Por que não usar?**
- ❌ Pode deletar dados importantes sem querer
- ❌ Difícil reverter
- ❌ Pode causar perda de dados

### **SetNull (Não usado, mas existe):**

```csharp
onDelete: ReferentialAction.SetNull
```

**O que faz:**
- Define Foreign Key como NULL quando deleta pai
- **Requisito:** Coluna deve permitir NULL

**Exemplo:**
```sql
-- Com SetNull (se ResponsavelId permitir NULL):
DELETE FROM Responsaveis WHERE Id = 'resp-1';
-- ✅ Deleta responsável
-- ✅ Define ResponsavelId = NULL em todos os bebês
--    (BebeNascido.ResponsavelId vira NULL)
```

**Por que não usar?**
- ❌ Dados ficam "órfãos" (sem responsável)
- ❌ Pode quebrar lógica de negócio
- ❌ Dificulta consultas

---

## 📋 PARTE 18: Índices - Performance e Busca

### **Índice Simples:**

```csharp
migrationBuilder.CreateIndex(
    name: "IX_BebeNascido_ResponsavelId",
    table: "BebeNascido",
    column: "ResponsavelId");
```

**O que faz:**
- Cria estrutura que acelera buscas
- **Analogia:** Como índice de livro (encontra página rapidamente)

**Sem índice:**
```sql
-- Buscar todos bebês do responsável X
SELECT * FROM BebeNascido WHERE ResponsavelId = 'resp-1';
-- ⏱️ Lento: Precisa verificar TODAS as linhas (scan completo)
--    Se tiver 1.000.000 de bebês, verifica todos!
```

**Com índice:**
```sql
-- Mesma busca
SELECT * FROM BebeNascido WHERE ResponsavelId = 'resp-1';
-- ⚡ Rápido: Usa índice para encontrar diretamente
--    Encontra em milissegundos, mesmo com milhões de registros!
```

**Custo vs Benefício:**
- **Custo:** Pouco espaço extra (índice ocupa espaço)
- **Benefício:** Buscas muito mais rápidas
- **Regra:** Sempre indexar Foreign Keys!

### **Índice Único Composto:**

```csharp
migrationBuilder.CreateIndex(
    name: "IX_ExamesRealizados_BebeNascidoId_ExameSusId",
    table: "ExamesRealizados",
    columns: new[] { "BebeNascidoId", "ExameSusId" },
    unique: true);
```

**O que faz:**
1. **Acelera buscas** por combinação de colunas
2. **Garante unicidade** da combinação

**Benefícios:**
- ✅ Busca rápida: "Buscar exame X do bebê Y"
- ✅ Evita duplicatas: Um bebê não pode ter mesmo exame duas vezes
- ✅ Validação automática: MySQL bloqueia duplicatas

**Exemplo de busca otimizada:**
```sql
-- Busca usando índice composto
SELECT * FROM ExamesRealizados 
WHERE BebeNascidoId = 'bebe-A' AND ExameSusId = 'exame-1';
-- ⚡ Muito rápido: Usa índice composto
--    Encontra diretamente sem verificar todas as linhas
```

---

## 📋 PARTE 19: Ordem de Execução - Por que Importa?

### **Ordem de Criação (Up):**

```
1. Conteudos          ← Independente
2. ExameSus           ← Independente
3. Responsaveis       ← Independente (CRÍTICO!)
4. VacinaSus           ← Independente
5. BebeGestacao        ← Depende de Responsaveis
6. BebeNascido         ← Depende de Responsaveis
7. EventoAgenda        ← Depende de Responsaveis
8. ControlesFralda     ← Depende de BebeNascido
9. ControlesLeiteMaterno ← Depende de BebeNascido
10. ControlesMamadeira  ← Depende de BebeNascido
11. ExamesRealizados    ← Depende de BebeNascido E ExameSus
12. VacinasAplicadas    ← Depende de BebeNascido E VacinaSus
```

**Regra:** Sempre criar tabelas pai antes das filhas!

**O que acontece se inverter?**
```sql
-- ❌ ERRO: Tenta criar BebeNascido antes de Responsaveis
CREATE TABLE BebeNascido (
    ResponsavelId CHAR(36),
    FOREIGN KEY (ResponsavelId) REFERENCES Responsaveis(Id)
    -- ↑ Responsaveis ainda não existe!
);
-- Erro: Table 'Responsaveis' doesn't exist
```

### **Ordem de Remoção (Down):**

```
1. ExamesRealizados    ← Remove primeiro (depende de 2)
2. VacinasAplicadas    ← Remove primeiro (depende de 2)
3. ControlesFralda     ← Remove depois
4. ControlesLeiteMaterno ← Remove depois
5. ControlesMamadeira  ← Remove depois
6. BebeNascido         ← Remove depois
7. BebeGestacao        ← Remove depois
8. EventoAgenda        ← Remove depois
9. ExameSus            ← Remove independentes
10. VacinaSus           ← Remove independentes
11. Conteudos           ← Remove independentes
12. Responsaveis        ← Remove por último
```

**Regra:** Sempre remover tabelas filhas antes dos pais!

**O que acontece se inverter?**
```sql
-- ❌ ERRO: Tenta deletar Responsaveis antes de BebeNascido
DROP TABLE Responsaveis;
-- Erro: Cannot delete table 'Responsaveis' 
--    because it is referenced by foreign key
--    (BebeNascido ainda existe e referencia Responsaveis!)
```

---

## 🎯 RESUMO FINAL: Tudo que Você Precisa Saber

### **1. Estrutura Básica:**
- **Imports:** O que importar
- **Classe:** Herda de Migration
- **Up():** Cria tudo
- **Down():** Remove tudo

### **2. Criar Tabela:**
- **Nome:** Define nome no banco
- **Colunas:** Tipo, tamanho, nullable, charset
- **Constraints:** Chaves primárias, Foreign Keys
- **Anotações:** Configurações especiais

### **3. Tipos de Dados:**
- **GUID:** char(36) - Identificador único
- **VARCHAR:** Texto variável
- **INT:** Números inteiros (enums)
- **DECIMAL:** Números decimais precisos
- **DATETIME:** Data e hora
- **DATETIME(6):** Data e hora com microsegundos

### **4. Relacionamentos:**
- **Foreign Key:** Liga tabelas
- **Restrict:** Protege dados
- **Ordem:** Pais antes de filhos

### **5. Índices:**
- **Simples:** Acelera buscas
- **Único:** Evita duplicatas
- **Composto:** Combinação de colunas

### **6. Collation:**
- **ASCII:** Para GUIDs
- **UTF8MB4:** Para textos com acentos/emojis

### **7. Ordem:**
- **Criar:** Independentes → Dependentes
- **Remover:** Dependentes → Independentes

---

**Agora você entende CADA LINHA em detalhes profundos!** 🎓

**Próximo passo:** Tente criar sua própria migration e veja o SQL gerado!

