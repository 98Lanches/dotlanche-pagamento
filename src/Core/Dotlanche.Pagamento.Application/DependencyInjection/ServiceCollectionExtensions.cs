﻿using Dotlanche.Pagamento.Application.Factories;
using Dotlanche.Pagamento.Application.UseCases;
using Dotlanche.Pagamento.Application.UseCases.Interfaces;
using Dotlanche.Pagamento.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Dotlanche.Pagamento.Application.DependencyInjection
{
    [ExcludeFromCodeCoverage(Justification = "DI Class with no business logic")]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPagamentoUseCases(this IServiceCollection services)
        {
            services.AddScoped<ITipoPagamentoUseCaseFactory, TipoPagamentoUseCaseFactory>();
            services.AddKeyedScoped<ITipoPagamentoUseCase, PagamentoQrCodeUseCase>(TipoPagamento.QrCode);
            services.AddScoped<ISolicitarPagamentoPedidoUseCase, SolicitarPagamentoPedidoUseCase>();
            services.AddScoped<IConfirmQrCodePagamentoUseCase, ConfirmQrCodePagamentoUseCase>();
            services.AddScoped<IGetStatusPagamentoForPedidoUseCase, GetStatusPagamentoForPedidoUseCase>();

            return services;
        }
    }
}
