using System;

namespace Raytracer
{
	public class Camo : Texture
	{
		Perlin noise;
		Vector3 color;
		float scale;

		public Camo (){
			noise = new Perlin ();
			color = new Vector3 (1.0f, 1.0f, 1.0f);
			scale = 1.0f;
		}

		public Camo (float s){
			noise = new Perlin ();
			color = new Vector3 (1.0f, 1.0f, 1.0f);
			scale = s;
		}

		public Camo (Vector3 c) {
			color = c;
			noise = new Perlin ();
			scale = 1.0f;
		}

		public Camo (Vector3 c, float s) {
			color = c;
			noise = new Perlin ();
			scale = s;
		}

		public override Vector3 value (float u, float v, ref Vector3 p)
		{
			return color * 0.5f * (1.0f + noise.turb (p * scale));
		}
	}
}
