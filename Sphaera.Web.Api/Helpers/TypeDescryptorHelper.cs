using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace Sphaera.Web.Api.Helpers
{
	public static class TypeDescryptorHelper
	{
		[NotNull]
		public static T Copy<T>(this T source) where T: class, new()
		{
			var res = new T();
			if (ReferenceEquals(source, null))
				return res;

			var sourceProps = TypeDescriptor.GetProperties(source);
			var destProps = TypeDescriptor.GetProperties(res);

			foreach (PropertyDescriptor sourceProp in sourceProps)
			{
				if (sourceProp.IsReadOnly)
					continue;

				var data = sourceProp.GetValue(source);
				if (data != null)
					destProps[sourceProp.Name].SetValue(res, data);
			}

			return res;
		}

		[NotNull]
		public static T CopyTo<T>(this T source, [NotNull] T res, [CanBeNull] string[] fields = null) where T : class
		{
			if (ReferenceEquals(source, null))
				return res;

			var sourceProps = TypeDescriptor.GetProperties(source);
			var destProps = TypeDescriptor.GetProperties(res);

			foreach (PropertyDescriptor sourceProp in sourceProps)
			{
				if (sourceProp.IsReadOnly || (fields != null && !fields.Contains(sourceProp.Name)))
					continue;

				var data = sourceProp.GetValue(source);
				if (data != null)
					destProps[sourceProp.Name].SetValue(res, data);
			}

			return res;
		}

		[CanBeNull]
		public static dynamic GetPropertyValue(this object instance, [NotNull] string name)
		{
			var properties = TypeDescriptor.GetProperties(instance);
			PropertyDescriptor property;

			if (name.Contains("."))
			{
				var names = name.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
				var propertyCollection = properties;
				var value = instance;

				foreach (var propName in names)
				{
					property = propertyCollection[propName];
					if (property == null || value == null)
						return null;

					value = property.GetValue(value);

					propertyCollection = property.GetChildProperties();
				}

				return value;
			}

			property = properties[name];
            return property?.GetValue(instance);
        }
    }
}