using Microsoft.EntityFrameworkCore;
using Batch.TransacaoFinanceira.data.database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Batch.TransacaoFinanceira.data.repositories;
using Batch.TransacaoFinanceira.data.repositories.interfaces;
using Batch.TransacaoFinanceira.services.interfaces;
using Batch.TransacaoFinanceira.services;
using Batch.TransacaoFinanceira.domain.models;
using Batch.TransacaoFinanceira.domain.mappers;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("ContasDb"));

        services.AddScoped<IContaRepository, ContaRepository>();
        services.AddScoped<IContaService, ContaService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<TransacaoMapper>();

        var loggerFactory = LoggerFactory.Create(static builder =>
        {
            builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
        });

        ILogger logger = loggerFactory.CreateLogger<Program>();

        var serviceProvider = services.BuildServiceProvider();

        var contaService = serviceProvider.GetRequiredService<IContaService>();

        //Cadadastrando Contas para o processamento das transações
        ICollection<Conta> contas =
        [
            new Conta(938485762, 180),
            new Conta(347586970, 1200),
            new Conta(2147483649, 0),
            new Conta(675869708, 4900),
            new Conta(238596054, 478),
            new Conta(573659065, 787),
            new Conta(210385733, 10),
            new Conta(674038564, 400),
            new Conta(563856300, 1200)
        ];
        await contaService.CadastrarConta(contas);


        // Iniciando o processando o arquivo de transações através de um arquivo JSON
        var transacaoService = serviceProvider.GetRequiredService<ITransacaoService>();
        string caminhoArquivo = "data/storage/transacoes/2023-09-09/transacoes.json";
        await transacaoService.ProcessarTransacoes(caminhoArquivo);
    }
}




