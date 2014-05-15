#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: ObjectCache
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//  Description		: Generic LRU (Least Recently Used) object caching class
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Threading;

namespace TcmTemplating.Helpers
{
	public class ObjectCache<TKey, TValue> where TKey : class
	{
		private ReaderWriterLockSlim mLock;
		private int mCapacity;
		private Dictionary<TKey, TValue> mCache;
		private TKey mLastKey;
		private TValue mLastValue;

		/// <summary>
		/// Keeps up with the most recently read items.
		/// Items at the end of the list were read last. 
		/// Items at the front of the list have been the most idle.
		/// Items at the front are removed if the cache capacity is reached.
		/// </summary>
		private LinkedList<TKey> mAge;

		private void ResetAge(TKey Key, TValue Value, bool WriteLockAcquired)
		{
			// Has this key already been requested?
			if (mLastKey == Key)
				return;

			try
			{
				if (!WriteLockAcquired)
					mLock.EnterWriteLock();

				// Super-cache a reference to the requested key/value
				mLastKey = Key;
				mLastValue = Value;

				// First Key being added in the cache
				if (mAge.Last == null)
				{
					mAge.AddLast(Key);
					return;
				}

				// Verify if the given Key is not already the last accessed item
				if (mAge.Last.Value != Key)
				{
					mAge.Remove(Key);
					mAge.AddLast(Key);
				}
			}
			finally
			{
				if (!WriteLockAcquired)
					mLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Gets the maximum cache capacity
		/// </summary>
		/// <value>
		/// Maximum Cache capacity
		/// </value>
		public int Capacity
		{
			get
			{
				return mCapacity;
			}
		}

		/// <summary>
		/// Get the number of objects in the cache
		/// </summary>
		/// <value>
		/// Cache item count
		/// </value>
		public int Count
		{
			get
			{
				try
				{
					mLock.EnterReadLock();

					return mCache.Count;
				}
				finally
				{
					mLock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectCache{T}"/> class.
		/// </summary>
		public ObjectCache(): this(10)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectCache" /> class.
		/// </summary>
		/// <param name="Capacity">Maximum cache capacity</param>
		public ObjectCache(int Capacity)
		{
			mLock = new ReaderWriterLockSlim();
			mCapacity = Capacity;
			mCache = new Dictionary<TKey, TValue>(mCapacity);
			mAge = new LinkedList<TKey>();
		}

		/// <summary>
		/// Gets or sets the <see cref="TValue"/> with the specified <see cref="TKey"/>
		/// </summary>
		/// <value>
		/// <see cref="TValue"/>
		/// </value>
		/// <param name="Key">Cache <see cref="TKey"/></param>
		/// <returns>Cache <see cref="TValue"/></returns>
		/// <exception cref="System.Exception">Capacity mismatch.</exception>
		public TValue this[TKey Key]
		{
			get
			{
				return Get(Key);
			}
			set
			{
				Add(Key, value);
			}
		}

		/// <summary>
		/// Adds the <see cref="TValue"/> using the specified <see cref="TKey" />
		/// </summary>
		/// <param name="Key">Cache <see cref="TKey"/></param>
		/// <param name="Value">Cache <see cref="TValue" /></param>
		/// <remarks>Add overwrites an existing <see cref="TValue"/> in case of duplicate <see cref="TKey"/></remarks>
		public void Add(TKey Key, TValue Value)
		{
			try
			{
				mLock.EnterWriteLock();

				if (mCapacity > 0 && mCache.Count == mCapacity)
				{
					// Capacity has been reached, remove the oldest item
					mCache.Remove(mAge.First.Value);
					mAge.RemoveFirst();
				}

				mCache[Key] = Value;

				ResetAge(Key, Value, true);

				if (mAge.Count != mCache.Count)
					throw new Exception("Capacity mismatch.");
			}
			finally
			{
				mLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Gets the <see cref="TValue" /> for the specified <see cref="TKey"/>
		/// </summary>
		/// <param name="Key">Cache <see cref="TKey"/></param>
		/// <returns>Cache <see cref="TValue"/></returns>
		public TValue Get(TKey Key)
		{
			try
			{
				mLock.EnterUpgradeableReadLock();

				// Immediately return if the Key is equal to the last fetched Key
				if (mLastKey == Key)
					return mLastValue;

				TValue value;

				if (!mCache.TryGetValue(Key, out value))
					return default(TValue);

				// This item has been accessed, reset its age
				ResetAge(Key, value, false);
				
				return value;
			}
			finally
			{
				mLock.ExitUpgradeableReadLock();
			}
		}

		/// <summary>
		/// Removes the specified <see cref="TKey" /> from the <see cref="ObjectCache"/>
		/// </summary>
		/// <param name="Key">Cache <see cref="TKey"/></param>
		public void Remove(TKey Key)
		{
			try
			{
				mLock.EnterWriteLock();

				mCache.Remove(Key);
				mAge.Remove(Key);
			}
			finally
			{
				mLock.ExitWriteLock();
			}
		}
		
		/// <summary>
		/// Clears all items from the <see cref="ObjectCache" />
		/// </summary>
		public void Clear()
		{
			try
			{
				mLock.EnterWriteLock();

				mAge.Clear();
				mCache.Clear();
			}
			finally
			{
				mLock.ExitWriteLock();
			}
		}
	}
}
