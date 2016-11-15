using System;

namespace Raytracer
{
	public class AABB
	{
		Vector3 _min;
		Vector3 _max;

		public AABB () {}

		public AABB (Vector3 a, Vector3 b) {
			_min = a;
			_max = b;
		}

		public bool hit (Ray r, float tmin, float tmax) {
			float t0 = Utils.fmin ((_min.x () - r.origin ().x ()) / r.direction ().x (),
				           (_max.x () - r.origin ().x ()) / r.direction ().x ());
			float t1 = Utils.fmax ((_min.x () - r.origin ().x ()) / r.direction ().x (),
				(_max.x () - r.origin ().x ()) / r.direction ().x ());

			tmin = Utils.fmax (t0, tmin);
			tmax = Utils.fmin (t1, tmax);

			if (tmax <= tmin) {
				return false;
			}

			t0 = Utils.fmin ((_min.y () - r.origin ().y ()) / r.direction ().y (),
				(_max.y () - r.origin ().y ()) / r.direction ().y ());
			t1 = Utils.fmax ((_min.y () - r.origin ().y ()) / r.direction ().y (),
				(_max.y () - r.origin ().y ()) / r.direction ().y ());

			tmin = Utils.fmax (t0, tmin);
			tmax = Utils.fmin (t1, tmax);

			if (tmax <= tmin) {
				return false;
			}

			t0 = Utils.fmin ((_min.z () - r.origin ().z ()) / r.direction ().z (),
				(_max.z () - r.origin ().z ()) / r.direction ().z ());
			t1 = Utils.fmax ((_min.y () - r.origin ().y ()) / r.direction ().y (),
				(_max.z () - r.origin ().z ()) / r.direction ().z ());

			tmin = Utils.fmax (t0, tmin);
			tmax = Utils.fmin (t1, tmax);

			if (tmax <= tmin) {
				return false;
			}

			return true;
		}
	}
}

