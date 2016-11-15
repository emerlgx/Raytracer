using System;

namespace Raytracer
{
	public class Moving_Sphere : Hitable
	{
		Vector3 center0;
		Vector3 center1;
		float time0, time1;

		float radius;
		Material material;

		public Moving_Sphere(Vector3 cen0, Vector3 cen1, float t0, float t1, float r, Material mat) {
			center0 = cen0;
			center1 = cen1;
			time0 = t0;
			time1 = t1;

			radius = r;
			material = mat;
		}

		public override bool hit(Ray r, float t_min, float t_max, ref hit_record rec) {
			Vector3 oc = r.origin () - center(r._time);
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
					rec.normal = (rec.p - center(r._time)) / radius;
					rec.mat = material;
					return true;
				}

				temp = (-b + (float)Math.Sqrt (b * b - 4 * a * c)) / (2 * a);
				if (temp < t_max && temp > t_min) {
					rec.t = temp;
					rec.p = r.point (rec.t);
					rec.normal = (rec.p - center(r._time)) / radius;
					rec.mat = material;
					return true;
				}
			}
			return false;
		}

		public override bool bounding_box(float t0, float t1, ref AABB box) {
			AABB box0 = new AABB (center(t0) - new Vector3 (radius, radius, radius),
				center + new Vector3 (radius, radius, radius));
			AABB box1 = new AABB (center(t1) - new Vector3 (radius, radius, radius),
				center + new Vector3 (radius, radius, radius));
			box = AABB.surrounding_box (box0, box1);

			return true;
		}

		Vector3 center (float t) {
			return center0 + (time0 - t) / (time1 - t) * (center1 - center0);
		}
	}
}