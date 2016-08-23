using System;

namespace Raytracer
{
	public struct hit_record {
		public float t;
		public Vector3 p;
		public Vector3 normal;
	}

	public class Hitable
	{
		public virtual bool hit(Ray r, float t_min, float t_max, ref hit_record rec) {
			return false;
		}
	}
}

