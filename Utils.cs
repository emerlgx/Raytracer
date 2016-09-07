using System;

namespace Raytracer
{
	public class Utils
	{
		public static Random rnd;

		static Utils() {
			rnd = new Random();
		}

		public static Vector3 random_in_unit_sphere() {
			Vector3 p = new Vector3 ();
			do {
				p = (2.0f * (new Vector3((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble())))
					- (new Vector3(1.0f, 1.0f, 1.0f));
			} while(Vector3.dot(p,p) >= 1.0f);
			p = p.unit_vector ();
			return p;
		}

		public static Vector3 reflect(Vector3 v, Vector3 n) {
			return v - 2.0f * Vector3.dot (v, n) * n;
		}
	}
}

