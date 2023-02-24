namespace Spoon.NuGet.Mediator.PipelineBehaviors.Permission
{
    public class PermissionFailed<T>
    {
        public string Command { get; set; }
        public T CommandValues { get; set; } 
        public string Origin { get; set; }
        public int HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}