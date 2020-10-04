using Game.Components;
using System;

namespace Game.Utils
{
    [Serializable]
    class DamageSerializableDictionary : SerializableDictionary<DamageType, float> { }
    [Serializable]
    class ResistSerializableDictionary : SerializableDictionary<DamageType, Resist> { } 
}
