namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission
{
    using Assistants;
    using Interceptors.LogInterceptor;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Class ServiceCollectionExtensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds MediatR Permission pipeline behaviour.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddPermissionPipelineBehaviour(this IServiceCollection services)
        {
            services.AddInterceptedSingleton<IPermissionPipelineBehaviourAssistant, PermissionPipelineBehaviourAssistant, LogInterceptorDefault>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PermissionPipelineBehaviour<,>));
            return services;
        }

        /// <summary>
        ///     Adds MediatR Permission pipeline behaviour ClaimManager always true default.
        ///     NOTE: When used everyone is allowed use all command.
        ///     Suggestion: Make your own instead.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddPermissionPipelineBehaviourClaimManagerAlwaysTrueDefault(this IServiceCollection services)
        {
            services.AddInterceptedSingleton<IPermissionPipelineBehaviourClaimManager, PermissionPipelineBehaviourClaimAlwaysTrueDefaultManager, LogInterceptorDefault>();

            return services;
        }
    }
}