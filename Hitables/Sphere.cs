using System;

namespace Raytracer
{
	public class Sphere : Hitable
	{
		Vector3 center;
		float radius;
		Material material;

		public Sphere () {
			center = new Vector3 ();
			radius = 0.0f;
			material = new Lambertian (new Vector3 ());
		}

		public Sphere(Vector3 cen, float r, Material mat) {
			center = cen;
			radius = r;
			material = mat;
		}

		public override bool hit(Ray r, float t_min, float t_max, ref hit_record rec) {
			Vector3 oc = r.origin () - center;
			float a = Vector3.dot (r.direction (), r.direction ());
			float b = 2.0f * Vector3.dot (oc, r.direction ());
			float c = Vector3.dot (oc, oc) - radius*radius;
			float discriminant = b * b - 4 * a * c;

			if (discriminant > 0) {
				// update the hit_record to pass the information back to the calling object
				float temp = (-b - (float)Math.Sqrt (b * b - 4 * a * c)) / (2 * a); // check the roots
				if (temp < t_max && temp > t_min) {
					rec.t = temp;
					rec.p = r.point (rec.t);
					rec.normal = (rec.p - center) / radius;
					rec.mat = material;
					return true;
				}

				temp = (-b + (float)Math.Sqrt (b * b - 4 * a * c)) / (2 * a);
				if (temp < t_max && temp > t_min) {
					rec.t = temp;
					rec.p = r.point (rec.t);
					rec.normal = (rec.p - center) / radius;
					rec.mat = material;
					return true;
				}
			}
			return false;
		}

		public override bool bounding_box(float t0, float t1, ref AABB box) {
			box = new AABB (center - new Vector3 (radius, radius, radius),
							center + new Vector3 (radius, radius, radius));
			return true;
		}
	}
}

