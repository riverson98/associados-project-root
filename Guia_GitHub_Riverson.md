# Guia GitHub para Riverson — Profissionalizar Repos e Criar Repo Novo

Este guia tem 3 partes:
1. **Ações URGENTES** no seu repo público atual (`associados-project-root`)
2. **Checklist para deixar qualquer repo apresentável** no portfólio
3. **Passo a passo** para criar um novo repo público profissional

---

## 1. AÇÕES URGENTES — `associados-project-root`

### 1.1. SECRETS EXPOSTOS NO REPO PÚBLICO (faça AGORA)

Eu inspecionei seu repo público `https://github.com/riverson98/associados-project-root` e os arquivos `.env.prod` e `.env.dev` estão **commitados publicamente**. O `docker-compose-prod.yml` referencia senhas de root do MySQL embutidas:

```
MYSQL_ROOT_PASSWORD: rSvwgBjcZ9NhQkS9W7xCDA==
MYSQL_ROOT_PASSWORD: W4uwjPD/xYec0fioZkfxAQ==
```

Recrutadores técnicos vão notar isso e é um sinal vermelho enorme. **Resolva nesta ordem:**

**Passo A — Rotacione as credenciais comprometidas**
- Troque as senhas do MySQL no banco em produção
- Troque qualquer chave/API key que estava nos `.env`
- Troque qualquer secret que estava no Azure Key Vault que tenha sido espelhado em `.env`

**Passo B — Remova os arquivos do repo e do histórico**

Abra um terminal na pasta do projeto e execute:

```bash
# 1) Adicione .env.* ao .gitignore
echo ".env" >> .gitignore
echo ".env.*" >> .gitignore
echo "!.env.example" >> .gitignore

# 2) Crie um .env.example com chaves SEM valores reais
cp .env.prod .env.example
# Abra .env.example e troque cada valor por um placeholder, ex:
#   DB_PASSWORD=YOUR_DB_PASSWORD_HERE

# 3) Remova os .env do tracking (eles continuam no disco)
git rm --cached .env.prod .env.dev
git add .gitignore .env.example
git commit -m "chore: remove secrets, add .env.example"
git push
```

**Passo C — Apague os secrets do histórico do Git**

O passo acima só impede commits futuros — os secrets ainda estão no histórico antigo. Use `git filter-repo` (recomendado pelo GitHub):

```bash
# Instale (pip)
pip install git-filter-repo

# Faça backup antes
cd ..
cp -r project-root project-root-backup
cd project-root

# Remova os arquivos do histórico completo
git filter-repo --invert-paths --path .env.prod --path .env.dev

# Force push (vai reescrever o histórico)
git push origin --force --all
git push origin --force --tags
```

**Passo D — Substitua hardcoded em `docker-compose-prod.yml`**

Troque qualquer senha que estiver direto no YAML por variável de ambiente:

```yaml
# Antes (ruim):
environment:
  MYSQL_ROOT_PASSWORD: rSvwgBjcZ9NhQkS9W7xCDA==

# Depois (bom):
environment:
  MYSQL_ROOT_PASSWORD: ${MYSQL_ROOT_PASSWORD}
```

E commit + push.

### 1.2. Repo sem descrição e sem README na raiz

Hoje, ao abrir seu repo, o visitante vê: "No description, website, or topics provided". Isso faz o projeto parecer abandonado. Vamos resolver.

**Adicione descrição, website e topics** (pela UI do GitHub):

1. Entre em `https://github.com/riverson98/associados-project-root`
2. Clique no ícone de engrenagem ⚙ ao lado de "About" (canto direito)
3. Preencha:
   - **Description**: `Plataforma full-stack em microsserviços .NET + Angular 17 para gestão de cooperativa/associação. Em produção.`
   - **Website**: `https://appdocdobrasil.com.br`
   - **Topics** (adicione): `dotnet`, `csharp`, `angular`, `microservices`, `clean-architecture`, `ddd`, `docker`, `nginx`, `azure-devops`, `mysql`, `jwt-authentication`, `fullstack`

### 1.3. Renomeie o repo para algo mais "vendável"

`associados-project-root` parece nome interno. Renomeie para algo como:
- `docbrasil-platform`
- `docbrasil-fullstack`

Em `Settings → General → Repository name`. O GitHub redireciona o URL antigo automaticamente.

### 1.4. Adicione um README.md profissional na raiz

Veja o **template de README** na seção 3.4 deste guia. Cole ele na raiz, ajuste com os dados do projeto e dê commit. Esse é o item de maior impacto visual.

---

## 2. CHECKLIST PARA QUALQUER REPO DO PORTFÓLIO

Todo repo que você quer usar no currículo deve passar nesta checklist:

**No repo:**
- [ ] `README.md` na raiz com: o que é, stack, screenshot/GIF, como rodar localmente, arquitetura
- [ ] `.gitignore` cobrindo `.env*`, `node_modules/`, `bin/`, `obj/`, `.vs/`, `dist/`
- [ ] `.env.example` com chaves de exemplo (sem valores reais)
- [ ] `LICENSE` (MIT é o padrão para portfólio)
- [ ] Sem secrets, senhas, tokens ou certificados no histórico do Git
- [ ] Commits com mensagens claras (Conventional Commits: `feat:`, `fix:`, `chore:`, `docs:`)
- [ ] Branch `main` como padrão (não `master`)

**Nas configurações do repo (no GitHub):**
- [ ] Description (1 linha contando o que faz)
- [ ] Website (se tiver demo ou URL de produção)
- [ ] Topics (5–10 tags relevantes da stack)
- [ ] Social preview image (opcional, mas aumenta cliques quando compartilhado)

**No seu perfil:**
- [ ] **Pin** dos 4–6 melhores repos (clique em "Customize your pins" no perfil)
- [ ] Foto de perfil
- [ ] Bio (você já tem boa: "Software Engineer | .NET/Java/NestJS | Open for Freelance Projects")
- [ ] Localização
- [ ] Link para LinkedIn
- [ ] **README de perfil** (repo público com seu próprio nome de usuário — `riverson98/riverson98`)

### 2.1. Sobre os outros 52 repos

Você tem 53 repos. Recrutadores olham os fixados (pins) e os mais recentes. Recomendação:

- **Mantenha públicos** apenas os 4–6 melhores e fixe esses no perfil
- **Torne privados** os repos de teste/tutorial/aulas (`Settings → Danger Zone → Change visibility`)
- **Apague** repos completamente vazios ou abandonados

---

## 3. CRIAR UM NOVO REPO PÚBLICO PROFISSIONAL — PASSO A PASSO

Suponha que você quer subir um projeto novo (ou refazer o `associados-project-root` do zero, mais limpo).

### 3.1. Criar o repo no GitHub

1. Vá em `https://github.com/new`
2. **Owner**: `riverson98`
3. **Repository name**: use `kebab-case`, curto e descritivo. Ex: `docbrasil-platform`
4. **Description**: 1 frase do que faz
5. **Public**
6. Marque:
   - ✅ Add a README file
   - ✅ Add .gitignore → escolha o template da stack principal (ex: `Node` ou `VisualStudio`)
   - ✅ Choose a license → `MIT License`
7. **Create repository**

### 3.2. Clonar e configurar localmente

```bash
git clone https://github.com/riverson98/docbrasil-platform.git
cd docbrasil-platform

# Configure seu nome/email (uma vez por máquina)
git config --global user.name "Riverson Costa"
git config --global user.email "riversonvicente@gmail.com"
```

### 3.3. Estrutura mínima de pastas (exemplo para projeto fullstack)

```
docbrasil-platform/
├── .github/
│   └── workflows/          # CI/CD do GitHub Actions
├── backend/
├── frontend/
├── docs/
│   └── architecture.md
├── .env.example
├── .gitignore
├── docker-compose.yml
├── LICENSE
└── README.md
```

### 3.4. Template de README.md (cole e adapte)

```markdown
# DocBrasil Platform

> Plataforma full-stack para gestão de cooperativa/associação, em microsserviços .NET + Angular 17. Em produção em [appdocdobrasil.com.br](https://appdocdobrasil.com.br).

![Status](https://img.shields.io/badge/status-em%20produção-success)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![Angular](https://img.shields.io/badge/Angular-17-DD0031)
![License](https://img.shields.io/badge/license-MIT-blue)

## Stack

- **Backend:** C# .NET 8, Clean Architecture, DDD, JWT
- **Frontend:** Angular 17, Angular Material, RxJS
- **Banco:** MySQL 8
- **Infra:** Docker Compose, NGINX (reverse proxy + SSL), Let's Encrypt
- **CI/CD:** Azure DevOps Pipelines, Azure Key Vault

## Arquitetura

Microsserviços orquestrados via Docker Compose:

- `api-gateway` — entrada única
- `identity-service` — autenticação JWT, gestão de usuários
- `associados-service` — domínio principal
- `frontend` — SPA Angular
- `nginx` — reverse proxy + terminação SSL
- `mysql-domain` / `mysql-auth` — bancos isolados por serviço

## Como rodar localmente

```bash
git clone https://github.com/riverson98/docbrasil-platform.git
cd docbrasil-platform
cp .env.example .env       # preencha com seus valores
docker compose up -d
```

Acesse `http://localhost:8080`.

## Estrutura do projeto

```
backend/
  DocAssociados/              # serviço principal
  DocAssociados.ApiGateway/   # gateway
  DocAssociados.Identity/     # autenticação
frontend/doc-brasil/          # Angular 17 app
nginx/                        # configs do reverse proxy
```

## Autor

[Riverson Costa](https://github.com/riverson98) — [LinkedIn](https://linkedin.com/in/riverson-dev)

## Licença

MIT
```

### 3.5. .gitignore essencial

Crie/edite `.gitignore`:

```
# Secrets
.env
.env.*
!.env.example

# Node / Angular
node_modules/
dist/
.angular/

# .NET
bin/
obj/
*.user
.vs/

# OS
.DS_Store
Thumbs.db
```

### 3.6. .env.example

Sempre tenha um `.env.example` versionado. Só com chaves, sem valores reais:

```
# Banco de dados
DB_HOST=
DB_PORT=3306
DB_USER=
DB_PASSWORD=
DB_NAME=

# JWT
JWT_SECRET=

# APIs externas
ANTHROPIC_API_KEY=
```

### 3.7. Primeiro commit e push

```bash
git add .
git commit -m "feat: initial commit with README, .gitignore, .env.example"
git push origin main
```

### 3.8. Configure description, topics e website pela UI

(Mesmo passo da seção 1.2 — engrenagem ⚙ ao lado de "About".)

### 3.9. Fixe o repo no perfil

1. Vá em `https://github.com/riverson98`
2. Clique em "Customize your pins"
3. Selecione até 6 repos

---

## 4. BÔNUS: README de Perfil (alta visibilidade)

Crie um repo público chamado **exatamente** `riverson98` (igual ao seu username). O README dele aparece no topo do seu perfil GitHub.

```bash
# No GitHub: New repo → name = riverson98 → Public → Add README
```

Template sugerido:

```markdown
### Olá! Eu sou o Riverson 👋

Desenvolvedor Fullstack com 5 anos de experiência em sistemas críticos de produção.
Backend em C# (.NET 8) e NestJS · Frontend em Angular/React · Python para ETL e IA.

🔭 Trabalho como Dev na **Qualyteam** e estou desenvolvendo o **Auditor 2.0** — ETL com IA generativa.
🌱 Atualmente estudando arquitetura de microsserviços avançada e integração de LLMs em produção.
💼 Aberto para projetos freelance — [LinkedIn](https://linkedin.com/in/riverson-dev) · riversonvicente@gmail.com

#### Stack
![C#](https://img.shields.io/badge/-C%23-239120?logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/-.NET-512BD4?logo=dotnet&logoColor=white)
![NestJS](https://img.shields.io/badge/-NestJS-E0234E?logo=nestjs&logoColor=white)
![Angular](https://img.shields.io/badge/-Angular-DD0031?logo=angular&logoColor=white)
![React](https://img.shields.io/badge/-React-61DAFB?logo=react&logoColor=black)
![Python](https://img.shields.io/badge/-Python-3776AB?logo=python&logoColor=white)
![Docker](https://img.shields.io/badge/-Docker-2496ED?logo=docker&logoColor=white)
```

---

## Ordem recomendada de execução

1. **HOJE** — Rotacionar senhas + remover `.env.*` do repo `associados-project-root` (seção 1.1)
2. **Hoje/amanhã** — Adicionar descrição, topics, website e README na raiz do repo (1.2 a 1.4)
3. **Esta semana** — Criar README de perfil (`riverson98/riverson98`)
4. **Esta semana** — Limpar/privatizar os 52 outros repos, deixando só os melhores fixados
5. **Quando puder** — Subir um repo novo bem-feito seguindo a seção 3 (pode ser o Auditor 2.0 quando estiver mais maduro)

Qualquer dúvida ou se quiser que eu já gere o README.md pronto pra colar no repo, é só pedir!
