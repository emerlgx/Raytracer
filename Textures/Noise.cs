using System;

namespace Raytracer
{
	public class Noise : Texture
	{
		Perlin noise;
		Vector3 color;

		public Noise (){
			noise = new Perlin ();
			color = new Vector3 (1.0f, 1.0f, 1.0f);
		}

		public Noise (Vector3 c) {
			color = c;
			noise = new Perlin ();
		}

		public override Vector3 value (float u, float v, ref Vector3 p)
		{
			return color * noise.noise (ref p);
		}
	}
}

