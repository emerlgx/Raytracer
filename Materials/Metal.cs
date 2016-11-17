using System;

namespace Raytracer
{
	public class Metal : Material
	{
		public Texture abledo;
		public float fuzz;

		public Metal (Texture a)
		{
			abledo = a;
			fuzz = 0.0f;
		}

		public Metal (Texture a, float f)
		{
			abledo = a;

			if (f < 0.0f) {
				fuzz = 0.0f;
			} else if (f > 1.0f) {
				fuzz = 1.0f;
			} else {
				fuzz = f;
			}
		}

		public override bool scatter(Ray r_in, hit_record rec, ref Vector3 attenuation, ref Ray scattered) {
			Vector3 reflected = Utils.reflect (r_in.direction().unit_vector(), rec.normal);
			scattered = new Ray (rec.p, reflected + fuzz*Utils.random_in_unit_sphere());
			attenuation = abledo.value (0.0f, 0.0f, ref rec.p);
			return (Vector3.dot (scattered.direction (), rec.normal) > 0.0f);
		}


	}
}

