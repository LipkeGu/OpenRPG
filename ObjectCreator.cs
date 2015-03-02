using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace OpenRPG
{
	/// <summary>Used to create instances of *Info types from a string. `Foo` -> instance of FooInfo.</summary>
	public class ObjectCreator
	{
		readonly string[] namespaces;

		public ObjectCreator()
		{
			namespaces = Assembly.GetExecutingAssembly().GetNamespaces().ToArray();
		}

		/// <summary>All *Info classes must derive from a single type (usually an interface) for the use of generics.</summary>
		public T CreateObject<T>(string infoClassName)
		{
			var type = GetType(infoClassName);

			if (type == null)
				throw new Exception("Couldn't locate type `{0}` to make `{1}`!".F(infoClassName, infoClassName.ReverseSubstring(4)));

			var ctor = GetCtor(type);

			// We can use this Invoke because Info types do not supply a ctor
			// (other than the empty default)

			return (T)ctor.Invoke(new object[0]);
		}

		public Type GetType(string className)
		{
			return namespaces.Select(x => Type.GetType(x + "." + className)).FirstOrDefault(x => x != null);
		}

		public ConstructorInfo GetCtor(Type type)
		{
			var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

			// Grab the first ctor because.. (refer to above comment about Invoke)

			return type.GetConstructors(flags)[0];
		}
	}
}
