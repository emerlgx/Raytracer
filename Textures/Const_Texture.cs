using System;

namespace Raytracer
{
	public class Const_Texture : Texture
	{
		Vector3 color;

		public Const_Texture ()
		{
			color = new Vector3 (1.0f, 0.0f, 0.5f);
		}

		public Const_Texture(Vector3 c) {
			color = c;
		}

		public override Vector3 value(float u, float v, ref Vector3 p)
		{
			return color;
		}
	}
}

