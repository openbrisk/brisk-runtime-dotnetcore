namespace OpenBrisk.Runtime.Core.Models
{
	using System.Collections.Generic;
	using System.Dynamic;

	public sealed class BriskContext : DynamicObject
	{
		public BriskContext()
		{
			this.Headers = new Dictionary<string, string>();
			this.Environment = new Dictionary<string, string>();
			this.Settings = new Dictionary<string, string>();
		}

		public dynamic Data { get; set; }

		public IDictionary<string, string> Headers { get; set; }

		public IDictionary<string, string> Environment { get; set; }

		public IDictionary<string, string> Settings { get; set; }
	}
}