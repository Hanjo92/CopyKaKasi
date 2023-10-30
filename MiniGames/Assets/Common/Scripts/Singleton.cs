using UnityEngine;

namespace Almond
{
	public class Singleton<T> where T : Singleton<T>, new()
	{
		static T _inst;
		public static T Inst
		{
			get
			{
				if(null == _inst)
					_inst = new T();

				return _inst;
			}
		}
		public override string ToString()
		{
			return typeof(T).ToString() + "_Singleton";
		}
	}

	public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
	{
		static MonoSingleton<T> _inst;
		public static MonoSingleton<T> Inst => _inst;

		private void Awake()
		{
			if(Inst == null)
				_inst = this;
			else
				Destroy(this);
		}

		public override string ToString()
		{
			return typeof(T).ToString() + "_Singleton";
		}
	}
}