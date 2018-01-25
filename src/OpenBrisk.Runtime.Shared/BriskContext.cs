namespace OpenBrisk.Runtime.Shared
{
	public class BriskContext : IBriskContext
	{
		public BriskContext() : this(null)
		{
		}

		public BriskContext(string data)
		{
			this.Data = data;
		}

		public string Data { get; }
	}
}