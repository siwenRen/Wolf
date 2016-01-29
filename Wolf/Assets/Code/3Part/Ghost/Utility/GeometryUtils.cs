using UnityEngine;
using System.Collections.Generic;
using Ghost.Extensions;

namespace Ghost.Utils
{
	public static class GeometryUtils {

		public static Vector3 GetCenterPoint(Vector3[] points)
		{
			if (points.IsNullOrEmpty())
			{
				return Vector3.zero;
			}
			if (1 == points.Length)
			{
				return points[0];
			}
			Vector3 min = points[0];
			Vector3 max = points[0];
			for (int i = 1; i < points.Length; ++i)
			{
				min = Vector3.Min(min, points[i]);
				max = Vector3.Min(max, points[i]);
			}
			return new Vector3((max.x-min.x)/2, (max.y-min.y)/2, (max.z-min.z)/2);
		}

		public static Vector3 GetCenterPoint(Component[] objts)
		{
			if (objts.IsNullOrEmpty())
			{
				return Vector3.zero;
			}
			if (1 == objts.Length)
			{
				return objts[0].transform.position;
			}
			Vector3 min = objts[0].transform.position;
			Vector3 max = objts[0].transform.position;
			for (int i = 1; i < objts.Length; ++i)
			{
				var point = objts[i].transform.position;
				min = Vector3.Min(min, point);
				max = Vector3.Min(max, point);
			}
			return new Vector3((max.x-min.x)/2, (max.y-min.y)/2, (max.z-min.z)/2);
		}

		public static bool PositionAlmostEqual(Vector3 p1, Vector3 p2)
		{
			return 0.01f > Vector3.Distance(p1, p2);
		}

		public static bool PositionAlmostEqual(Vector2 p1, Vector2 p2)
		{
			return 0.01f > Vector2.Distance(p1, p2);
		}

		public static Vector3 Bezier (float t, Vector3 p0, Vector3 p1, Vector3 p2)
		{
			float u = 1.0f - t;
			float tt = t * t;
			float tu = 2 * t * u;
			float uu = u * u;
			Vector3 res = new Vector3 ();
			res.x = uu * p0.x + tu * p1.x + tt * p2.x;
			res.y = uu * p0.y + tu * p1.y + tt * p2.y;
			res.z = uu * p0.z + tu * p1.z + tt * p2.z;
			return res;
		}

		public static float UniformAngle(float a)
		{
//			var oldA = a;
			a = ((int)a % 360) + (a-(int)a);
			if (0 > a)
			{
				a += 360;
			}
			return a;
		}

		public static float DistanceOfPointToVector(Vector3 startPoint, Vector3 endPoint, Vector3 point)
		{
			Vector2 startVe2 = startPoint.XZ();
			Vector2 endVe2 = endPoint.XZ();
			float A = endVe2.y - startVe2.y;
			float B = startVe2.x - endVe2.x;
			float C = endVe2.x * startVe2.y - startVe2.x * endVe2.y;
			float denominator = Mathf.Sqrt(A * A + B * B);
			Vector2 pointVe2 = point.XZ();
			return Mathf.Abs((A * pointVe2.x + B * pointVe2.y + C) / denominator);;
		}

		public static float GetAngleByAxisY(Vector3 src, Vector3 target)
		{
			var direction = target-src;
			return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		}

	}
}
