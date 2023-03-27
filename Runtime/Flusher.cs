using JetBrains.Annotations;

namespace Byrone.Utilities
{
	/// <summary>
	/// Utility class for subscribing to- and unsubscribing from multiple events
	/// </summary>
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public sealed class Flusher : System.IDisposable
	{
		private readonly System.Action[] callbacks;

		/// <inheritdoc cref="Flusher"/>
		public Flusher(params System.Action[] callbacks)
		{
			this.callbacks = callbacks;
		}

		/// <summary>
		/// Invoke all callbacks in this instance
		/// </summary>
		public void Flush()
		{
			foreach (var callback in this.callbacks)
			{
				callback.Invoke();
			}
		}

		public void Dispose() =>
			this.Flush();
	}
}
