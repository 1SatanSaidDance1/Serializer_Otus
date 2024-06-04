using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SerializerReflections.Classes
{
    public class MyCSVSerializer(string? columnsSeparator, string? rowsSeparator)
    {
        private readonly string? _columnsSeparator = columnsSeparator;
        private readonly string? _rowsSeparator = rowsSeparator;

        #region Seriailize
        public string Serialize<T>(T obj)
        {
            StringBuilder result = new();
            StringBuilder header = new();
            StringBuilder data = new();

            PropertyInfo[] fields = obj.GetType().GetProperties(
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public
                | BindingFlags.Static
                );

            foreach (var field in fields)
            {
                string name = field.Name;
                object? value = field.GetValue(obj);

                if (header.Length > 0)
                {
                    header.Append(_columnsSeparator);
                }

                header.Append(name);

                if (data.Length > 0)
                {
                    data.Append(_columnsSeparator);
                }
                if (value != null)
                {
                    data.Append(value);
                }else
                {
                    data.Append("");
                }
            }

            result.Append(header);

            if (result.Length > 0)
            {
                result.Append(_rowsSeparator);
            }

            result.Append(data);

            return result.ToString();
        }
        #endregion


        #region DeSerializer

        public T Deserialiser<T>(string data) where T : new() 
        {
            T obj = new();

            string[] dataObject = data.Split(_rowsSeparator, StringSplitOptions.TrimEntries);
            string[] columnNames = dataObject[0].Split(_columnsSeparator, StringSplitOptions.TrimEntries);
            string[] columnValues = dataObject[1].Split(_columnsSeparator, StringSplitOptions.TrimEntries);

            for (int i = 0; i < columnNames.Length; i++)
            {
                PropertyInfo? field = dataObject.GetType()
                    .GetProperty(columnNames[i],
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Static
                    );

                if ( field != null )
                {
                    string? value = columnValues[i];
                    string? column = columnNames[i];
                    TypeConverter? converter = TypeDescriptor.GetConverter(field.PropertyType);
                    Object? convertedvalue = converter.ConvertFrom(value);
                    field.SetValue(obj, convertedvalue);
                }
            }

            return obj;
        }


        #endregion
     
    }

}