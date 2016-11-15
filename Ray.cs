using System;

namespace Raytracer
{
	// class to represent an individual ray
	public class Ray
	{
		Vector3 A;
		Vector3 B;
		public float _time;

		public Ray () {
			A = new Vector3 ();
			B = new Vector3 ();
			_time = 0.0f;
		}

		public Ray (Vector3 a, Vector3 b) {
			A = a;
			B = b;
			_time = 0.0f;
		}

		public Ray (Vector3 a, Vector3 b, float ti) {
			A = a;
			B = b;
			_time = ti;
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

