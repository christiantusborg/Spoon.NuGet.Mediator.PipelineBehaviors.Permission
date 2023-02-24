namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission
{
    using Assistants;
    using EitherCore.Enums;
    using MediatR;

    /// <summary>
    ///     Class PermissionBehaviour. This class cannot be inherited.
    ///     Implements the <see cref="IPipelineBehavior{TRequest,TResponse}" />.
    /// </summary>
    /// <typeparam name="TRequest">The type of the t request.</typeparam>
    /// <typeparam name="TResponse">The type of the t response.</typeparam>
    /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
    public sealed class PermissionPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        ///     The claim manager.
        /// </summary>
        private readonly IPermissionPipelineBehaviourClaimManager _claimManager;

        private readonly IPermissionPipelineBehaviourAssistant _permissionPipelineAssistant;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PermissionPipelineBehaviour{TRequest,TResponse}" /> class.
        /// </summary>
        /// <param name="claimManager">The claim manager.</param>
        /// <param name="permissionPipelineAssistant"></param>
        public PermissionPipelineBehaviour(IPermissionPipelineBehaviourClaimManager claimManager, IPermissionPipelineBehaviourAssistant permissionPipelineAssistant)
        {
            this._claimManager = claimManager;
            this._permissionPipelineAssistant = permissionPipelineAssistant;
        }

        /// <summary>
        ///     Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
        /// </summary>
        /// <param name="request">Incoming request</param>
        /// <param name="next">
        ///     Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the
        ///     handler.
        /// </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Awaitable task returning the <typeparamref name="TResponse" /></returns>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var pipelineBehaviorPermission = this._permissionPipelineAssistant.HasPipelineBehaviorPermission(request);

            if (!this._permissionPipelineAssistant.HasPipelineBehaviorPermission(request))
            {
                return await next();
            }

            if (this._permissionPipelineAssistant.HasPermissionExcludeAttribute(request))
            {
                return await next();
            }

            var requiredClaims = this._permissionPipelineAssistant.GetRequiredClaims(request);

            if (!this._permissionPipelineAssistant.HasAnyRequiredClaims(requiredClaims.Count))
            {
                return await next();
            }

            if (this._claimManager.HasRequiredClaim(requiredClaims))
            {
                return await next();
            }

            if (!this._permissionPipelineAssistant.IsNotEither<TResponse>(out var responseType))
            {
                throw new UnauthorizedAccessException(typeof(TResponse).Name);
            }

            var ext = this._permissionPipelineAssistant.CreateEitherException(request, "PermissionPipelineBehaviour", "HasRequiredClaim_Failed", BaseHttpStatusCodes.Status403Forbidden);

            var response = this._permissionPipelineAssistant.CreateResponse<TResponse>(responseType, ext);

            return response;
        }
    }
}