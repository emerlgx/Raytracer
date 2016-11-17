using System;
using System.IO;

namespace Raytracer
{
	public class Utils
	{
		public static Random rnd;

		static Utils() {
			rnd = new Random();
		}

		public static float fmin (float a, float b) {
			if (a > b) {
				return b;
			} else {
				return a;
			}
		}

		public static float fmax (float a, float b) {
			if (a < b) {
				return b;
			} else {
				return a;
			}
		}

		public static Vector3 random_in_unit_sphere() {
			Vector3 p = new Vector3 ();
			do {
				p = 2.0f * new Vector3((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble())
					- new Vector3(1.0f, 1.0f, 1.0f);
			} while(Vector3.dot(p,p) >= 1.0f);
			p = p.unit_vector ();
			return p;
		}

		public static Vector3 random_in_unit_disk() {
			Vector3 p;
			do {
				p = 2.0f * new Vector3((float)rnd.NextDouble(), (float)rnd.NextDouble(), 0.0f) - new Vector3(1.0f,1.0f,0.0f);
			} while (Vector3.dot (p, p) >= 1.0f);

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

		// used for comparing bounding boxes of two hitables
		public static int hitable_box_compare_x (Hitable a, Hitable b) {
			AABB box_left = new AABB ();
			AABB box_right = new AABB();
			if (!a.bounding_box (0.0f, 0.0f, ref box_left) || !b.bounding_box (0.0f, 0.0f, ref box_right)) {
				Console.Error.WriteLine ("No possible bounding box during x comparison");
			}
			if (box_left.min ().x () - box_right.min ().x () < 0.0f) {
				return -1;
			} else {
				return 1;
			}
		}

		public static int hitable_box_compare_y (Hitable a, Hitable b) {
			AABB box_left = new AABB ();
			AABB box_right = new AABB();
			if (!a.bounding_box (0.0f, 0.0f, ref box_left) || !b.bounding_box (0.0f, 0.0f, ref box_right)) {
				Console.Error.WriteLine ("No possible bounding box during x comparison");
			}
			if (box_left.min ().y () - box_right.min ().y () < 0.0f) {
				return -1;
			} else {
				return 1;
			}
		}

		public static int hitable_box_compare_z (Hitable a, Hitable b) {
			AABB box_left = new AABB ();
			AABB box_right = new AABB();
			if (!a.bounding_box (0.0f, 0.0f, ref box_left) || !b.bounding_box (0.0f, 0.0f, ref box_right)) {
				Console.Error.WriteLine ("No possible bounding box during x comparison");
			}
			if (box_left.min ().z () - box_right.min ().z () < 0.0f) {
				return -1;
			} else {
				return 1;
			}
		}

		public static float trilinear_interpolation(Vector3[,,] c, float u, float v, float w) {
			float accum = 0.0f;
			float uu = u * u * (3 - 2 * u);
			float vv = v * v * (3 - 2 * v);
			float ww = w * w * (3 - 2 * w);
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					for (int k = 0; k < 2; k++) {
						Vector3 weight = new Vector3 (u - i, v - j, w - k);
						accum += (i * uu + (1.0f - i) * (1.0f - uu)) *
						(j * vv + (1.0f - j) * (1.0f - vv)) *
						(k * ww + (1.0f - k) * (1.0f - ww)) *
						Vector3.dot(c [i, j, k], weight);
					}
				}
			}
			return accum;
		}
	}
}

