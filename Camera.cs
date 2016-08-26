using System;

namespace Raytracer
{
	public class Camera
	{
		Vector3 origin;
		Vector3 lower_left;
		Vector3 horizontal;
		Vector3 vertical;

		public Camera () {
			// default camera coordinates
			lower_left = new Vector3 (-2.0f, -1.0f, -1.0f);
			horizontal = new Vector3 (4.0f, 0.0f, 0.0f);
			vertical = new Vector3 (0.0f, 2.0f, 0.0f);
			origin = new Vector3 (0.0f, 0.0f, 0.0f);
		}

		public Camera (Vector3 ll, Vector3 h, Vector3 v, Vector3 o) {
			lower_left = ll;
			horizontal = h;
			vertical = v;
			origin = o;
		}

		public Ray getRay(float u, float v) {
			return new Ray (origin, lower_left + horizontal * u + vertical * v - origin);
		}
	}
}

