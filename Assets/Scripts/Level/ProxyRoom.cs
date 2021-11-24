using System;
using System.Collections;
using System.Collections.Generic;

namespace HackedDesign
{
    [System.Serializable]
    public class ProxyRoom
    {
        public bool isEntry = false;
        public bool isEnd = false;
        public bool isMainChain = false;
        public bool isNearEntry = false;

        public string floor = "";
        public string top = "";
        public string left = "";
        public string bottom = "";
        public string right = "";

        public const string Wall = "w";
        public const string Open = "o";
        public const string Door = "d";
        public const string Exit = "e";
        public const string Entry = "n";
        public const string Any = "a";
        public const string OpenOrDoor = "x";
        public const string OpenOrWall = "y";
        public const string DoorOrWall = "z";

        public const string OpenOptions = ProxyRoom.Open + ProxyRoom.Any + ProxyRoom.OpenOrDoor + ProxyRoom.OpenOrWall;
        public const string DoorOptions = ProxyRoom.Door + ProxyRoom.Any + ProxyRoom.OpenOrDoor + ProxyRoom.DoorOrWall;
        public const string WallOptions = ProxyRoom.Wall + ProxyRoom.Any + ProxyRoom.OpenOrWall + ProxyRoom.DoorOrWall;
        public const string ExitOptions = ProxyRoom.Exit + DoorOptions;
        public const string EntryOptions = ProxyRoom.Entry + DoorOptions;        

        public List<string> bottomLeft = new List<string>();
        public List<string> bottomRight = new List<string>();
        public List<string> topLeft = new List<string>();
        public List<string> topRight = new List<string>();

        // FIXME: Create individual as strings
        public override string ToString() => "" + left + top + bottom + right;
    }


    [System.Serializable]
    public class Corner
    {
        public string name;
    }

    [System.Serializable]
    public class ProxyRow
    {
        public ProxyRoom[] rooms;
    }

}