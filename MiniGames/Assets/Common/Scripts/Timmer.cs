using System;

namespace Almond
{
	public class Timmer
	{
		private readonly float endTime;
		private float fTime;
		public bool enable { get; private set; }

		public Action onTimeEnd;

		public Timmer(float _endTime, float _fTime = 0, Action timeEndAction = null)
		{
			endTime = _endTime;
			fTime = _fTime;
			onTimeEnd = timeEndAction;
			enable = false;
		}

		public void Init()
		{
			fTime = 0;
			enable = false;
		}

		public void Resume()
		{
			enable = true;
		}

		public void Start(float _fTime = 0)
		{
			fTime = _fTime;
			enable = true;
		}

		public void Stop()
		{
			enable = false;
		}
		public void Update(float _fTime)
		{
			if(enable)
				fTime += _fTime;
			if(fTime > endTime)
			{
				onTimeEnd?.Invoke();
			}
		}
	}
}