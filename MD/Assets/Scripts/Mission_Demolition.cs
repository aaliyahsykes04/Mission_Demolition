using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;   

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class Mission_Demolition : MonoBehaviour
{
    static public Mission_Demolition S; // Singleton  
    public Text utiLevel;
    public Text utiShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    public int level;
    public int levelMax;
    public int shotTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";


    // Start is called before the first frame update
    void Start()
    {
        S = this; // Set the singleton
        level = 0;
        shotTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        //get rid of old castle if one exists
        if (castle != null)
        {            
            Destroy(castle);
        }
        // destroy projectiles if they exist
        Projectile.DESTROY_PROJECTILES();
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;

    }

    void UpdateGUI()
    {
        utiLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        utiShots.text = "Shots Taken: " + shotTaken;
    }



    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        //See if the level is complete
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            //once the level is complete , stop and go to next level after a delay
            Invoke("NextLevel", 2f);

        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    static public void SHOT_FIRED()
    {
        S.shotTaken++;
    }

    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }



}
