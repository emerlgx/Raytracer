using System;

namespace Raytracer
{
	public class Perlin
	{
		static int[] perm_x;
		static int[] perm_y;
		static int[] perm_z;
		static float[] ran_float;

		static Perlin () {
			ran_float = generate ();
			perm_x = perlin_generate_perm ();
			perm_y = perlin_generate_perm ();
			perm_z = perlin_generate_perm ();

		}

		public Perlin (){}

		public float noise(ref Vector3 p) {
			/*float u = p.x () - (float)Math.Floor (p.x ());
			float v = p.y () - (float)Math.Floor (p.y ());
			float w = p.z () - (float)Math.Floor (p.z ());*/
			int i = (int)(4*(int)p.x() & 255);
			int j = (int)(4*(int)p.y() & 255);
			int k = (int)(4*(int)p.z() & 255);
			return ran_float[perm_x[i] ^ perm_y[j] ^ perm_z[k]];

		}

		static float[] generate() {
			float[] arr = new float[256];
			for (int i = 0; i < 256; i++) {
				arr [i] = (float)Utils.rnd.NextDouble ();
			}
			return arr;
		}

		static int[] permute(int[] p) {
			int[] temp = p;
			for (int i = 0; i < p.Length; i++) {
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

