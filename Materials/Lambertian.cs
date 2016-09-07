using System;

namespace Raytracer
{
	public class Lambertian: Material
	{
		public Vector3 abledo;

		public Lambertian (Vector3 a)
		{
			abledo = a;
		}

		public override bool scatter(Ray r_in, hit_record rec, ref Vector3 attenuation, ref Ray scattered) {
			Vector3 target = rec.normal + MainClass.random_in_unit_sphere ();
			scattered = new Ray (rec.p, target);
			attenuation = abledo;
			return true;
		}
	}
}

