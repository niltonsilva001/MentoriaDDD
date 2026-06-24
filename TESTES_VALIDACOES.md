# 📋 Bateria de Testes - Validadores

## 🎯 Resumo Executivo

Teste aqui os validadores básicos da aplicação MentoriaDDD. Use cURL, Postman ou seu cliente HTTP favorito.

---

## 🔧 Testes Manuais - NOME

### ✅ Teste 1: Nome Válido
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Validação de email e CPF será feita depois. Nome PASSA ✓

---

### ❌ Teste 2: Nome Vazio
```bash
POST /clientes
{
  "nome": "",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "Nome é obrigatório." ❌

---

### ❌ Teste 3: Nome com Menos de 3 Caracteres
```bash
POST /clientes
{
  "nome": "Jo",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "Nome deve ter pelo menos 3 caracteres." ❌

---

### ❌ Teste 4: Nome com Mais de 100 Caracteres
```bash
POST /clientes
{
  "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "Nome deve ter no máximo 100 caracteres." ❌

---

### ❌ Teste 5: Nome com Números
```bash
POST /clientes
{
  "nome": "João Silva 123",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "Nome não pode conter números ou caracteres especiais." ❌

---

### ❌ Teste 6: Nome com Caracteres Especiais
```bash
POST /clientes
{
  "nome": "João Silva @#$",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "Nome não pode conter números ou caracteres especiais." ❌

---

### ✅ Teste 7: Nome com Acentos (DEVE PASSAR)
```bash
POST /clientes
{
  "nome": "José Antônio Pereira",
  "email": "jose@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Nome PASSA ✓

---

## 🔧 Testes Manuais - EMAIL

### ✅ Teste 8: Email Válido Simples
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "12345678901"
}
```
**Esperado:** Email PASSA ✓

---

### ❌ Teste 9: Email Vazio
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "E-mail é obrigatório." ❌

---

### ❌ Teste 10: Email Sem @
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joaoexample.com",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "E-mail inválido." ❌

---

### ❌ Teste 11: Email Sem Domínio
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "E-mail inválido." ❌

---

### ❌ Teste 12: Email Sem Extensão
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@dominio",
  "cpf": "12345678901"
}
```
**Esperado:** Erro - "E-mail inválido." ❌

---

### ✅ Teste 13: Email com Múltiplos Pontos
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao.silva@empresa.com.br",
  "cpf": "12345678901"
}
```
**Esperado:** Email PASSA ✓

---

## 🔧 Testes Manuais - CPF

### ❌ Teste 14: CPF Vazio
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": ""
}
```
**Esperado:** Erro - "CPF é obrigatório." ❌

---

### ❌ Teste 15: CPF com Menos de 11 Dígitos
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "1234567890"
}
```
**Esperado:** Erro - "CPF inválido." ❌

---

### ❌ Teste 16: CPF com Todos os Dígitos Iguais
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "11111111111"
}
```
**Esperado:** Erro - "CPF inválido." ❌

---

### ❌ Teste 17: CPF com Letras
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "12345678ABC"
}
```
**Esperado:** Erro - "CPF inválido." ❌

---

### ✅ Teste 18: CPF Válido COM Formatação
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "111.444.777-35"
}
```
**Esperado:** CPF PASSA (algoritmo real validado) ✓

---

### ✅ Teste 19: CPF Válido SEM Formatação
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "11144477735"
}
```
**Esperado:** CPF PASSA (algoritmo real validado) ✓

---

### ❌ Teste 20: CPF com Dígitos Verificadores Errados
```bash
POST /clientes
{
  "nome": "João Silva",
  "email": "joao@example.com",
  "cpf": "11144477736"
}
```
**Esperado:** Erro - "CPF inválido." ❌

---

## 📊 Casos de Teste Combinados

### ❌ Teste 21: Todos os Campos Vazios
```bash
POST /clientes
{
  "nome": "",
  "email": "",
  "cpf": ""
}
```
**Esperado:** 3 erros (Nome, Email, CPF) ❌

---

### ✅ Teste 22: Todos os Campos Válidos
```bash
POST /clientes
{
  "nome": "José Antônio Silva",
  "email": "jose.antonio@empresa.com.br",
  "cpf": "11144477735"
}
```
**Esperado:** SUCESSO - Cliente criado com ID ✓

---

## 🎯 Checklist de Validação

| # | Teste | Campo | Tipo | Status |
|----|-------|-------|------|--------|
| 1 | Nome Válido | Nome | ✅ Pass | [ ] |
| 2 | Nome Vazio | Nome | ❌ Fail | [ ] |
| 3 | Nome < 3 chars | Nome | ❌ Fail | [ ] |
| 4 | Nome > 100 chars | Nome | ❌ Fail | [ ] |
| 5 | Nome com números | Nome | ❌ Fail | [ ] |
| 6 | Nome com especiais | Nome | ❌ Fail | [ ] |
| 7 | Nome com acentos | Nome | ✅ Pass | [ ] |
| 8 | Email válido simples | Email | ✅ Pass | [ ] |
| 9 | Email vazio | Email | ❌ Fail | [ ] |
| 10 | Email sem @ | Email | ❌ Fail | [ ] |
| 11 | Email sem domínio | Email | ❌ Fail | [ ] |
| 12 | Email sem extensão | Email | ❌ Fail | [ ] |
| 13 | Email múltiplos pontos | Email | ✅ Pass | [ ] |
| 14 | CPF vazio | CPF | ❌ Fail | [ ] |
| 15 | CPF < 11 dígitos | CPF | ❌ Fail | [ ] |
| 16 | CPF dígitos iguais | CPF | ❌ Fail | [ ] |
| 17 | CPF com letras | CPF | ❌ Fail | [ ] |
| 18 | CPF válido formatado | CPF | ✅ Pass | [ ] |
| 19 | CPF válido sem formato | CPF | ✅ Pass | [ ] |
| 20 | CPF dígitos errados | CPF | ❌ Fail | [ ] |
| 21 | Todos vazios | Múltiplos | ❌ Fail | [ ] |
| 22 | Todos válidos | Múltiplos | ✅ Pass | [ ] |

---

## 🚀 Como Executar

### 1. Com cURL
```bash
curl -X POST http://localhost:5000/clientes \
  -H "Content-Type: application/json" \
  -d '{"nome":"João","email":"joao@test.com","cpf":"11144477735"}'
```

### 2. Com Postman
1. Import a collection
2. Configure as requisições com os dados acima
3. Execute e valide as respostas

### 3. Com PowerShell
```powershell
$body = @{
    nome = "João"
    email = "joao@test.com"
    cpf = "11144477735"
} | ConvertTo-Json

Invoke-WebRequest -Uri http://localhost:5000/clientes -Method POST `
  -ContentType "application/json" -Body $body
```

---

## ✅ Critério de Sucesso

✅ **TODOS os testes PASS devem retornar 200 ou criar o cliente**
❌ **TODOS os testes FAIL devem retornar 400 com mensagem de erro clara**

---

## 📝 Notas

- Use CPF real válido: `111.444.777-35` ou `11144477735`
- Acentos em nomes DEVEM ser aceitos
- Formatação de CPF é opcional
- Valide sempre a mensagem de erro retornada
