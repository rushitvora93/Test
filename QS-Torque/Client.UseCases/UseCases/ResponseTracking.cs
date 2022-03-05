using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Core.UseCases
{
    class ResponseTracking<T>
	{
		public class ResponseTracker
		{
			public bool requestCompleted;
			public T payload;
		}

		public ResponseTracker Add(T item)
		{
			var tracker = new ResponseTracker { requestCompleted = false, payload = item };
			requestResponse.AddOrUpdate(counter++, (a) => tracker, (a, b) => tracker);
			return tracker;
		}

		public void Update(ResponseTracker tracker, T data)
		{
			tracker.payload = data;
			tracker.requestCompleted = true;
		}

		public T TopResponse()
		{
			return requestResponse.GetOrAdd(counter - 1, (ResponseTracker)null).payload;
		}

		public void Cleanup()
		{
			lock (this)
			{
				var itemsToRemove = new List<int>();
				foreach (var item in requestResponse)
				{
					if (item.Value.requestCompleted == true && item.Key != counter - 1)
					{
						itemsToRemove.Add(item.Key);
					}
				}
				foreach (var item in itemsToRemove)
				{
					requestResponse.TryRemove(item, out ResponseTracker result);
				}
			}
		}

		private ConcurrentDictionary<int, ResponseTracker> requestResponse = new ConcurrentDictionary<int, ResponseTracker>();
		private volatile int counter = 0;
	}
}
