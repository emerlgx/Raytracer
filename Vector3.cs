using System;

namespace Raytracer
{
	public class Vector3
	{
		float[] e;
		// constructors
		public Vector3 (){
			e = new float[3];
			e [0] = 0.0f;
			e [1] = 0.0f;
			e [2] = 0.0f;
		}
		public Vector3 (float e0, float e1, float e2) {
			e = new float[3];
			e [0] = e0;
			e [1] = e1;
			e [2] = e2;
		}

		//access functions
		public float x() { return e [0];}
		public float y() { return e [1];}
		public float z() { return e [2];}
		public float r() { return e [0];}
		public float g() { return e [1];}
		public float b() { return e [2];}

		// simple vector operators
		public static Vector3 operator +(Vector3 a, Vector3 b) {
			return new Vector3 (a.x() + b.x(), a.y() + b.y(), a.z() + b.z());
		}

		public static Vector3 operator -(Vector3 a, Vector3 b) {
			return new Vector3 (a.x() - b.x(), a.y() - b.y(), a.z() - b.z());
		}

		public static Vector3 operator *(Vector3 a, Vector3 b) {
			return new Vector3 (a.x() * b.x(), a.y() * b.y(), a.z() * b.z());
		}

		public static Vector3 operator *(Vector3 a, float f) {
			return new Vector3 (a.x() * f, a.y() * f, a.z() * f);
		}

		public static Vector3 operator *(float f, Vector3 a) {
			return new Vector3 (a.x() * f, a.y() * f, a.z() * f);
		}

		public static Vector3 operator /(Vector3 a, Vector3 b) {
			return new Vector3 (a.x() / b.x(), a.y() / b.y(), a.z() / b.z());
		}

		public static Vector3 operator /(Vector3 a, float f) {
			return new Vector3 (a.x() / f, a.y() / f, a.z() / f);
		}

		public float length () {
			return (float)Math.Sqrt (x()*x() + y()*y() + z()*z());
		}

		public float squared_length() {
			return (x()*x() + y()*y() + z()*z());
		}

		public Vector3 unit_vector() {
			return new Vector3(x() / length (),
			                   y() / length (),
			                   z() / length ());
		}

		// other operations
		public static float dot (Vector3 a, Vector3 b) {
			return (a.x () * b.x () + a.y () * b.y () + a.z () * b.z ());
		}

		public static Vector3 cross(Vector3 a, Vector3 b) {
			return new Vector3 (a.y () * b.z () - a.z () * b.y (),
			                    -(a.x () * b.z () - a.z () * b.x ()),
			                    a.x () * b.y () - a.y () * b.x ());
		}
	}
}

