﻿namespace ComandaPro.Domain.Interfaces.Entities;

public interface IEntity<T>
{
    T Id { get; set; }
}
