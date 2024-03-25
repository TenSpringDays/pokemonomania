using UnityEngine;


namespace StoneBreaker
{
    [System.Serializable]
    public class PokemonControllerConfig
    {
        public float ItemHeight = 1f;
        public int MaxItems = 10;
        public float FallSpeed = 3f;
        public float PositionPower = 10f;
        public Vector2Int SpawnSequenceRange = new(3, 15);
    }
}