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

		public static bool refract (Vector3 v, Vector3 n, float ni_over_nt, ref Vector3 refracted) {
			Vector3 uv = v.unit_vector ();
			Vector3 un = n.unit_vector ();
			float dt = Vector3.dot (uv, un);
			float discriminant = 1.0f - ni_over_nt * ni_over_nt * (1.0f - dt * dt);

			// decide whether to reflect or refract
			if (discriminant > 0.0f) {
				refracted = ni_over_nt * (uv - un * dt) - un * (float)Math.Sqrt (discriminant);
				return true;
			} else {
				return false;
			}
		}

		// used for approximating glass reflectivity
		public static float schlick(float cosine, float ref_index) {
			float r0 = (1.0f - ref_index) / (1.0f + ref_index);
			r0 = r0 * r0;
			return r0 + (1.0f - r0) * (float)Math.Pow ((1.0f - cosine), 5.0f);
		}

	}
}

