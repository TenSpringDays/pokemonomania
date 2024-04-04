namespace Pokemonomania.Data
{
    [System.Serializable]
    public class GameSceneData
    {
        public sbyte[] Pokemons = new sbyte[2] { 0, 1 };
        public sbyte LeftButton = 0;
        public sbyte RightButton = 1;
        public sbyte SpectialButton = -1;
    }
}