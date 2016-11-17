using System;

namespace Raytracer
{
	public class Perlin
	{
		static int[] perm_x;
		static int[] perm_y;
		static int[] perm_z;
		static Vector3[] ran_vecs;

		static Perlin () {
			perm_x = perlin_generate_perm ();
			perm_y = perlin_generate_perm ();
			perm_z = perlin_generate_perm ();

			ran_vecs = generate_vecs ();
		}

		public Perlin (){}

		public float noise(Vector3 p) {
			float u = p.x () - (float)Math.Floor (p.x ());
			float v = p.y () - (float)Math.Floor (p.y ());
			float w = p.z () - (float)Math.Floor (p.z ());
			int i = (int)Math.Floor (p.x ());
			int j = (int)Math.Floor (p.y ());
			int k = (int)Math.Floor (p.z ());
			Vector3[,,] c = new Vector3[2,2,2];
			for (int di = 0; di < 2; di++) {
				for (int dj = 0; dj < 2; dj++) {
					for (int dk = 0; dk < 2; dk++) {
						c [di, dj, dk] = ran_vecs [perm_x [(i + di) & 255] ^ perm_y [(j + dj) & 255] ^ perm_z [(k + dk) & 255]];
					}
				}
			}

			return Utils.trilinear_interpolation(c,u,v,w);

		}

		static Vector3[] generate_vecs() {
			Vector3[] vecs = new Vector3[256];
			for (int i = 0; i < 256; i++) {
				vecs [i] = new Vector3 (-1.0f + 2.0f * (float)Utils.rnd.NextDouble(),
					-1.0f + 2.0f * (float)Utils.rnd.NextDouble(),
					-1.0f + 2.0f * (float)Utils.rnd.NextDouble());
				vecs [i] = vecs [i].unit_vector ();
			}

			return vecs;
		}

		static int[] permute(int[] p) {
			int[] temp = p;
			for (int i = p.Length-1; i > 0; i--) {
				int target = (int)(Utils.rnd.NextDouble () * (i + 1));
				int swap = temp [i];
				temp [i] = temp [target];
				temp [target] = swap;
			}
			return temp;
		}

		static int[] perlin_generate_perm() {
			int[] p = new int[256];
			for (int i = 0; i < 256; i++) {
				p [i] = i;
			}
			return permute (p);
		}
	}
}

