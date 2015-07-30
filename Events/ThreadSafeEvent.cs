using System;

namespace Xevle.Core.Events
{
	/// <summary>
	/// Thread safe event.
	/// </summary>
	public class ThreadSafeEvent
	{
		EventHandler internalEventHandler;
		readonly object internalEventHandlerLock = new object();

		/// <summary>
		/// Encapsulates the event.
		/// </summary>
		public event EventHandler Event
		{
			add
			{
				lock (internalEventHandlerLock)
				{
					internalEventHandler += value;
				}
			}
			remove
			{
				lock (internalEventHandlerLock)
				{
					internalEventHandler -= value;
				}
			}
		}

		/// <summary>
		/// Fires the event.
		/// </summary>
		public virtual void Fire(object sender, EventArgs e)
		{
			EventHandler handler;
			lock (internalEventHandlerLock)
			{
				handler = internalEventHandler;
			}
			if (handler != null)
			{
				handler(sender, e);
			}
		}
	}

	/// <summary>
	/// Thread safe event.
	/// </summary>
	public sealed class ThreadSafeEvent<T> : IDisposable where T : EventArgs
	{
		EventHandler<T> internalEventHandler;
		readonly object internalEventHandlerLock = new object();

		/// <summary>
		/// Encapsulates the event.
		/// </summary>
		public event EventHandler<T> Event
		{
			add
			{
				lock (internalEventHandlerLock)
				{
					internalEventHandler += value;
				}
			}
			remove
			{
				lock (internalEventHandlerLock)
				{
					internalEventHandler -= value;
				}
			}
		}

		/// <summary>
		/// Fires the event.
		/// </summary>
		public void Fire(object sender, T e)
		{
			EventHandler<T> handler;
			lock (internalEventHandlerLock)
			{
				handler = internalEventHandler;
			}

			if (handler != null)
			{
				handler(sender, e);
			}
		}

		/// <summary>
		/// Removes all.
		/// </summary>
		public void RemoveAll()
		{
			lock (internalEventHandlerLock)
			{
				internalEventHandler = null;
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Xevle.Core.Events.ThreadSafeEvent`1"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Xevle.Core.Events.ThreadSafeEvent`1"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Xevle.Core.Events.ThreadSafeEvent`1"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="Xevle.Core.Events.ThreadSafeEvent`1"/> so the garbage collector can reclaim the memory that the
		/// <see cref="Xevle.Core.Events.ThreadSafeEvent`1"/> was occupying.</remarks>
		public void Dispose()
		{
			RemoveAll();
		}
	}
}
