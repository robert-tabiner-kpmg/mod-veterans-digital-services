using Forms.Core.EffectHandlers;
using Forms.Core.EffectHandlers.Handlers;
using Forms.Core.EffectHandlers.Interfaces;
using Forms.Core.NodeHandlers.DecisionNodeHandlers;
using Forms.Core.NodeHandlers.DecisionNodeHandlers.Handlers;
using Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Handlers;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces;
using Forms.Core.Options;
using Forms.Core.Repositories;
using Forms.Core.Repositories.Interfaces;
using Forms.Core.Services;
using Forms.Core.Services.Interfaces;
using Forms.Core.SubmissionHandlers;
using Forms.Core.SubmissionHandlers.Handlers;
using Forms.Core.SubmissionHandlers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forms.Web
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddFormServices(services);
            AddDecisionFormHandlers(services);
            AddPhysicalFormHandlers(services);
            AddEffectHandlers(services);
            AddRepositories(services);
            AddSubmissionHandlers(services);
            AddEmailServices(services, configuration);
        }

        private static void AddFormServices(IServiceCollection services)
        {
            services.AddScoped<ISummaryService, SummaryService>();
            services.AddScoped<IReferenceNumberService, ReferenceNumberService>();
            services.AddScoped<IFormService, FormService>();
            services.AddSingleton<IStaticFormProvider, StaticFormProvider>();
        }

        private static void AddDecisionFormHandlers(IServiceCollection services)
        {
            services.AddScoped<IDecisionNodeHandlerStrategy, DecisionNodeHandlerStrategy>();
            services.AddScoped<IDecisionNodeHandler, FormRouterHandler>();
            services.AddScoped<IDecisionNodeHandler, TaskRouterHandler>();
            services.AddScoped<IDecisionNodeHandler, TaskItemRouterHandler>();
            services.AddScoped<IDecisionNodeHandler, PreTaskGhostHandler>();
            services.AddScoped<IDecisionNodeHandler, PostTaskGhostHandler>();
            services.AddScoped<IDecisionNodeHandler, TaskSummaryGhostHandler>();
            services.AddScoped<IDecisionNodeHandler, TaskQuestionGhostHandler>();
            services.AddScoped<IDecisionNodeHandler, SubTaskRouterHandler>();
            services.AddScoped<IDecisionNodeHandler, SubTaskItemRouterHandler>();
            services.AddScoped<IDecisionNodeHandler, TaskListGhostHandler>();
        }

        private static void AddPhysicalFormHandlers(IServiceCollection services)
        {
            services.AddScoped<IPhysicalNodeHandlerStrategy, PhysicalNodeHandlerStrategy>();
            services.AddScoped<IPhysicalNodeHandler, TaskListHandler>();
            services.AddScoped<IPhysicalNodeHandler, TaskQuestionPageHandler>();
            services.AddScoped<IPhysicalNodeHandler, TaskSummaryHandler>();
            services.AddScoped<IPhysicalNodeHandler, PostTaskHandler>();
            services.AddScoped<IPhysicalNodeHandler, PreTaskHandler>();
            services.AddScoped<IPhysicalNodeHandler, PreSubTaskHandler>();
            services.AddScoped<IPhysicalNodeHandler, PostSubTaskHandler>();
        }

        private static void AddEffectHandlers(IServiceCollection services)
        {
            services.AddScoped<IEffectHandlerStrategy, EffectHandlerStrategy>();
            services.AddScoped<IEffectHandler, PathChangeEffectHandler>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IFormRepository, FormRepository>();
        }

        private static void AddSubmissionHandlers(IServiceCollection services)
        {
            services.AddScoped<ISubmissionHandlerStrategy, SubmissionHandlerStrategy>();
            services.AddScoped<ISubmissionHandler, AfcsSubmissionHandler>();
            services.AddScoped<ISubmissionHandler, AfipSubmissionHandler>();
            services.AddScoped<ISubmissionHandler, TestSubmissionHandler>();
        }
        private static void AddEmailServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailTemplateOptions>(configuration.GetSection("Email"));
            services.AddScoped<IEmailService, EmailService>();
        }
        
    }
}