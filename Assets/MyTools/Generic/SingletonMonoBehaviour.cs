using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
	private static T instance;
	public bool preInstantiate;

	public static T Instance {
		get {
			if (instance == null) {
				T temp = FindObjectOfType<T>();
				if (temp != null)
					instance = temp;
                else
                {
					instance = new GameObject().AddComponent<T>();
					instance.gameObject.name = instance.GetType().Name;
				}
			}
			return instance;
		}
	}

	public virtual void Awake () {
		//if (preInstantiate)
		instance = this as T;
	}

	private void Reset () {
		preInstantiate = true;
	}

	public static bool Exists () {
		return (instance != null);
	}
}