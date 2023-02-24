namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Class PermissionPipelineBehaviourExcludeAttribute. This class cannot be inherited.
    ///     Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PermissionPipelineBehaviourExcludeAttribute : Attribute
    {
        /// <summary>
        ///     The explanation
        /// </summary>
        private readonly string _explanation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PermissionPipelineBehaviourExcludeAttribute" /> class.
        /// </summary>
        /// <param name="explanation">The explanation.</param>
        public PermissionPipelineBehaviourExcludeAttribute([Required] string explanation)
        {
            this._explanation = explanation;
        }
    }
}