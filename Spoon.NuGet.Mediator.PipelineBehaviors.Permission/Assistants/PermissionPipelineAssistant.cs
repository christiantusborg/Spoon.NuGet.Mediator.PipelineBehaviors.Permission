namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission.Assistants
{
    using EitherCore;
    using EitherCore.Enums;
    using EitherCore.Exceptions;
    using Interceptors.LogInterceptor;
    using Interfaces.Permission;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class PermissionPipelineBehaviourAssistant.
    ///     Implements the
    ///     <see cref="IPermissionPipelineBehaviourAssistant" />
    /// </summary>
    /// <seealso cref="IPermissionPipelineBehaviourAssistant" />
    [LogInterceptorDefaultLogLevel(LogLevel.Debug)]
    public class PermissionPipelineBehaviourAssistant : IPermissionPipelineBehaviourAssistant
    {
        /// <summary>
        ///     Determines whether [has pipeline behavior permission] [the specified request].
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if [has pipeline behavior permission] [the specified request]; otherwise, <c>false</c>.</returns>
        public bool HasPipelineBehaviorPermission<TRequest>(TRequest request)
        {
            return request is IPipelineBehaviorPermission;
        }

        /// <summary>
        ///     Determines whether [has permission exclude attribute] [the specified request].
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if [has permission exclude attribute] [the specified request]; otherwise, <c>false</c>.</returns>
        public bool HasPermissionExcludeAttribute<TRequest>(TRequest request)
        {
            return typeof(TRequest).IsDefined(typeof(PermissionPipelineBehaviourExcludeAttribute), true);
        }

        /// <summary>
        ///     Gets the required claims.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetRequiredClaims<TRequest>(TRequest request)
        {
            var pipelineBehaviorPermission = request as IPipelineBehaviorPermission;
            var requiredClaims = pipelineBehaviorPermission?.GetRequiredClaims().Select(x => x.Value).ToList() ?? new List<string>();

            return requiredClaims;
        }

        /// <summary>
        ///     Determines whether [has any required claims] [the specified required claims count].
        /// </summary>
        /// <param name="requiredClaimsCount">The required claims count.</param>
        /// <returns><c>true</c> if [has any required claims] [the specified required claims count]; otherwise, <c>false</c>.</returns>
        public bool HasAnyRequiredClaims(int requiredClaimsCount)
        {
            return requiredClaimsCount != 0;
        }

        /// <summary>
        ///     Determines whether [is not either] [the specified type either].
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="typeEither">The type either.</param>
        /// <returns><c>true</c> if [is not either] [the specified type either]; otherwise, <c>false</c>.</returns>
        public bool IsNotEither<TResponse>(out Type? typeEither)
        {
            var responseType = typeof(TResponse);

            if (responseType.GetGenericTypeDefinition() != typeof(Either<>))
            {
                typeEither = null;
                return false;
            }

            typeEither = responseType;
            return true;
        }

        /// <summary>
        ///     Creates the either exception.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="message">The message.</param>
        /// <param name="httpStatusCodes">The HTTP status codes.</param>
        /// <returns>EitherException.</returns>
        public EitherException CreateEitherException<TRequest>(TRequest request, string origin, string message, BaseHttpStatusCodes httpStatusCodes)
        {
            var ext = new EitherException(request!, origin, message, httpStatusCodes);
            return ext;
        }

        /// <summary>
        ///     Creates the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="responseType">Type of the response.</param>
        /// <param name="ext">The ext.</param>
        /// <returns>TResponse.</returns>
        public TResponse CreateResponse<TResponse>(Type? responseType, EitherException ext)
        {
            var response = (TResponse)Activator.CreateInstance(responseType!, ext) !;
            return response;
        }
    }
}