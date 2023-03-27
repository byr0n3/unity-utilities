using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;

namespace Byrone.Utilities
{
	/// <summary>
	/// Pool that automatically re-queues enqueued items
	/// </summary>
	/// <typeparam name="T">Type of the items in the pool</typeparam>
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public class RecyclePool<T>
	{
		public int poolSize =>
			this.Items.Count;

		// @todo Custom queue
		protected readonly Queue<T> Items;

		private readonly int poolCapacity;

		/// <summary>
		/// Create an empty <see cref="RecyclePool{T}"/> 
		/// </summary>
		/// <param name="maxPoolSize">Maximum amount of items for in the pool</param>
		protected RecyclePool(int maxPoolSize)
		{
			this.poolCapacity = maxPoolSize;

			this.Items = new Queue<T>(maxPoolSize);
		}

		/// <summary>
		/// Create a prefilled <see cref="RecyclePool{T}"/> 
		/// </summary>
		/// <param name="maxPoolSize">Maximum amount of items for in the pool</param>
		/// <param name="factory">Function that generates an item for in the pool</param>
		public RecyclePool(int maxPoolSize, System.Func<T> factory) : this(maxPoolSize)
		{
			for (var i = 0; i < this.poolCapacity; i++)
			{
				this.Items.Enqueue(factory());
			}
		}

		/// <inheritdoc cref="RecyclePool{T}(int, System.Func{T})"/>
		public RecyclePool(int maxPoolSize, System.Func<int, T> factory) : this(maxPoolSize)
		{
			for (var i = 0; i < this.poolCapacity; i++)
			{
				this.Items.Enqueue(factory(i));
			}
		}

		/// <summary>
		/// Fetch the item at the top of the queue
		/// </summary>
		/// <returns>Item at the top of the queue</returns>
		/// <exception cref="System.InvalidOperationException">Thrown when the queue is empty</exception>
		public T Get()
		{
			var output = this.Items.Dequeue();

			this.Items.Enqueue(output);

			return output;
		}

		/// <summary>
		/// Try to get the item at the top of the queue
		/// </summary>
		/// <param name="item">Item that will be dequeued</param>
		/// <returns>If an item was available to dequeue</returns>
		public bool TryGet([NotNullWhen(true)] out T item)
		{
			if (this.poolSize is 0)
			{
				item = default;
				return false;
			}

			item = this.Get();

			return true;
		}

		/// <summary>
		/// Return an item to the pool. It will be added to the back of the pool.
		/// </summary>
		/// <param name="item">Item to return</param>
		/// <returns>If the item has been enqueued</returns>
		public bool Return(T item)
		{
			if (this.poolSize == this.poolCapacity)
			{
				return false;
			}

			this.Items.Enqueue(item);

			return true;
		}
	}

	/// <summary>
	/// Custom version of the <see cref="RecyclePool{T}"/> for <see cref="GameObject"/>s
	/// </summary>
	public sealed class GameObjectRecyclePool : RecyclePool<GameObject>, System.IDisposable
	{
		/// <inheritdoc cref="RecyclePool{T}(int)"/>
		public GameObjectRecyclePool(int maxPoolSize) : base(maxPoolSize)
		{
		}

		/// <inheritdoc cref="RecyclePool{T}(int, System.Func{T})"/>
		public GameObjectRecyclePool(int maxPoolSize, System.Func<GameObject> factory) : base(maxPoolSize, factory)
		{
		}

		/// <inheritdoc cref="RecyclePool{T}(int, System.Func{T})"/>
		public GameObjectRecyclePool(int maxPoolSize, System.Func<int, GameObject> factory) : base(maxPoolSize, factory)
		{
		}

		/// <summary>
		/// Dispose all items in the <see cref="RecyclePool{T}"/>
		/// </summary>
		public void Dispose()
		{
			while (this.Items.Count is not 0)
			{
				Object.Destroy(this.Items.Dequeue());
			}
		}
	}
}
