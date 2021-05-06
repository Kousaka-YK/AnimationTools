using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

// A behaviour that is attached to a playable
public class TimeLinePlayableBehaviour : PlayableBehaviour
{
    public GameObject talkText1;
    // public GameObject talkStr1;
    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        // _dialog = dialog.Resolve(playable.GetGraph().GetResolver());
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {

    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // talkText1 = talkStr1;
		talkText1.SetActive(true);
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        talkText1.SetActive(false);
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {

    }
}
