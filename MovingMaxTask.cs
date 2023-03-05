using System.Collections.Generic;

namespace yield
{

	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var window = new Queue<DataPoint>();
			var potentialMax = new LinkedList<DataPoint>();
			foreach (DataPoint point in data)
			{
				if (window.Count == windowWidth)
                {
					DataPoint deletedPoint = window.Dequeue();
					if (deletedPoint == potentialMax.First.Value)
						potentialMax.RemoveFirst();
				}
                while (potentialMax.Last?.Value.OriginalY < point.OriginalY)
                {
					potentialMax.RemoveLast();
                }
				DataPoint movingMax = point.WithMaxY(potentialMax.First == null ? point.OriginalY : potentialMax.First.Value.OriginalY);
				potentialMax.AddLast(movingMax);
				window.Enqueue(movingMax);
				yield return movingMax;
			}
		}
	}
}