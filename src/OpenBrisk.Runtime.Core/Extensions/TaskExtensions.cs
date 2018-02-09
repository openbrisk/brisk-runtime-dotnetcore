namespace OpenBrisk.Runtime.Core.Extensions
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;

	public static class TaskExtensions
	{
		public static async Task<object> TimeoutAfter(this Task task, TimeSpan timeout)
		{
			using (CancellationTokenSource cts = new CancellationTokenSource())
			{
				Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token));
				if (completedTask == task)
				{
					cts.Cancel();
					return await (dynamic)task; // Very important in order to propagate exceptions.
				}
				else
				{
					throw new TimeoutException("The function has timed out.");
				}
			}
		}
	}
}
