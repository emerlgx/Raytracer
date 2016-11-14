using System;

namespace Raytracer
{
	public class Dielectric: Material
	{
		float reflect_index;

		public Dielectric (float r)
		{
			reflect_index = r;
		}

		public override bool scatter(Ray r_in, hit_record rec, ref Vector3 attenuation, ref Ray scattered) {
			Vector3 outward_norm;
			Vector3 reflected = Utils.reflect (r_in.direction (), rec.normal);
			float ni_over_nt;
			attenuation = new Vector3 (1.0f, 1.0f, 0.0f);
			Vector3 refracted = new Vector3();
			float reflect_prob = 0.0f;
			float cosine = 0.0f;

			if (Vector3.dot (r_in.direction (), rec.normal) > 0.0f) {
				outward_norm = -1.0f * rec.normal;
				ni_over_nt = reflect_index;
				cosine = reflect_index*Vector3.dot(r_in.direction(), rec.normal) / r_in.direction().length();
			} else {
				outward_norm = rec.normal;
				ni_over_nt = 1.0f / reflect_index;
				cosine = -1.0f*Vector3.dot(r_in.direction(), rec.normal) / r_in.direction().length();
			}

			// determine probability for reflecting vs refracting based on if a refraction is possible
			if (Utils.refract (r_in.direction (), outward_norm, ni_over_nt, ref refracted)) {
				reflect_prob = Utils.schlick (cosine, reflect_index);
			} else {
				scattered = new Ray (rec.p, reflected);
				reflect_prob = 1.0f;
			}

			// decide whether to reflect or refract
			if (Utils.rnd.NextDouble () < reflect_prob) {
				scattered = new Ray (rec.p, reflected);
			} else {
				scattered = new Ray (rec.p, refracted);
			}
			return true;
		}
	}
}