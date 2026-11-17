using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils
{
    public static class FormBuilder
    {
        public static T Build<T>() where T : new()
        {
            T obj = new T();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo prop = properties[i];
                
                if (prop.GetSetMethod() == null) continue;

                string prompt = prop.Name;
                var required = prop.GetCustomAttribute<RequiredAttribute>();
                var maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
                var minValue = prop.GetCustomAttribute<MinValueAttribute>();

                if (required != null) prompt += " (обов'язкове";
                if (maxLength != null) prompt += required != null ? $", макс. {maxLength.Length}" : $" (макс. {maxLength.Length}";
                if (minValue != null) prompt += (required != null || maxLength != null) ? $", мін. {minValue.Min}" : $" (мін. {minValue.Min}";

                if (required != null || maxLength != null || minValue != null)
                {
                    prompt += ")";
                }

                while (true)
                {
                    Console.Write($"{prompt}: ");
                    string? input = Console.ReadLine();
                    
                    try
                    {
                        object? converted = null;
                        if (prop.PropertyType == typeof(string))
                        {
                            converted = input ?? "";
                        }
                        else
                        {
                            converted = Convert.ChangeType(input, prop.PropertyType);
                        }
                        
                        prop.SetValue(obj, converted);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Некоректне значення, спробуйте ще раз.");
                    }
                }
            }

            return obj;
        }
    }
}