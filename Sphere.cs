using System;

namespace Raytracer
{
	public class Sphere : Hitable
	{
		Vector3 center;
		float radius;

		public Sphere () {
			center = new Vector3 ();
			radius = 0.0f;
		}

		public Sphere(Vector3 cen, float r) {
			center = cen;
			radius = r;
		}

		public override bool hit(Ray r, float t_min, float t_max, ref hit_record rec) {
			Vector3 oc = r.origin () - center;
			float a = Vector3.dot (r.direction (), r.direction ());
			float b = 2.0f * Vector3.dot (oc, r.direction ());
			float c = Vector3.dot (oc, oc) - radius*radius;
			float discriminant = b * b - 4 * a * c;

			if (discriminant > 0) {
				// update the hit_record to pass the information back to the calling object
				float temp = (-b - (float)Math.Sqrt (b * b - a * c)) / a; // check the roots
				if (temp < t_max && temp > t_min) {
					rec.t = temp;
					rec.p = r.point (rec.t);
					rec.normal = (rec.p - center) / radius;
					return true;
				}

				temp = (-b + (float)Math.Sqrt (b * b - a * c)) / a;
				if (temp < t_max && temp > t_min) {
					rec.t = temp;
					rec.p = r.point (rec.t);
					rec.normal = (rec.p - center) / radius;
					return true;
				}
			}
			return false;
		}
	}
}

