using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
public struct CharacterState : INetworkSerializable, IEquatable<CharacterState> {
    public ulong clientID;
    public int characterID;
    public bool isPicked;
    public CharacterState(ulong clientID, int characterID = -1, bool isPicked = false) {
        this.clientID = clientID;
        this.characterID = characterID;
        this.isPicked = isPicked;
    }
    public bool Equals(CharacterState other) {
        return clientID == other.clientID && characterID == other.characterID && isPicked == other.isPicked;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        serializer.SerializeValue(ref clientID);
        serializer.SerializeValue(ref characterID);
        serializer.SerializeValue(ref isPicked);
    }
}
