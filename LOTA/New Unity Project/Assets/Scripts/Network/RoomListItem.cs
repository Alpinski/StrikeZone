using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;


public class RoomListItem : MonoBehaviour {


    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
    public JoinRoomDelegate joinRoomDelegate;

    private MatchInfoSnapshot match;

    [SerializeField]
    private Text roomNameText;

    public void Setup (MatchInfoSnapshot _match, JoinRoomDelegate joinRoomCallBack)
    {
        match = _match;
        joinRoomDelegate = joinRoomCallBack;
        roomNameText.text = match.name + "(" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom()
    {
        joinRoomDelegate.Invoke(match);
    }

}
