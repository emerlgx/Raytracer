using System;

namespace Raytracer
{
	public class Checker : Texture
	{
		Texture even;
		Texture odd;
		float size;

		public Checker () {
			even = new Const_Texture (new Vector3 (0.0f, 0.0f, 0.0f));
			odd = new Const_Texture (new Vector3 (1.0f, 1.0f, 1.0f));
			size = 10.0f;
		}

		public Checker (Texture e, Texture o) {
			even = e;
			odd = o;
			size = 10.0f;
		}

		public Checker (Texture e, Texture o, float s) {
			even = e;
			odd = o;
			size = s;
		}

		public override Vector3 value(float u, float v, ref Vector3 p)
		{
			float sins = (float)(Math.Sin(p.x()*size) * Math.Sin(p.y()*size) * Math.Sin(p.z()*size));
			if (sins > 0.0f) {
				return even.value (u, v, ref p);
			} else {
				return odd.value (u, v, ref p);
			}
		}
	}
}

