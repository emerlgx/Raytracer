using System;

namespace Raytracer
{
	public class Lambertian: Material
	{
		public Texture abledo;

		public Lambertian (Texture a)
		{
			abledo = a;
		}

		public override bool scatter(Ray r_in, hit_record rec, ref Vector3 attenuation, ref Ray scattered) {
			Vector3 target = rec.normal + Utils.random_in_unit_sphere ();
			scattered = new Ray (rec.p, target);
			attenuation = abledo.value (0.0f, 0.0f, ref rec.p);
			return true;
		}
	}
}