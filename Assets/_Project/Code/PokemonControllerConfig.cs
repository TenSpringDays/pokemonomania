using UnityEngine;


namespace StoneBreaker
{
    [System.Serializable]
    public class PokemonControllerConfig
    {
        public float ItemHeight = 1f;
        public int MaxItems = 10;
        public float MinSpeed = 3f;
        public float MaxSpeed = 10f;
        public Vector2Int SpawnSequenceRange = new(3, 15);
    }
}