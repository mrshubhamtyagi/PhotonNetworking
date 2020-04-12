using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby Instance;

    public string roomName;
    public int maxPlayers = 1;

    public List<string> rooms;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }


    [ContextMenu("ConnectToMaser")]
    public void ConnectToMaser()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    [ContextMenu("CreateRoom")]
    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Not Connected to Master");
            return;
        }

        int num = Random.Range(1, 1000);
        string _roomName = roomName.Trim() + " " + num;

        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4
        };

        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    [ContextMenu("JoinRoom")]
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }


    [ContextMenu("LeaveRoom")]
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Leaved Room");
    }

    public Room GetCurrentRoom()
    {
        return PhotonNetwork.CurrentRoom;
    }

    public void IsConnectedToRoom()
    {

    }














    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room > " + PhotonNetwork.CurrentRoom.Name);
        rooms.Add(GetCurrentRoom().Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Created room Failed > " + roomName);
    }

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Not Connected to Master");
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("Joined Room > " + GetCurrentRoom().Name);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Joined room Failed because > " + message);
    }

    #endregion

}
