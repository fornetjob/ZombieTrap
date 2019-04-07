using System;

/// <summary>
/// Указывается у интерфейса для связывания его с сервисом
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class DependencyAttribute : Attribute
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dependencyType">Тип связи, реализующего данный интерфейс</param>
    public DependencyAttribute(Type dependencyType)
    {
        DependencyType = dependencyType;
    }

    public Type DependencyType;
}