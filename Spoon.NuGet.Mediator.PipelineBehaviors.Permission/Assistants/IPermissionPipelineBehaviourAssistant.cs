namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission.Assistants
{
    using EitherCore.Enums;
    using EitherCore.Exceptions;

    /// <summary>
    ///     Interface IPermissionPipelineBehaviourAssistant
    ///     Used in PermissionPipelineBehaviour.
    /// </summary>
    public interface IPermissionPipelineBehaviourAssistant
    {
        /// <summary>
        ///     Determines whether [has pipeline behavior permission] [the specified request].
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if [has pipeline behavior permission] [the specified request]; otherwise, <c>false</c>.</returns>
        bool HasPipelineBehaviorPermission<TRequest>(TRequest request);

        /// <summary>
        ///     Determines whether [has permission exclude attribute] [the specified request].
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if [has permission exclude attribute] [the specified request]; otherwise, <c>false</c>.</returns>
        bool HasPermissionExcludeAttribute<TRequest>(TRequest request);

        /// <summary>
        ///     Gets the required claims.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetRequiredClaims<TRequest>(TRequest request);

        /// <summary>
        ///     Determines whether [has any required claims] [the specified required claims count].
        /// </summary>
        /// <param name="requiredClaimsCount">The required claims count.</param>
        /// <returns><c>true</c> if [has any required claims] [the specified required claims count]; otherwise, <c>false</c>.</returns>
        bool HasAnyRequiredClaims(int requiredClaimsCount);

        /// <summary>
        ///     Determines whether [is not either] [the specified type either].
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="typeEither">The type either.</param>
        /// <returns><c>true</c> if [is not either] [the specified type either]; otherwise, <c>false</c>.</returns>
        public bool IsNotEither<TResponse>(out Type? typeEither);

        /// <summary>
        ///     Creates the either exception.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="message">The message.</param>
        /// <param name="httpStatusCodes">The HTTP status codes.</param>
        EitherException CreateEitherException<TRequest>(TRequest request, string origin, string message, BaseHttpStatusCodes httpStatusCodes);

        /// <summary>
        ///     Creates the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="responseType">Type of the response.</param>
        /// <param name="ext">The ext.</param>
        /// <returns>TResponse.</returns>
        TResponse CreateResponse<TResponse>(Type? responseType, EitherException ext);
    }
}