using Metsuoki.Shared.Enums;

namespace Metsuoki.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class LogUserActionAttribute : Attribute
{
    public UserActionType ActionType { get; }

    public LogUserActionAttribute(UserActionType actionType)
    {
        ActionType = actionType;
    }
}