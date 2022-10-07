using UnityEngine;

public static class RaycastEnhanced
{
    //This Raycast method wrap the Physics2D.Raycast() and provide some extra
	//functionality
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 rayDirection, float length, LayerMask mask,bool drawDebugRaycasts)
	{
		//Send out the desired raycasr and record the result
		RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, length, mask);

		//If we want to show debug raycasts in the scene...
		if (drawDebugRaycasts)
		{
			//...determine the color based on if the raycast hit...
			Color color = hit ? Color.red : Color.green;
			//...and draw the ray in the scene view
			Debug.DrawRay(origin, rayDirection * length, color);
		}

		//Return the results of the raycast
		return hit;
	}
}
