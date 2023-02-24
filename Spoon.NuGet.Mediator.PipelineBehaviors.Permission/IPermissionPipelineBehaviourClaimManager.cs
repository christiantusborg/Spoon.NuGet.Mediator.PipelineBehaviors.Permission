namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission
{
    /// <summary>
    ///     Interface IPermissionClaimManager.
    /// </summary>
    public interface IPermissionPipelineBehaviourClaimManager
    {
        /// <summary>
        ///     Determines whether [has required claim] [the specified required claims].
        /// </summary>
        /// <param name="requiredClaims">The required claims.</param>
        /// <returns><c>true</c> if [has required claim] [the specified required claims]; otherwise, <c>false</c>.</returns>
        bool HasRequiredClaim(List<string> requiredClaims);
    }
}