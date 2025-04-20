namespace Metsuoki.Shared.Interfaces.Logger;

/// <summary>
/// Сервис для установки и получения описания действия логирования.
/// </summary>
public interface ILogActionService
{
    /// <summary>
    /// Устанавливает описание действия.
    /// </summary>
    /// <param name="description">Описание действия.</param>
    void SetDescription(string description);

    /// <summary>
    /// Возвращает текущее описание действия.
    /// </summary>
    /// <returns>Описание действия.</returns>
    string GetDescription();
}

/// <summary>
/// Реализация сервиса логирования действия.
/// Позволяет установить и получить описание действия, связанного с логированием.
/// </summary>
public class LogActionService : ILogActionService
{
    private string _description;

    public void SetDescription(string description)
    {
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }
}