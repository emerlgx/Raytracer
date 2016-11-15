using System;

namespace Raytracer
{
	public class Camera
	{
		Vector3 origin;
		Vector3 lower_left;
		Vector3 horizontal;
		Vector3 vertical;
		Vector3 u, v, w;
		float lens_radius;

		/*public Camera () {
			// default camera coordinates
			lower_left = new Vector3 (-2.0f, -1.0f, -1.0f);
			horizontal = new Vector3 (4.0f, 0.0f, 0.0f);
			vertical = new Vector3 (0.0f, 2.0f, 0.0f);
			origin = new Vector3 (0.0f, 0.0f, 0.0f);
		}*/

		public Camera (Vector3 lookfrom, Vector3 lookat, Vector3 vup, float vfov, float aspect, float apeture, float focus_dist) {
			lens_radius = apeture / 2.0f;
			float theta = vfov * (float)Math.PI / 180.0f;
			float half_height = (float)Math.Tan (theta / 2.0f);
			float half_width = half_height * aspect;
			origin = lookfrom;
			w = (lookfrom - lookat).unit_vector ();
			u = (Vector3.cross (vup, w)).unit_vector ();
			v = Vector3.cross (w, u);
			lower_left = origin - half_width * focus_dist * u - half_height * focus_dist * v - focus_dist * w;
			horizontal = 2.0f * half_width * focus_dist * u;
			vertical = 2.0f * half_height * focus_dist * v;

		}

		public Ray getRay(float s, float t) {
			Vector3 rd = lens_radius * Utils.random_in_unit_disk ();
			Vector3 offset = u * rd.x () + v * rd.y ();
			return new Ray (origin + offset, lower_left + s * horizontal + t * vertical - origin - offset);
		}
	}
}

