using System;

namespace Raytracer
{
	public class Texture
	{
		public virtual Vector3 value(float u, float v, ref Vector3 p)
		{
			// should return an ugly pink color
			return new Vector3 (1.0f, 0.0f, 0.5f);
		}
	}
}

