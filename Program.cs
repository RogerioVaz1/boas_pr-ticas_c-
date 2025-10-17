// Exemplo prático: Boas práticas de programação em C# para iniciantes
// - Nomes claros (PascalCase para tipos/métodos; camelCase para variáveis/parâmetros)
// - Funções pequenas e responsabilidade única
// - Validação defensiva e retorno cedo (guard clauses)
// - Tratamento de exceções na borda (Main)
// - Imutabilidade com 'record'
// - Comentários XML para documentação
// - Uso de 'using' para recursos que precisam ser liberados
// - LINQ com cuidado e legibilidade

#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BoasPraticas;

/// <summary>
/// Representa uma pessoa com <see cref="Nome"/> e <see cref="Idade"/>.
/// Imutável (record), ideal para transportar dados com segurança e clareza.
/// </summary>
/// <param name="Nome">Nome da pessoa (não vazio).</param>
/// <param name="Idade">Idade em anos, valor não negativo.</param>
public sealed record Pessoa(string Nome, int Idade)
{
    /// <summary>
    /// Representação amigável da pessoa.
    /// </summary>
    public override string ToString() => $"Pessoa: {Nome} ({Idade} anos)";
}

internal static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            Run();
            return 0; // sucesso
        }
        catch (Exception ex)
        {
            // Evite engolir exceções silenciosamente. Informe algo útil ao usuário.
            Console.Error.WriteLine($"Erro inesperado: {ex.Message}");
            return 1; // código de erro genérico
        }
    }

    /// <summary>
    /// Ponto de orquestração do exemplo. Mantém <c>Main</c> enxuto.
    /// </summary>
    private static void Run()
    {
        EscreverTitulo("Boas práticas em C# (iniciante)");

        var nome = LerNomeObrigatorio("Digite seu nome: ");
        var idade = LerIdade("Digite sua idade: ");

        var pessoa = new Pessoa(nome, idade);
        Console.WriteLine($"Olá, {pessoa.Nome}. Você tem {pessoa.Idade} anos.");

        var anoNascimento = CalcularAnoNascimento(pessoa, DateTime.Today.Year);
        Console.WriteLine($"Ano de nascimento (estimado): {anoNascimento}");

        // Exemplo com coleções + LINQ (simples e legível)
        var notas = new List<double> { 8.5, 7.0, 9.0, 6.5 };
        var media = CalcularMedia(notas);
        Console.WriteLine($"Média das notas: {media:F2}");

        // Exemplo de escrita segura em arquivo (IDisposable) com 'using'
        using var writer = CriarWriterSeguro("relatorio.txt");
        writer.WriteLine($"Relatório gerado em {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        writer.WriteLine(pessoa);
        writer.WriteLine($"Média das notas: {media:F2}");
        Console.WriteLine("Relatório salvo em 'relatorio.txt'.");
    }

    /// <summary>
    /// Exibe um título com sublinhado.
    /// </summary>
    private static void EscreverTitulo(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return;
        Console.WriteLine(texto);
        Console.WriteLine(new string('-', Math.Min(texto.Length, 60)));
    }

    /// <summary>
    /// Lê um nome não vazio do console.
    /// </summary>
    private static string LerNomeObrigatorio(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var entrada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entrada))
            {
                // Normalize espaços e capitalização simples
                var nome = entrada.Trim();
                return nome;
            }
            Console.WriteLine("Nome não pode ser vazio. Tente novamente.\n");
        }
    }

    /// <summary>
    /// Lê uma idade válida (0 a 130 anos) do console.
    /// </summary>
    private static int LerIdade(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var entrada = Console.ReadLine();
            if (int.TryParse(entrada, NumberStyles.Integer, CultureInfo.InvariantCulture, out var idade))
            {
                if (idade is >= 0 and <= 130)
                {
                    return idade;
                }
                Console.WriteLine("Idade deve estar entre 0 e 130. Tente novamente.\n");
            }
            else
            {
                Console.WriteLine("Valor inválido. Digite um número inteiro.\n");
            }
        }
    }

    /// <summary>
    /// Calcula a média de uma sequência de valores.
    /// </summary>
    /// <param name="valores">Sequência de números (não nula e não vazia).</param>
    /// <returns>Média aritmética dos valores.</returns>
    /// <exception cref="ArgumentNullException">Se <paramref name="valores"/> for nulo.</exception>
    /// <exception cref="ArgumentException">Se a sequência estiver vazia.</exception>
    private static double CalcularMedia(IEnumerable<double> valores)
    {
        if (valores is null) throw new ArgumentNullException(nameof(valores));
        if (!valores.Any()) throw new ArgumentException("Sequência não pode ser vazia.", nameof(valores));
        return valores.Average();
    }

    /// <summary>
    /// Estima o ano de nascimento com base na idade atual e ano corrente.
    /// </summary>
    /// <param name="pessoa">Pessoa (não nula).</param>
    /// <param name="anoAtual">Ano atual (ex.: <c>DateTime.Today.Year</c>).</param>
    /// <returns>Ano de nascimento estimado.</returns>
    /// <exception cref="ArgumentNullException">Se <paramref name="pessoa"/> for nula.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Se <paramref name="anoAtual"/> for menor que 1900.</exception>
    private static int CalcularAnoNascimento(Pessoa pessoa, int anoAtual)
    {
        if (pessoa is null) throw new ArgumentNullException(nameof(pessoa));
        if (anoAtual < 1900) throw new ArgumentOutOfRangeException(nameof(anoAtual), "Ano atual inválido.");
        var idade = Math.Max(0, pessoa.Idade);
        return anoAtual - idade;
    }

    /// <summary>
    /// Cria um <see cref="StreamWriter"/> para o caminho indicado, garantindo que a pasta exista.
    /// </summary>
    /// <param name="caminho">Caminho do arquivo a ser criado/sobrescrito.</param>
    /// <returns>Instância de <see cref="StreamWriter"/> com AutoFlush habilitado.</returns>
    /// <exception cref="ArgumentException">Se caminho for nulo/vazio.</exception>
    private static StreamWriter CriarWriterSeguro(string caminho)
    {
        if (string.IsNullOrWhiteSpace(caminho))
            throw new ArgumentException("Caminho não pode ser vazio.", nameof(caminho));

        var pasta = Path.GetDirectoryName(Path.GetFullPath(caminho));
        if (!string.IsNullOrWhiteSpace(pasta) && !Directory.Exists(pasta))
        {
            Directory.CreateDirectory(pasta);
        }

        var stream = File.Open(caminho, FileMode.Create, FileAccess.Write, FileShare.Read);
        var writer = new StreamWriter(stream) { AutoFlush = true };
        return writer;
    }
}
