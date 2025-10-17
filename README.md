# Boas práticas de programação em C# (iniciante)

Este repositório minimalista contém:

- `README.md`: um guia direto sobre boas práticas em C# para quem está começando.
- `Program.cs`: um único arquivo C# com um exemplo prático, comentado e fácil de executar, aplicando as boas práticas descritas.

## O que você vai aprender

- Convenções de nomenclatura (PascalCase, camelCase)
- Funções pequenas e responsabilidade única
- Validação defensiva ("guard clauses")
- Tratamento de exceções sem engolir erros
- Imutabilidade com `record` e inicializadores
- Comentários XML para gerar documentação
- Uso de `using`/`IDisposable`
- LINQ e coleções (com cuidados)
- Nullabilidade (`#nullable enable`) e checagens de nulos
- Dicas de estilo: `var`, interpolação de strings, `CultureInfo`

## Estrutura

Apenas dois arquivos:

- `README.md`
- `Program.cs`

Você pode copiar o `Program.cs` para dentro de um projeto console e executar.

## Como executar no Windows (PowerShell)

Se você ainda não tem um projeto, crie um temporário para rodar o arquivo único. O conteúdo original desse projeto será substituído pelo `Program.cs` deste repositório.

1) Verifique o .NET SDK:

```powershell
dotnet --version
```

2) Crie um projeto console temporário (ex.: `BoasPraticasDemo`):

```powershell
mkdir BoasPraticasDemo; cd BoasPraticasDemo
 dotnet new console -f net8.0
```

3) Substitua o `Program.cs` do projeto pelo deste repositório (ajuste o caminho conforme onde você salvou este repo):

```powershell
# Exemplo: copie a partir da pasta onde estão README.md e Program.cs
Copy-Item "..\Program.cs" -Destination ".\Program.cs" -Force
```

4) Execute:

```powershell
dotnet run
```

Dica: Se preferir usar o compilador `csc` diretamente, ele vem com o Visual Studio (Prompt de Desenvolvedor). No entanto, o fluxo com `dotnet` é mais simples para iniciantes.

## Boas práticas aplicadas (resumo)

- Nomes claros e consistentes
  - Tipos e métodos: PascalCase (`Pessoa`, `CalcularMedia`)
  - Variáveis locais e parâmetros: camelCase (`idade`, `valores`)
- Funções pequenas e focadas em uma tarefa
- Validação de entrada (retorno cedo/"early return")
- Não engolir exceções; tratar no nível certo (borda do sistema) e informar o usuário
- Imutabilidade quando possível (ex.: `record`)
- Comentários XML para ferramentas de documentação
- Evitar `null` quando possível; quando inevitável, checar antes de usar
- `using` para liberar recursos (arquivos, conexões, etc.)
- LINQ para legibilidade, com cuidado de complexidade e performance
- Cultura invariante quando fizer parsing/formatting que não depende de localização

## Saída esperada (exemplo)

Durante a execução, o programa solicitará nome e idade, mostrará uma estimativa de ano de nascimento, calculará a média de uma lista de notas e salvará um arquivo `relatorio.txt` com um pequeno relatório.

