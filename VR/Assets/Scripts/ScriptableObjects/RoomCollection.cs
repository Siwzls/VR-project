using System.Collections.Generic;
using UnityEngine;
using LevelGenaration;

[CreateAssetMenu(fileName = "Default level generation data set", menuName = "Generation data sets/Default level generation data set")]
public class RoomCollection : ScriptableObject
{
    [SerializeField] private Room _startRoom;
    [SerializeField] private List<Room> _rooms;

    public List<Room> Rooms => _rooms;
    public Room StartRoom => _startRoom;
}