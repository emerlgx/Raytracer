using System;
using System.Collections;
namespace Raytracer
{
	public class Hitable_List : Hitable
	{
		public ArrayList hitables;

		public Hitable_List (){
			hitables = new ArrayList ();
		}
		public Hitable_List (ArrayList h){
			hitables = h;
		}

		public override bool hit(Ray r, float t_min, float t_max, ref hit_record rec) {
			hit_record temp_rec = rec;
			bool hit_anything = false;
			double closest = t_max;

			for (int i = 0; i < hitables.Count; i++) {
				//cycle through all hitables and return the hit closest to the camera
				if (((Hitable)hitables [i]).hit (r, t_min, (float)closest, ref temp_rec)) {
					hit_anything = true;
					closest = temp_rec.t;
					rec = temp_rec;
				}
			}

			return hit_anything;
		}

	}
}

