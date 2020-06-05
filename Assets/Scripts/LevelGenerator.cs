using UnityEngine;

public class LevelGenerator : MonoBehaviour {

 
    public LevelStats level;
	public ColorToPrefab[] colorMappings;

	// Use this for initialization
	void Awake () {
		GenerateLevel();  
	}

	void GenerateLevel ()
	{
		for (int x = 0; x < level.levelMap.width; x++)
		{
			for (int y = 0; y < level.levelMap.height; y++)
			{
                 
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile (int x, int y)
	{
		Color pixelColor = level.levelMap.GetPixel(x, y);
         
        if (pixelColor.a == 0)
		{
            // The pixel is transparrent. Let's ignore it!
 
			return;
		}

		foreach (ColorToPrefab colorMapping in colorMappings)
		{
			if (colorMapping.color.Equals(pixelColor))
			{
                Debug.Log("SPAWN");
                Vector2 position = new Vector2(x, y);
				Instantiate(colorMapping.prefab, position, Quaternion.identity,transform);
			}
		}
	}
	
}
