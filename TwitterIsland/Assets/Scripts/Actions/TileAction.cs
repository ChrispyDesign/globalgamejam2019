using System.Collections.Generic;
using UnityEngine;

public abstract class TileAction : MonoBehaviour
{

    protected List<System.Type> affectedTypes = new List<System.Type>();
    protected List<System.Func<bool>> conditionals;
    protected int cost = 1;

    // EXAMPLE OF HOW TO SET UP ACTION CLASS
    void setup()
    {
        // make this action only affect some tiles
        affectedTypes.Add(typeof(BaseTile)); // replace BaseTile with the tile type you want

        // make this action only performable if a condition is met
        conditionals.Add(() =>
        {
            // we can only perform if the result of this function returns true
            return GameController.worldValues["air"] > 0.5f;
        });
    }

    public abstract void Perform(BaseTile onTile);

    public bool CanPerform()
    {
        // check that there are tiles to be performed on
        if (GetAffectedTiles().Count == 0)
            return false;
        
        // requires all of the conditionals to be true
        foreach(var f in conditionals)
            if (!f())
                return false;

        return true;
    }

    public List<GameObject> GetAffectedTiles()
    {
        List<GameObject> objects = new List<GameObject>();
        foreach (var t in affectedTypes)
        {
            var objs = FindObjectsOfType(t);
            foreach (var o in objs)
                objects.Add(((MonoBehaviour)o).gameObject);
        }
        return objects;
    }

}
