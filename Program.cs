using System;
using System.IO;

// code adapted from "Ray Tracing in One Weekend" by Peter Shirley
namespace Raytracer
{
	class MainClass
	{
		static int nx = 800;
		static int ny = 400;
		static int ns = 100;

		public static void Main (string[] args)
		{
			// output to a ppm file
			Console.Write("P3\n" + nx + " " + ny + " " + "\n255\n");

			// create the world
			Hitable_List world = new Hitable_List ();
			world.hitables.Add (new Moving_Sphere (new Vector3 (0.0f, 0.0f, 0.0f),
											new Vector3 (0.0f, 0.0f, -3.0f),
											0.0f,
											10.0f,
											0.5f,
			                                new Lambertian (new Vector3 (0.8f, 0.3f, 0.3f))));
			world.hitables.Add (new Sphere (new Vector3 (1.0f, 0.0f, -1.0f),
			                                0.5f,
			                                new Dielectric (1.5f)));
			world.hitables.Add (new Sphere (new Vector3 (1.0f, 0.0f, -1.0f),
											-0.3f,
											new Dielectric (1.5f)));
			world.hitables.Add (new Sphere (new Vector3 (-1.0f, 0.0f, -1.0f),
			                                0.5f,
			                                new Metal (new Vector3 (0.8f, 0.8f, 0.8f), 1.0f)));
			world.hitables.Add (new Sphere (new Vector3 (0.0f, -100.5f, -1.0f),
											100.0f,
											new Lambertian (new Vector3 (0.8f, 0.8f, 0.0f))));


			// init the camera
			Vector3 lookfrom = new Vector3 (3.0f, 3.0f, 2.0f);
			Vector3 lookat = new Vector3 (0.0f, 0.0f, -1.0f);
			float dist_to_focus = (lookfrom - lookat).length ();
			float apeture = 2.0f;
			Camera cam = new Camera (lookfrom,
				             lookat,
				             new Vector3 (0.0f, 1.0f, 0.0f),
				             20.0f,
				             (float)nx / (float)ny,
				             apeture,
				             dist_to_focus,
				             0.0f,
				             10.0f);

			for (int i = ny-1; i >= 0; i--) {
				for (int j = 0; j < nx; j++) {

					// supersampling for antialiasing
					Vector3 col = new Vector3 ();
					for (int s = 0; s < ns; s++) {
						float u = (float)(j + Utils.rnd.NextDouble()) / (float)nx;
						float v = (float)(i + Utils.rnd.NextDouble()) / (float)ny;
						
						Ray r = cam.getRay(u,v);
						col += color (r, world, 0);
					}

					col /= (float)ns;
					// gamma correction
					col = new Vector3 ((float)Math.Sqrt (col.x ()), 
					                   (float)Math.Sqrt (col.y ()), 
					                   (float)Math.Sqrt (col.z ()));
					// values written to file
					int ir = (int)(255.99f * col.r());
					int ig = (int)(255.99f * col.g());
					int ib = (int)(255.99f * col.b());
					Console.Write (ir + " " + ig + " " + ib + "\n");
				}
			}
		}

		// if no object is hit, draw a gradient
		static Vector3 color(Ray r, Hitable world, int depth) {
			hit_record rec = new hit_record();

			// see if an object is hit
			if (world.hit(r, 0.001f, float.MaxValue, ref rec)) {
				// shade the indicated point
				Ray scattered = new Ray();
				Vector3 attenuation = new Vector3();
				if (depth < 50 && rec.mat.scatter (r, rec, ref attenuation, ref scattered)) {
					return attenuation * color (scattered, world, depth + 1);
				} else {
					// reached our max depth, don't render any further
					return new Vector3 ();
				}
			} else {
				// no object hit, draw the background
				Vector3 unit_dir = r.direction().unit_vector();
				float t = 0.5f * (unit_dir.y () + 1.0f);
				return (new Vector3 (1.0f, 1.0f, 1.0f)) * (1.0f - t) + (new Vector3 (0.5f, 0.7f, 1.0f) * t);
			}
		}

	}
}
