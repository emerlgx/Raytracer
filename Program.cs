using System;
using System.IO;

// code adapted from "Ray Tracing in One Weekend" by Peter Shirley
namespace Raytracer
{
	class MainClass
	{
		static int nx = 200;
		static int ny = 100;
		static int ns = 100;
		static Random rnd;

		public static void Main (string[] args)
		{
			// output to a ppm file
			Console.Write("P3\n" + nx + " " + ny + " " + "\n255\n");

			// create the world
			Hitable_List world = new Hitable_List ();
			world.hitables.Add (new Sphere (new Vector3 (0.0f, 0.0f, -1.0f),
			                                0.5f,
			                                new Lambertian (new Vector3 (0.8f, 0.3f, 0.3f))));
			world.hitables.Add (new Sphere (new Vector3 (0.0f, -100.5f, -1.0f),
			                                100.0f,
			                                new Lambertian (new Vector3 (0.8f, 0.8f, 0.0f))));
			world.hitables.Add (new Sphere (new Vector3 (1.0f, 0.0f, -1.0f),
			                                0.5f,
			                                new Metal (new Vector3 (0.8f, 0.6f, 0.2f), 0.3f)));
			world.hitables.Add (new Sphere (new Vector3 (-1.0f, 0.0f, -1.0f),
			                                0.5f,
			                                new Metal (new Vector3 (0.8f, 0.8f, 0.8f), 1.0f)));


			// init the camera
			Camera cam = new Camera ();

			rnd = new Random ();

			for (int i = ny-1; i >= 0; i--) {
				for (int j = 0; j < nx; j++) {

					// supersampling for antialiasing
					Vector3 col = new Vector3 ();
					for (int s = 0; s < ns; s++) {
						float u = (float)(j + rnd.NextDouble()) / (float)nx;
						float v = (float)(i + rnd.NextDouble()) / (float)ny;
						
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
