using UnityEngine;

public class VideoTest : MonoBehaviour
{
	void OnGUI()
	{
#if UNITY_N3DS
        if (UnityEngine.N3DS.Video.IsPlaying == false)
		{
			if (GUI.Button(buttonRect1, "Play (Upper Screen)"))
			{
				UnityEngine.N3DS.Video.Play(Application.streamingAssetsPath + "/UpperScreen.moflex", N3dsScreen.Top);
			}
			else if (GUI.Button(buttonRect2, "Play (Lower Screen)"))
			{
				UnityEngine.N3DS.Video.Play(Application.streamingAssetsPath + "/LowerScreen.moflex", N3dsScreen.Bottom);
                Debug.Log(Application.streamingAssetsPath);
			}
		}
		else
		{
			if (GUI.Button(buttonRect1, "Stop"))
			{
				UnityEngine.N3DS.Video.Stop();
			}

			if (UnityEngine.N3DS.Video.IsPaused == false)
			{
				if (GUI.Button(buttonRect2, "Pause"))
				{
					UnityEngine.N3DS.Video.Pause();
				}
			}
			else
			{
				if (GUI.Button(buttonRect2, "Resume"))
				{
					UnityEngine.N3DS.Video.Resume();
				}
			}
		}
#endif
	}

	private Rect buttonRect1 = new Rect(60, 40, 200, 50);
	private Rect buttonRect2 = new Rect(60, 130, 200, 50);
}
