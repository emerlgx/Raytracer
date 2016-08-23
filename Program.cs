using System;
using System.IO;

// code adapted from "Ray Tracing in One Weekend" by Peter Shirley
namespace Raytracer
{
	class MainClass
	{
		static int nx = 200;
		static int ny = 100;
		public static void Main (string[] args)
		{
			// output to a ppm file
			Console.Write("P3\n" + nx + " " + ny + " " + "\n255\n");

			// make axis vectors
			Vector3 lower_left = new Vector3 (-2.0f, -1.0f, -1.0f);
			Vector3 horizontal = new Vector3 (4.0f, 0.0f, 0.0f);
			Vector3 vertical = new Vector3 (0.0f, 2.0f, 0.0f);
			Vector3 origin = new Vector3 (0.0f, 0.0f, 0.0f);

			for (int i = ny-1; i >= 0; i--) {
				for (int j = 0; j < nx; j++) {
					float u = (float)j / (float)nx;
					float v = (float)i / (float)ny;

					Ray r = new Ray (origin, lower_left + horizontal * u + vertical * v);
					Vector3 col = color (r);

					// values written to file
					int ir = (int)(255.99f * col.r());
					int ig = (int)(255.99f * col.g());
					int ib = (int)(255.99f * col.b());
					Console.Write (ir + " " + ig + " " + ib + "\n");
				}
			}
		}

		// if no object is hit, draw a gradient
		static Vector3 color(Ray r) {
			// see if an object is hit
			float t = hit_sphere (new Vector3 (0.0f, 0.0f, -1.0f), 0.5f, r);
			if (t > 0.0f) {
				// shade the indicated point on the sphere
				Vector3 N = (r.point (t) - new Vector3 (0.0f, 0.0f, -1.0f)).unit_vector();
				return (new Vector3 (N.x () + 1.0f, N.y () + 1.0f, N.z () + 1.0f)) * 0.5f;
			}

			// no object hit, draw the background
			Vector3 unit_dir = r.direction().unit_vector();
			t = 0.5f * (unit_dir.y () + 1.0f);
			return (new Vector3 (1.0f, 1.0f, 1.0f)) * (1.0f - t) + (new Vector3 (0.5f, 0.7f, 1.0f) * t);

		}

		// detect if a specified sphere is hit
		static float hit_sphere(Vector3 center, float radius, Ray r) {
			Vector3 oc = r.origin () - center;
			float a = Vector3.dot (r.direction (), r.direction ());
			float b = 2.0f * Vector3.dot (oc, r.direction ());
			float c = Vector3.dot (oc, oc) - radius*radius;
			float discriminant = b * b - 4 * a * c;

			if (discriminant < 0.0f) {
				return -1.0f;
			} else {
				return (float)((-b - Math.Sqrt (discriminant)) / (2.0f * a));
			}
		}
	}
}
