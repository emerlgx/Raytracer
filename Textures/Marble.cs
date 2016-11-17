using System;

namespace Raytracer
{
	public class Marble : Texture
	{
		Perlin noise;
		Vector3 color;
		float scale;

		public Marble (){
			noise = new Perlin ();
			color = new Vector3 (1.0f, 1.0f, 1.0f);
			scale = 1.0f;
		}

		public Marble (float s){
			noise = new Perlin ();
			color = new Vector3 (1.0f, 1.0f, 1.0f);
			scale = s;
		}

		public Marble (Vector3 c) {
			color = c;
			noise = new Perlin ();
			scale = 1.0f;
		}

		public Marble (Vector3 c, float s) {
			color = c;
			noise = new Perlin ();
			scale = s;
		}

		public override Vector3 value (float u, float v, ref Vector3 p)
		{
			return color * 0.5f * (1.0f + (float)Math.Sin(scale*p.z() + 10.0f * noise.turb(p)));

		}
	}
}
