using AutoMapper;
using Contas.Domain.Contas.Commands;
using Contas.Domain.Contas.Events;
using Contas.Domain.Contas.Repository;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Contas.Domain.Handlers;
using Contas.Domain.Usuarios.Commands;
using Contas.Domain.Usuarios.Events;
using Contas.Domain.Usuarios.Repository;
using Contas.Infrastructure.CrossCutting.AspNetFilters;
using Contas.Infrastructure.CrossCutting.Identity.Models;
using Contas.Infrastructure.CrossCutting.Identity.Services;
using Contas.Infrastructure.Data.Context;
using Contas.Infrastructure.Data.EventSourcing;
using Contas.Infrastructure.Data.Repository;
using Contas.Infrastructure.Data.Repository.EventSourcing;
using Contas.Infrastructure.Data.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Contas.Infrastructure.CrossCutting.IoC
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegistrarContaCommand>, ContaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarContaCommand>, ContaCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirContaCommand>, ContaCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarUsuarioCommand>, UsuarioCommandHandler>();

            // Domain - Contas
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<ContaRegistradaEvent>, ContaEventHandler>();
            services.AddScoped<INotificationHandler<ContaAtualizadaEvent>, ContaEventHandler>();
            services.AddScoped<INotificationHandler<ContaExcluidaEvent>, ContaEventHandler>();
            services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();

            // Infra - Data
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ContasContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUser, User>();

            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();
        }
    }
}