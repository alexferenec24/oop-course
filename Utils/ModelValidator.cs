using System;
using System.Collections.Generic;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils
{
    public static class ModelValidator
    {
        public static ValidationResult Validate(object obj)
        {
            ValidationResult result = new ValidationResult();
            if (obj == null) return result;

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (var prop in properties)
            {
                object? value = prop.GetValue(obj);

                var required = prop.GetCustomAttribute<RequiredAttribute>();
                if (required != null)
                {
                    bool isEmpty = false;
                    if (value == null)
                    {
                        isEmpty = true;
                    }
                    else if (value is string str && string.IsNullOrWhiteSpace(str))
                    {
                        isEmpty = true;
                    }

                    if (isEmpty)
                    {
                        result.AddError($"{prop.Name}: {required.ErrorMessage}");
                    }
                }

                var maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLength != null && value is string s)
                {
                    if (s.Length > maxLength.Length)
                    {
                        result.AddError($"{prop.Name}: {maxLength.ErrorMessage}");
                    }
                }

                var minValue = prop.GetCustomAttribute<MinValueAttribute>();
                if (minValue != null && value != null && IsNumeric(value))
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (doubleValue < minValue.Min)
                    {
                        result.AddError($"{prop.Name}: {minValue.ErrorMessage}");
                    }
                }
            }

            return result;
        }

        public static void PrintInfo(Type type)
        {
            Console.WriteLine($"Тип: {type.Name}");
            PropertyInfo[] properties = type.GetProperties();
            
            foreach (var prop in properties)
            {
                Console.WriteLine($"  Властивість: {prop.Name} ({prop.PropertyType.Name})");
                var attributes = prop.GetCustomAttributes();
                foreach (var attr in attributes)
                {
                    Console.WriteLine($"    Атрибут: {attr.GetType().Name}");
                }
            }
        }

        private static bool IsNumeric(object value)
        {
            return value is int || value is double || value is float || value is decimal || value is long || value is short || value is byte;
        }
    }
}