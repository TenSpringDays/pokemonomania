using UnityEngine;

public class StoneManager : MonoBehaviour
{
    public static StoneManager instance;
    public Stone[] activeStone;
    //public Stone activeStone;

    int stoneFilling;
    [SerializeField]
    int maximumFilling;
    [SerializeField]
    int idGameLevel;
    [SerializeField]
    int countAlreadyCreatedStone;

    public GameObject[] StoneInThisLevel;
    bool isStillCreatingStone;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stoneFilling = 0;
        countAlreadyCreatedStone = 0;
        activeStone = new Stone[maximumFilling];
        isStillCreatingStone = false;

        StoneInThisLevel = InGameLevelManager.instance.gameLevel[idGameLevel].StoneInThisLevel;
    }

    void Update()
    {
        if (!isStillCreatingStone && stoneFilling < maximumFilling)
            FullSpawningStone();
    }

    void FullSpawningStone()
    {
        isStillCreatingStone = true;
        //CEK INI............................mother
        /*int tempValue = stoneFilling;
        stoneFilling = maximumFilling;

        for (int i = tempValue; i < maximumFilling; i++)
        {
            SpawningStone(i);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.25f);
        isStillCreatingStone = false;*/

        SpawningStone(stoneFilling);
        //Debug.Log("StoneFilling : " + stoneFilling);
        stoneFilling += 1;
        countAlreadyCreatedStone += 1;
        isStillCreatingStone = false;
    }

    void SpawningStone(int idActiveStone)
    {
        int tempValue = Random.Range(1, 100);
        int choosenId = tempValue / (100 / StoneInThisLevel.Length);

        float yPosition = 6f;
        if (stoneFilling > 0)
            yPosition = activeStone[stoneFilling - 1].transform.position.y + 2f;

        GameObject CreatedStone = Instantiate(StoneInThisLevel[choosenId], new Vector3(0f, yPosition, -1f), new Quaternion()) as GameObject;
        CreatedStone.GetComponent<Stone>().StoneID = idActiveStone;
        activeStone[idActiveStone] = CreatedStone.GetComponent<Stone>();
    }

    public void BreakingStone()
    {
        Destroy(activeStone[0].gameObject);
        for (int i = 0; i < activeStone.Length - 1; i++)
        {
            activeStone[i + 1].StoneID = i;
            activeStone[i] = activeStone[i + 1];
        }
        activeStone[activeStone.Length - 1] = null;
        stoneFilling -= 1;
    }

    public void makeThisStoneBeingFirst(GameObject stone)
    {
        activeStone[0] = stone.GetComponent<Stone>();
    }
}