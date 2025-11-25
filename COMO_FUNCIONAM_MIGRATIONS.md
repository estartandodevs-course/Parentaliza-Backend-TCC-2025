# 📚 Como Funcionam as Migrations - Guia Completo

## 🎯 O que são Migrations?

**Migrations** são como "versões" do seu banco de dados. Elas permitem:
- ✅ Criar/alterar tabelas automaticamente
- ✅ Popular dados iniciais (seed data)
- ✅ Controlar mudanças no banco de forma organizada
- ✅ Reverter mudanças se necessário

**Pense assim:** É como um "histórico de versões" do seu banco de dados! 📝

---

## 📋 PASSO A PASSO: Como Criamos a Migration de Seed Data

### **PASSO 1: Criar a Migration**

Usamos o comando do Entity Framework Core para criar uma nova migration:

```powershell
dotnet ef migrations add SeedExameSusVacinaSus `
  --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj `
  --startup-project src/Parentaliza.API/Parentaliza.API.csproj `
  --context ApplicationDbContext
```

**O que acontece:**
- O EF Core cria um arquivo novo na pasta `Migrations`
- O nome do arquivo inclui a data/hora: `20251125214904_SeedExameSusVacinaSus.cs`
- O arquivo vem "vazio" (só com estrutura básica)

---

### **PASSO 2: Preencher a Migration**

Abrimos o arquivo criado e preenchemos com o código:

#### **Estrutura do Arquivo:**

```csharp
public partial class SeedExameSusVacinaSus : Migration
{
    // Método Up = O que fazer quando aplicar a migration
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Código aqui
    }

    // Método Down = O que fazer quando reverter a migration
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Código aqui
    }
}
```

#### **O que colocamos no método `Up`:**

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    // Inserir 10 exames SUS
    migrationBuilder.Sql(@"
        INSERT IGNORE INTO ExameSus (...) VALUES
        ('11111111-1111-1111-1111-111111111111', 'Teste do Pezinho', ...),
        ('11111111-1111-1111-1111-111111111112', 'Teste da Orelhinha', ...),
        ...
    ");

    // Inserir 27 vacinas SUS
    migrationBuilder.Sql(@"
        INSERT IGNORE INTO VacinaSus (...) VALUES
        ('22222222-2222-2222-2222-222222222221', 'BCG', ...),
        ('22222222-2222-2222-2222-222222222222', 'Hepatite B', ...),
        ...
    ");
}
```

**Por que `INSERT IGNORE`?**
- Se o registro já existir (mesmo GUID), não dá erro
- Permite executar a migration várias vezes sem problemas
- É específico do MySQL

**Por que GUIDs fixos?**
- Permite identificar os dados de seed facilmente
- Facilita reverter (sabemos exatamente quais remover)
- Evita duplicatas

#### **O que colocamos no método `Down`:**

```csharp
protected override void Down(MigrationBuilder migrationBuilder)
{
    // Remove os exames usando os GUIDs fixos
    migrationBuilder.Sql(@"
        DELETE FROM ExameSus WHERE Id IN (
            '11111111-1111-1111-1111-111111111111',
            '11111111-1111-1111-1111-111111111112',
            ...
        );
    ");

    // Remove as vacinas usando os GUIDs fixos
    migrationBuilder.Sql(@"
        DELETE FROM VacinaSus WHERE Id IN (
            '22222222-2222-2222-2222-222222222221',
            '22222222-2222-2222-2222-222222222222',
            ...
        );
    ");
}
```

**Por que o método `Down`?**
- Permite "desfazer" a migration se necessário
- Remove exatamente os dados que foram inseridos
- Mantém o banco limpo se precisar reverter

---

### **PASSO 3: Aplicar a Migration no Banco**

Depois de criar e preencher a migration, aplicamos no banco de dados:

```powershell
dotnet ef database update `
  --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj `
  --startup-project src/Parentaliza.API/Parentaliza.API.csproj `
  --context ApplicationDbContext
```

**O que acontece:**
1. O EF Core verifica quais migrations já foram aplicadas (tabela `__EFMigrationsHistory`)
2. Aplica apenas as migrations novas
3. Executa o método `Up()` da migration
4. Registra a migration como aplicada

---

## 🔍 DETALHES TÉCNICOS

### **1. Por que usar SQL direto (`migrationBuilder.Sql`)?**

- ✅ Mais controle sobre o SQL gerado
- ✅ Compatibilidade garantida com MySQL
- ✅ Permite usar comandos específicos do MySQL (como `INSERT IGNORE`)
- ✅ Melhor performance para inserções em lote

### **2. Estrutura do nome do arquivo:**

```
20251125214904_SeedExameSusVacinaSus.cs
│          │    │
│          │    └─ Nome descritivo
│          └─ Timestamp (ano-mês-dia-hora-minuto-segundo)
└─ Timestamp completo
```

**Por que timestamp?**
- Garante ordem de execução
- Evita conflitos entre desenvolvedores
- Facilita identificar quando foi criada

### **3. Tabela `__EFMigrationsHistory`:**

O EF Core cria uma tabela especial para rastrear migrations:

```sql
__EFMigrationsHistory
├─ MigrationId (nome da migration)
└─ ProductVersion (versão do EF Core)
```

**Exemplo:**
```
MigrationId: 20251125214904_SeedExameSusVacinaSus
ProductVersion: 9.0.0
```

---

## 📊 RESUMO VISUAL DO PROCESSO

```
┌─────────────────────────────────────────┐
│  1. Criar Migration                     │
│     dotnet ef migrations add ...        │
└──────────────┬──────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────┐
│  2. Preencher Migration                 │
│     - Método Up() = inserir dados        │
│     - Método Down() = remover dados     │
└──────────────┬──────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────┐
│  3. Aplicar Migration                   │
│     dotnet ef database update           │
└──────────────┬──────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────┐
│  4. Banco de Dados Atualizado          │
│     - Dados inseridos                   │
│     - Migration registrada              │
└─────────────────────────────────────────┘
```

---

## 🛠️ COMANDOS ÚTEIS

### **Ver migrations pendentes:**
```powershell
dotnet ef migrations list `
  --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj `
  --startup-project src/Parentaliza.API/Parentaliza.API.csproj `
  --context ApplicationDbContext
```

### **Reverter última migration:**
```powershell
dotnet ef database update NomeDaMigrationAnterior `
  --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj `
  --startup-project src/Parentaliza.API/Parentaliza.API.csproj `
  --context ApplicationDbContext
```

### **Remover última migration (sem aplicar):**
```powershell
dotnet ef migrations remove `
  --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj `
  --startup-project src/Parentaliza.API/Parentaliza.API.csproj `
  --context ApplicationDbContext
```

---

## ✅ CHECKLIST: Criar uma Migration de Seed Data

- [ ] 1. Criar a migration com `dotnet ef migrations add`
- [ ] 2. Abrir o arquivo criado na pasta `Migrations`
- [ ] 3. Preencher o método `Up()` com `migrationBuilder.Sql()`
- [ ] 4. Preencher o método `Down()` para reverter
- [ ] 5. Usar GUIDs fixos para identificar os dados
- [ ] 6. Usar `INSERT IGNORE` (MySQL) para evitar duplicatas
- [ ] 7. Testar aplicando a migration
- [ ] 8. Verificar se os dados foram inseridos corretamente

---

## 🎓 EXEMPLO PRÁTICO COMPLETO

### **Cenário:** Inserir 3 categorias iniciais

#### **1. Criar migration:**
```powershell
dotnet ef migrations add SeedCategorias --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj --startup-project src/Parentaliza.API/Parentaliza.API.csproj --context ApplicationDbContext
```

#### **2. Preencher o arquivo:**
```csharp
public partial class SeedCategorias : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            INSERT IGNORE INTO Categorias (Id, Nome, CreatedAt) VALUES
            ('33333333-3333-3333-3333-333333333331', 'Categoria A', NOW()),
            ('33333333-3333-3333-3333-333333333332', 'Categoria B', NOW()),
            ('33333333-3333-3333-3333-333333333333', 'Categoria C', NOW());
        ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            DELETE FROM Categorias WHERE Id IN (
                '33333333-3333-3333-3333-333333333331',
                '33333333-3333-3333-3333-333333333332',
                '33333333-3333-3333-3333-333333333333'
            );
        ");
    }
}
```

#### **3. Aplicar:**
```powershell
dotnet ef database update --project src/Parentaliza.Infrastructure/Parentaliza.Infrastructure.csproj --startup-project src/Parentaliza.API/Parentaliza.API.csproj --context ApplicationDbContext
```

---

## 💡 DICAS IMPORTANTES

1. **Sempre teste a migration antes de commitar**
   - Aplique localmente primeiro
   - Verifique se os dados foram inseridos corretamente

2. **Use GUIDs fixos e organizados**
   - Facilita identificar dados de seed
   - Permite reverter facilmente

3. **Sempre implemente o método `Down()`**
   - Permite reverter se necessário
   - Mantém o banco limpo

4. **Use `INSERT IGNORE` para MySQL**
   - Evita erros se executar a migration várias vezes
   - Torna a migration idempotente

5. **Documente o que a migration faz**
   - Comentários no código explicam o propósito
   - Facilita manutenção futura

---

## 🎯 RESUMO PARA EXPLICAR AOS AMIGOS

**Em poucas palavras:**

1. **Migration = versão do banco de dados**
2. **Criamos com:** `dotnet ef migrations add Nome`
3. **Preenchemos com:** SQL para inserir dados
4. **Aplicamos com:** `dotnet ef database update`
5. **Resultado:** Dados inseridos automaticamente no banco!

**Vantagens:**
- ✅ Automatiza inserção de dados
- ✅ Pode reverter se necessário
- ✅ Funciona em qualquer ambiente
- ✅ Histórico de mudanças organizado

---

**Pronto para explicar!** 🚀

