using System;
using System.Text.RegularExpressions;

namespace Messaging.Domain.ValueObjects
{
    public class Email : Recipient, Sender
    {
        private readonly string _value;

        public string Address => _value;

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email address cannot be empty", nameof(value));
            }

            if (!IsValid(value))
            {
                throw new ArgumentException("Email address is invalid", nameof(value));
            }

            _value = value;
        }

        public static bool IsValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Email other)
            {
                return _value.Equals(other._value, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
