# Guia: Como Funciona a Correlação entre Exames/Vacinas SUS e Realizações/Aplicações

## 📋 Visão Geral

O sistema possui **duas camadas de entidades** que trabalham juntas:

### 1. **Entidades de Catálogo (Referência)**
- `ExameSus` - Catálogo de exames obrigatórios do SUS
- `VacinaSus` - Catálogo de vacinas obrigatórias do SUS

**Características:**
- ✅ Dados de **referência** (não modificáveis pelos usuários finais)
- ✅ Populados via **seed/migration** com dados oficiais do SUS
- ✅ Contêm informações sobre faixa etária, descrição, etc.
- ✅ Endpoints apenas de **leitura** (GET)

### 2. **Entidades de Registro (Transacional)**
- `ExameRealizado` - Registro individual de cada exame realizado por um bebê
- `VacinaAplicada` - Registro individual de cada vacina aplicada em um bebê

**Características:**
- ✅ Dados **transacionais** (criados/modificados pelos usuários)
- ✅ Um registro por bebê + exame/vacina
- ✅ Contêm status (realizado/aplicado ou não), data, observações
- ✅ Endpoints de **criação/atualização** (POST/PUT)

---

## 🔗 Como Funciona a Correlação

### Relacionamento:
```
ExameSus (1) ──── (N) ExameRealizado (N) ──── (1) BebeNascido
VacinaSus (1) ──── (N) VacinaAplicada (N) ──── (1) BebeNascido
```

### Fluxo de Uso:

1. **Listar Exames/Vacinas Obrigatórios para um Bebê**
   ```
   GET /api/ExameRealizado/ListarPorBebe/{bebeNascidoId}
   GET /api/VacinaAplicada/ListarPorBebe/{bebeNascidoId}
   ```
   
   **O que retorna:**
   - ✅ **TODOS** os exames/vacinas SUS obrigatórios (do catálogo)
   - ✅ Para cada um, mostra:
     - Informações do exame/vacina (nome, descrição, faixa etária)
     - Status: `realizado: true/false` ou `aplicada: true/false`
     - Data de realização/aplicação (se foi feito)
     - Observações (se houver)

2. **Marcar como Realizado/Aplicado**
   ```
   POST /api/ExameRealizado/MarcarRealizado/{bebeNascidoId}/{exameSusId}
   POST /api/VacinaAplicada/MarcarAplicada/{bebeNascidoId}/{vacinaSusId}
   ```
   
   **O que faz:**
   - ✅ Se o registro não existe, **cria** um novo `ExameRealizado`/`VacinaAplicada`
   - ✅ Se já existe, **atualiza** o registro existente
   - ✅ Marca como `realizado: true` ou `aplicada: true`
   - ✅ Salva a data e observações

3. **Desmarcar (Marcar como Não Realizado/Não Aplicado)**
   ```
   PUT /api/ExameRealizado/Desmarcar/{bebeNascidoId}/{exameSusId}
   PUT /api/VacinaAplicada/Desmarcar/{bebeNascidoId}/{vacinaSusId}
   ```
   
   **O que faz:**
   - ✅ Atualiza o registro existente
   - ✅ Marca como `realizado: false` ou `aplicada: false`
   - ✅ Remove data e observações

---

## 📊 Exemplo de Resposta da Listagem

### GET /api/ExameRealizado/ListarPorBebe/{bebeNascidoId}

```json
[
  {
    "exameSusId": "guid-do-exame-sus",
    "nomeExame": "Teste do Pezinho",
    "categoriaFaixaEtaria": "Recém-nascido",
    "idadeMinMeses": 0,
    "idadeMaxMeses": 1,
    "realizado": true,
    "dataRealizacao": "2025-01-15T10:30:00Z",
    "observacoes": "Exame realizado com sucesso"
  },
  {
    "exameSusId": "guid-de-outro-exame",
    "nomeExame": "Teste da Orelhinha",
    "categoriaFaixaEtaria": "Recém-nascido",
    "idadeMinMeses": 0,
    "idadeMaxMeses": 1,
    "realizado": false,
    "dataRealizacao": null,
    "observacoes": null
  }
]
```

---

## 🎯 Casos de Uso

### Caso 1: Verificar quais exames/vacinas o bebê já fez
1. Chama `ListarPorBebe`
2. Filtra os itens com `realizado: true` ou `aplicada: true`
3. Mostra lista de "Concluídos"

### Caso 2: Verificar quais exames/vacinas estão pendentes
1. Chama `ListarPorBebe`
2. Filtra os itens com `realizado: false` ou `aplicada: false`
3. Mostra lista de "Pendentes"

### Caso 3: Verificar exames/vacinas por faixa etária
1. Chama `ListarPorBebe`
2. Calcula idade do bebê em meses
3. Filtra exames/vacinas onde:
   - `idadeMinMeses <= idadeDoBebe <= idadeMaxMeses`
4. Mostra lista de "Recomendados para a idade"

### Caso 4: Marcar exame/vacina como feito
1. Usuário clica em "Marcar como realizado"
2. Chama `MarcarRealizado` com data e observações
3. Atualiza a lista automaticamente

### Caso 5: Desmarcar (corrigir erro)
1. Usuário clica em "Desmarcar"
2. Chama `Desmarcar`
3. O exame/vacina volta para status "não realizado/não aplicado"

---

## 🔍 Endpoints Disponíveis

### Exames SUS (Catálogo - Somente Leitura)
- `GET /api/ExameSus/ObterTodos` - Lista todos os exames SUS
- `GET /api/ExameSus/Obter/{id}` - Obtém um exame SUS específico

### Vacinas SUS (Catálogo - Somente Leitura)
- `GET /api/VacinaSus/ObterTodos` - Lista todas as vacinas SUS
- `GET /api/VacinaSus/Obter/{id}` - Obtém uma vacina SUS específica

### Exames Realizados (Registro - Leitura e Escrita)
- `GET /api/ExameRealizado/ListarPorBebe/{bebeNascidoId}` - Lista todos os exames SUS com status para um bebê
- `POST /api/ExameRealizado/MarcarRealizado/{bebeNascidoId}/{exameSusId}` - Marca como realizado
- `PUT /api/ExameRealizado/Desmarcar/{bebeNascidoId}/{exameSusId}` - Desmarca (marca como não realizado)

### Vacinas Aplicadas (Registro - Leitura e Escrita)
- `GET /api/VacinaAplicada/ListarPorBebe/{bebeNascidoId}` - Lista todas as vacinas SUS com status para um bebê
- `POST /api/VacinaAplicada/MarcarAplicada/{bebeNascidoId}/{vacinaSusId}` - Marca como aplicada
- `PUT /api/VacinaAplicada/Desmarcar/{bebeNascidoId}/{vacinaSusId}` - Desmarca (marca como não aplicada)

---

## 💡 Dicas de Implementação no Frontend

1. **Tela de Listagem:**
   - Use `ListarPorBebe` para obter todos os exames/vacinas
   - Mostre checkbox/switch baseado em `realizado`/`aplicada`
   - Destaque itens pendentes (vermelho) e concluídos (verde)

2. **Filtros:**
   - Por status: Pendentes / Concluídos / Todos
   - Por faixa etária: Filtre usando `idadeMinMeses` e `idadeMaxMeses`
   - Por categoria: Use `categoriaFaixaEtaria`

3. **Ações:**
   - Checkbox marcado → Chama `MarcarRealizado`/`MarcarAplicada`
   - Checkbox desmarcado → Chama `Desmarcar`
   - Modal de detalhes → Mostra data, observações, etc.

---

## ✅ Resumo

- **ExameSus/VacinaSus** = "O que deve ser feito" (catálogo)
- **ExameRealizado/VacinaAplicada** = "O que foi feito" (registro individual)
- **ListarPorBebe** = Combina ambos mostrando status de cada item
- **Marcar/Desmarcar** = Atualiza o status individual do bebê

