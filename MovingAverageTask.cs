using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<DataPoint> window = new Queue<DataPoint>();
			double ySum = 0;
			foreach(DataPoint point in data)
            {
				if(window.Count == windowWidth)
					ySum -= window.Dequeue().OriginalY;
				double averageY = (point.OriginalY + ySum) / (1 + window.Count);
				ySum += point.OriginalY;
				DataPoint smoothedPoint = point.WithAvgSmoothedY(averageY);
				window.Enqueue(smoothedPoint);
				yield return smoothedPoint;
			}
		}
	}
}