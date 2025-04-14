using System.Reflection;
using MediatR.Pipeline;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Attributes;
using Metsuoki.Shared.Interfaces;
using Metsuoki.Shared.Interfaces.Logger;
using Shared.Core.Models.Entities;

namespace Metsuoki.Application.MediatR.PostProcessors;

/// <summary>
/// Post-процессор MediatR, логирующий действия пользователя после выполнения запроса,
/// если запрос помечен атрибутом <see cref="LogUserActionAttribute" />.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
public class UserActionLogPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
{
    private readonly IMetsuokiDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly ILogActionService _logActionService;

    public UserActionLogPostProcessor(IMetsuokiDbContext dbContext, IUserService userService, ILogActionService actionDescriptionService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _logActionService = actionDescriptionService;
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        var attr = request.GetType().GetCustomAttribute<LogUserActionAttribute>();
        if (attr is null) return;

        var description = _logActionService.GetDescription();
        if (string.IsNullOrWhiteSpace(description)) return;

        var log = new UserActionLog
        {
            Timestamp = DateTime.Now,
            UserId = (Guid)_userService.UserId,
            UserFullName = _userService.UserName,
            ActionType = attr.ActionType,
            Description = description
        };

        _dbContext.UserActionLogs.Add(log);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
