﻿using Core.Entities.Exceptions.Abstractions;

namespace Core.Entities.Exceptions;

internal class OrderPropertyException : BaseEntityException<Order>
{
    public OrderPropertyException(string propertyName) : base(propertyName)
    {
    }

    internal static string ThrowIfNullOrEmpty(string? value, string propertyName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new OrderPropertyException(propertyName);
        }
        return value;
    }

    internal static decimal ThrowIfZeroOrNegative(decimal value, string propertyName)
    {
        if (value < 0.00M)
        {
            throw new OrderPropertyException(propertyName);
        }

        return value;
    }
}