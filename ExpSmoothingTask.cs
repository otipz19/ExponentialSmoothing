using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		private static DataPoint prevPoint;

		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			prevPoint = null;
			foreach(DataPoint point in data)
            {
				if(prevPoint == null)
                {
					yield return prevPoint = point.WithExpSmoothedY(point.OriginalY);
                }
                else
                {
					yield return prevPoint = point.WithExpSmoothedY(alpha * point.OriginalY + (1 - alpha) * prevPoint.ExpSmoothedY);
				}
            }
		}
	}
}