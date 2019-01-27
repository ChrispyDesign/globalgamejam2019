public class LumberYardTile : BaseTile
{

    public override void ProcessEndOfTurn()
    {
        if (GameController.instance.GetTreeCount() <= 0)
            return;

        // get the first grown tree tile
        TreesTile tl = null;
        foreach (var t in GameController.instance.GetTiles(typeof(TreesTile)))
            if (((TreesTile)t).CanBeCut())
                tl = (TreesTile)t;

        if (tl == null)
            return;
        tl.Cut(false);
        GameController.worldResources["wood"] += 2;
    }

    public override string GetPrefabName()
    {
        return "LumberYard";
    }
}
