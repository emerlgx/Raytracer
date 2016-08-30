using System;

namespace Raytracer
{
	// class to represent an individual ray
	public class Ray
	{
		Vector3 A;
		Vector3 B;

		public Ray () {
			A = new Vector3 ();
			B = new Vector3 ();
		}

		public Ray (Vector3 a, Vector3 b) {
			A = a;
			B = b;
		}

		public Vector3 origin() {
			return A;
		}

		public Vector3 direction() {
			return B;
		}

		public Vector3 point(float t) {
			return (A + t*B);
		}
	}
}

