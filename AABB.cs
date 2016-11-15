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

		public Vector3 min() {
			return _min;
		}

		public Vector3 max() {
			return _max;
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

		public static AABB surrounding_box(AABB box0, AABB box1) {
			Vector3 small = new Vector3 (Utils.fmin (box0.min ().x (), box1.min ().x ()),
				                Utils.fmin (box0.min ().y (), box1.min ().y ()),
				                Utils.fmin (box0.min ().z (), box1.min ().z ()));
			Vector3 large = new Vector3 (Utils.fmax (box0.max ().x (), box1.max ().x ()),
				                Utils.fmax (box0.max ().y (), box1.max ().y ()),
				                Utils.fmax (box0.max ().z (), box1.max ().z ()));
			return new AABB (small, large);
		}
	}
}

