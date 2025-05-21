using UnityEngine;

public class TownHall : Singleton<TownHall>
{
    public int townHallLevel = 1;

   
    public void townHallLevelUp()
    {
        townHallLevel += 1; 
    }

}
