﻿using FluentValidation;
using FluentValidation.Results;

namespace ComandaPro.Domain.Interfaces.Services;

public interface IBaseService
{
    ValidationResult Validate<T>(T obj, AbstractValidator<T> validator);
}
