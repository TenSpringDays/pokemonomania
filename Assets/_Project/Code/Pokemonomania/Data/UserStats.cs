using UnityEngine.Serialization;


namespace Pokemonomania.Data
{
    [System.Serializable]
    public class UserStats
    {
        public int Version;
        
        [System.Serializable]
        public class Values
        {
            public int Score;
            public int Combo;
            public float Elapsed;
        }


        public Values Best = new ();
        public Values Last = new ();
        public EndGameStatus LastEndGameStatus;
    }


}