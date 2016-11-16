using System;
using System.Collections.Generic;
using System.IO;

namespace Raytracer
{
	public class BVH_Node : Hitable
	{
		Hitable left;
		Hitable right;
		AABB box;

		public BVH_Node ()
		{
		}

		public BVH_Node (List<Hitable> l, float t0, float t1) {

			// randomly select an axis to sort child nodes by
			int axis = Utils.rnd.Next (3);
			if (axis == 0) {
				//x
				l.Sort(Utils.hitable_box_compare_x);
			} else if (axis == 1) {
				//y
				l.Sort(Utils.hitable_box_compare_y);
			} else {
				//z
				l.Sort(Utils.hitable_box_compare_z);
			}

			// build the tree
			if (l.Count == 1) {
				left = l [0];
				right = l [0];
			} else if (l.Count == 2) {

				left = l [0];
				right = l [1];
			} else {
				List<Hitable> l_hitables = l.GetRange (0, (int)Math.Floor(l.Count / 2.0f) + 1);
				List<Hitable> r_hitables = l.GetRange (l_hitables.Count, l.Count - l_hitables.Count);
				left = new BVH_Node (l_hitables, t0, t1);
				right = new BVH_Node (r_hitables, t0, t1);
			}

			AABB box_left = new AABB ();
			AABB box_right = new AABB ();

			// ensure that there is a possible bounding box before continuing
			if(!left.bounding_box(t0,t1, ref box_left) || !right.bounding_box(t0,t1,ref box_right)) {
				Console.Error.WriteLine("No possible bounding box in BVH constructor!");
				List<Hitable> l_hitables = l.GetRange (0, (int)Math.Floor(l.Count / 2.0f) + 1);
				List<Hitable> r_hitables = l.GetRange (l_hitables.Count, l.Count - l_hitables.Count);
				Console.Error.WriteLine(l_hitables.Count + " " + r_hitables.Count);
			}
			box = AABB.surrounding_box (box_left, box_right);

		}

		public override bool hit(Ray r, float t_min, float t_max, ref hit_record rec) {
			if (box.hit (r, t_min, t_max)) {
				hit_record left_rec = new hit_record();
				hit_record right_rec = new hit_record();
				bool hit_left = left.hit (r, t_min, t_max, ref left_rec);
				bool hit_right = right.hit (r, t_min, t_max, ref right_rec);

				if (hit_left && hit_right) {
					if (left_rec.t < right_rec.t) {
						rec = left_rec;
					} else {
						rec = right_rec;
					}
					return true;
				} else if (hit_left) {
					rec = left_rec;
					return true;
				} else if (hit_right) {
					rec = right_rec;
					return true;
				} else {
					return false;
				}

			} else {
				return false;
			}
		}

		public override bool bounding_box(float t0, float t1, ref AABB b) {
			b = box;
			return true;
		}
	}
}

