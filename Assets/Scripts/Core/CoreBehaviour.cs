using UnityEngine;

namespace MathApp
{
	public abstract class CoreBehaviour : MonoBehaviour
	{
		public new string Name
		{
			get
			{
#if UNITY_EDITOR
				if (Application.isPlaying == false)
					return base.name;
#endif
				if (nameCached == false)
				{
					cachedName = base.name;
					nameCached = true;
				}

				return cachedName;
			}
			set
			{
				if (string.CompareOrdinal(cachedName, value) != 0)
				{
					base.name = value;
					cachedName = value;
					nameCached = true;
				}
			}
		}

		public new GameObject GameObject
		{
			get
			{
#if UNITY_EDITOR
				if (Application.isPlaying == false)
					return base.gameObject;
#endif
				if (gameObjectCached == false)
				{
					cachedGameObject = base.gameObject;
					gameObjectCached = true;
				}

				return cachedGameObject;
			}
		}

		public new Transform Transform
		{
			get
			{
#if UNITY_EDITOR
				if (Application.isPlaying == false)
					return base.transform;
#endif
				if (transformCached == false)
				{
					cachedTransform = base.transform;
					transformCached = true;
				}

				return cachedTransform;
			}
		}

		private string cachedName;
		private bool nameCached;
		private GameObject cachedGameObject;
		private bool gameObjectCached;
		private Transform cachedTransform;
		private bool transformCached;
	}
}
