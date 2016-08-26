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

			// create the world
			Hitable_List world = new Hitable_List ();
			world.hitables.Add (new Sphere (new Vector3 (0.0f, 0.0f, -1.0f), 0.5f));
			world.hitables.Add (new Sphere (new Vector3 (0.0f, -100.5f, -1.0f), 100.0f));

			for (int i = ny-1; i >= 0; i--) {
				for (int j = 0; j < nx; j++) {
					float u = (float)j / (float)nx;
					float v = (float)i / (float)ny;

					Ray r = new Ray (origin, lower_left + horizontal * u + vertical * v);
					Vector3 p = r.point (2.0f);
					Vector3 col = color (r, world);

					// values written to file
					int ir = (int)(255.99f * col.r());
					int ig = (int)(255.99f * col.g());
					int ib = (int)(255.99f * col.b());
					Console.Write (ir + " " + ig + " " + ib + "\n");
				}
			}
		}

		// if no object is hit, draw a gradient
		static Vector3 color(Ray r, Hitable world) {
			hit_record rec = new hit_record();

			// see if an object is hit
			if (world.hit(r, 0.0f, float.MaxValue, ref rec)) {
				// shade the indicated point
				return (new Vector3 (rec.normal.x () + 1.0f, rec.normal.y () + 1.0f, rec.normal.z () + 1.0f)) * 0.5f;
			} else {
				// no object hit, draw the background
				Vector3 unit_dir = r.direction().unit_vector();
				float t = 0.5f * (unit_dir.y () + 1.0f);
				return (new Vector3 (1.0f, 1.0f, 1.0f)) * (1.0f - t) + (new Vector3 (0.5f, 0.7f, 1.0f) * t);
			
			}
		}
	}
}
