using System;

namespace Raytracer
{
	public class Material
	{
		public virtual bool scatter(Ray r_in, hit_record rec, ref Vector3 attenuation, ref Ray scattered) {
			return false;
		}
	}
}

