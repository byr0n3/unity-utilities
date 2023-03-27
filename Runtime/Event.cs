using System.Collections.Generic;
using JetBrains.Annotations;

namespace Byrone.Utilities
{
	// @todo Listener struct

	/// <summary>
	/// Custom event class
	/// </summary>
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public sealed class Event
	{
		private readonly List<System.Action> listeners;

		/// <inheritdoc cref="Event"/>
		public Event()
		{
			this.listeners = new List<System.Action>();
		}

		/// <summary>
		/// Call all the event listeners
		/// </summary>
		public void Invoke()
		{
			for (var i = this.listeners.Count - 1; i >= 0; i--)
			{
				try
				{
					this.listeners[i].Invoke();
				}
				catch (System.Exception ex)
				{
#if DEBUG
					UnityEngine.Debug.LogError(ex);
#endif
				}
			}
		}

		/// <summary>
		/// Subscribe to this event
		/// </summary>
		/// <param name="callback">Callback to trigger when the event gets invoked</param>
		public void Subscribe(System.Action callback) =>
			this.listeners.Add(callback);

		/// <summary>
		/// Subscribe to this event
		/// </summary>
		/// <param name="callback">Callback to trigger when the event gets invoked</param>
		/// <returns>Unsubscribe function</returns>
		[Pure]
		public System.Action Subscribe2(System.Action callback)
		{
			this.listeners.Add(callback);

			return () => this.Unsubscribe(this.listeners.Count - 1);
		}

		/// <summary>
		/// Unsubscribe from the event
		/// </summary>
		/// <param name="callback">Callback to unsubscribe</param>
		public void Unsubscribe(System.Action callback) =>
			this.listeners.Remove(callback);

		/// <summary>
		/// Remove the event listener at a given index
		/// </summary>
		/// <param name="index">List index to remove</param>
		public void Unsubscribe(int index) =>
			this.listeners.RemoveAt(index);
	}

	/// <summary>
	/// Custom event class with a parameter
	/// </summary>
	/// <typeparam name="T">Type of the parameter</typeparam>
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public sealed class Event<T>
	{
		private readonly List<System.Action<T>> listeners;

		/// <inheritdoc cref="Event{T}"/>
		public Event()
		{
			this.listeners = new List<System.Action<T>>();
		}

		/// <summary>
		/// Call all the event listeners
		/// </summary>
		/// <param name="value">Argument to give to the listeners</param>
		public void Invoke(T value)
		{
			for (var i = this.listeners.Count - 1; i >= 0; i--)
			{
				try
				{
					this.listeners[i].Invoke(value);
				}
				catch (System.Exception ex)
				{
#if DEBUG
					UnityEngine.Debug.LogError(ex);
#endif
				}
			}
		}

		/// <summary>
		/// Subscribe to this event
		/// </summary>
		/// <param name="callback">Callback to trigger when the event gets invoked</param>
		public void Subscribe(System.Action<T> callback) =>
			this.listeners.Add(callback);

		/// <summary>
		/// Subscribe to this event
		/// </summary>
		/// <param name="callback">Callback to trigger when the event gets invoked</param>
		/// <returns>Unsubscribe function</returns>
		[Pure]
		public System.Action Subscribe2(System.Action<T> callback)
		{
			this.listeners.Add(callback);

			return () => this.Unsubscribe(this.listeners.Count - 1);
		}

		/// <summary>
		/// Unsubscribe from the event
		/// </summary>
		/// <param name="callback">Callback to unsubscribe</param>
		public void Unsubscribe(System.Action<T> callback) =>
			this.listeners.Remove(callback);

		/// <summary>
		/// Remove the event listener at a given index
		/// </summary>
		/// <param name="index">List index to remove</param>
		public void Unsubscribe(int index) =>
			this.listeners.RemoveAt(index);
	}
}
