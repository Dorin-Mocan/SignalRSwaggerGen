using SignalRSwaggerGen.Enums;
using SignalRSwaggerGen.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRSwaggerGen.Attributes
{
	/// <summary>
	/// Use this attribute to enable Swagger documentation for hubs
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SignalRHubAttribute : Attribute
	{
		public string Path { get; }
		public AutoDiscover AutoDiscover { get; }
		public IEnumerable<string> DocumentNames { get; }
		public LowerCamelCase LowerCamelCase { get; }

		/// <param name="path">Relative path of the hub.
		/// If path contains "[Hub]", this part will be replaced with the name of the type holding this attribute.</param>
		/// <param name="autoDiscover">A flag indicating what components will have Swagger documentation enabled automatically</param>
		/// <param name="documentNames">An array of names of the Swagger documents the hub will be displayed in. If null or empty array specified, then the hub will be displayed in all documents.</param>
		/// <param name="lowerCamelCase">Defines rules for conversion to lower camel case.</param>
		/// <exception cref="ArgumentException">Thrown if path is null or empty, or auto-discover value not allowed for this attribute</exception>
		public SignalRHubAttribute(string path = Constants.DefaultHubPath, AutoDiscover autoDiscover = AutoDiscover.None, string[] documentNames = null, LowerCamelCase lowerCamelCase = LowerCamelCase.Default)
		{
			if (path.IsNullOrEmpty()) throw new ArgumentException("Path is null or empty", nameof(path));
			if (!_validAutoDiscoverValues.Contains(autoDiscover)) throw new ArgumentException($"Value {autoDiscover} not allowed for this attribute", nameof(autoDiscover));
			Path = path;
			AutoDiscover = autoDiscover;
			DocumentNames = documentNames?.ToList().Distinct() ?? Enumerable.Empty<string>();
			LowerCamelCase = lowerCamelCase;
		}

		private static readonly IEnumerable<AutoDiscover> _validAutoDiscoverValues = new List<AutoDiscover>
		{
			AutoDiscover.None,
			AutoDiscover.Methods,
			AutoDiscover.MethodsAndArgs
		};
	}
}
