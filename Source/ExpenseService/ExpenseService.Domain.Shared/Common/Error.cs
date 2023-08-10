using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Shared.Common;

public class Error : IEquatable<Error>
{
	public string Code { get; }
	public string Message { get; }
	
	public static readonly Error None = new (string.Empty, string.Empty);
	public static readonly Error NullValue = new (nameof(NullValue), "Value cannot be null.");
	
	public Error(string code, string message)
	{
		Code = code;
		Message = message;
	}
	
	public static Error FromException(Exception exception)
	{
		return new Error(exception.GetType().Name, exception.Message);
	}

    public bool Equals(Error other) => other is not null && Code == other.Code && Message == other.Message;

    public static implicit operator string(Error error) => error.Code;
	
	public static bool operator ==(Error left, Error right) 
	{
		if (left is null && right is null)
		{
			return true;
		}
		
		if (left is null || right is null)
		{
			return false;
		}
		
		return left.Equals(right);
	}
	
	public static bool operator !=(Error left, Error right) => !(left == right);
}
