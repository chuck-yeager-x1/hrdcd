﻿namespace HRDCD.Common.DataModel.Entity;

/// <summary>
/// Класс для наследования базовых полей сущности, хранимой в БД.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Возвращает или задает первичный ключ сущности.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Возвращает или задает дату создания сущности.
    /// </summary>
    public DateTime InsertDate { get; set; }

    /// <summary>
    /// Возвращает или задает дату последнего изменения сущности.
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// Возвращает или задает дату удаления сущности.
    /// </summary>
    public DateTime? DeleteDate { get; set; }

    /// <summary>
    /// Возвращает или задает признак удаления сущности.
    /// </summary>
    public bool IsDeleted { get; set; }
}