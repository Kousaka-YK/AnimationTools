using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class TimeLinePlayableAsset : PlayableAsset
{
    public ExposedReference<GameObject> talkText;
    // public GameObject talkStr;
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        TimeLinePlayableBehaviour talkPlayable = new TimeLinePlayableBehaviour();

        talkPlayable.talkText1 = talkText.Resolve(graph.GetResolver());

        // talkPlayable.talkStr1 = talkStr;

        return ScriptPlayable<TimeLinePlayableBehaviour>.Create(graph, talkPlayable);
    }
}
