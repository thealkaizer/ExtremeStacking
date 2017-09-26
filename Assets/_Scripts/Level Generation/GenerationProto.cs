using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GenerationProto : MonoBehaviour {

	//Other scripts
	CameraManager cameraScript;
	

	bool fallHappening = false;
	int gridSize = 15;
	int numberOfLevels = 100, currentLevel = 1;
	public GameObject tileOriginal, emptyObject, player;
	float tileSize, wallsThickness;
	float shakeDuration = 3f, fallDuration = 0.50f, settleDuration = 0.7f;
	public GameObject[] allTiles;
	public GameObject[] allStacks;
	public GameObject oldStacks;
	Vector3 shakeVector = new Vector3(0.5f, 0f, 0.5f);

	// Use this for initialization
	void Start () {
		cameraScript = GetComponent<CameraManager>();
		tileSize = tileOriginal.transform.localScale.x;
		wallsThickness = tileSize - 1f;
		allTiles = new GameObject[gridSize * gridSize];
		allStacks = new GameObject[numberOfLevels];

		StartCoroutine("GameLoop");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator GameLoop()
	{
		for(int i = 1; i <= 100; i = currentLevel) {
			if (i == 1) {
				CreateFirstStack();
			} else {
				CreateStack();
			}
			yield return new WaitForSeconds(7f);
		}

	}

	public void CreateFirstStack() {
		fallHappening = true;

		//Create old stack object
		GameObject instancedGroupedStack = Instantiate(emptyObject, Vector3.zero, Quaternion.Euler(Vector3.zero));
		instancedGroupedStack.name = "Old Stacks";
		oldStacks = instancedGroupedStack;

		//Create stack
		GameObject instancedStack = Instantiate(emptyObject, Vector3.zero, Quaternion.Euler(Vector3.zero));
		instancedStack.name = "GeneratedStack_" + currentLevel;
		allStacks[currentLevel] = instancedStack;
		//Create tiles
		for (int x = 0; x < gridSize; x++) {
			for (int z = 0; z < gridSize; z++) {
				GameObject instancedTile = Instantiate(tileOriginal, new Vector3(x * tileSize, -0.3f, z * tileSize), Quaternion.Euler(Vector3.zero));
				instancedTile.transform.name = "tile_" + x + ", " + z;
				instancedTile.transform.SetParent(instancedStack.transform);
				allTiles[x + z * gridSize] = instancedTile;
			}
		}
		currentLevel++;
		fallHappening = false;
		Instantiate(player, new Vector3(7f, 1f, 7f), Quaternion.identity);
	}

	public void CreateStack() {
		fallHappening = true;
		cameraScript.ZoomOut();
		//Create stack
		GameObject instancedStack = Instantiate(emptyObject, new Vector3(0f, 15f, 0f), Quaternion.Euler(Vector3.zero));
		instancedStack.name = "GeneratedStack_" + currentLevel;
		allStacks[currentLevel] = instancedStack;

		//Create tiles
		for (int x = 0; x < gridSize; x++) {
			for (int z = 0; z < gridSize; z++) {
				GameObject instancedTile = Instantiate(tileOriginal, new Vector3(x * tileSize, 15f, z * tileSize), Quaternion.Euler(Vector3.zero));
				instancedTile.transform.name = "tile_" + x + ", " + z;
				instancedTile.transform.SetParent(instancedStack.transform);
				allTiles[x + z * gridSize] = instancedTile;
			}
		}
		currentLevel++;

		//Make holes
		int numberHoles = Random.Range(2, 6);
		for (int i = 0; i < 6; i++) {
			DestroyTiles();
		}

		//Collapse
		StartCoroutine(CollapseStack(allStacks[currentLevel - 1], allStacks[currentLevel - 2]));


		
	}

	IEnumerator CollapseStack(GameObject newStack, GameObject oldStack) {
		oldStack.transform.SetParent(oldStacks.transform);

		//Shake
		Tween Tween1 = newStack.transform.DOShakePosition(shakeDuration, shakeVector, 10, 90, false);
		//Tween Tween1 = newStack.transform.DOMoveY(13f, shakeDuration, false);
		yield return Tween1.WaitForCompletion();

		//Fall
		Tween1 = newStack.transform.DOMoveY(0f, fallDuration, false);
		yield return Tween1.WaitForCompletion();
		Tween1 = newStack.transform.DOShakePosition (0.4f, shakeVector, 10, 90, false);
		cameraScript.ZoomIn();
		yield return Tween1.WaitForCompletion();

		//Settle
		newStack.transform.DOMoveY(-0.3f, settleDuration, false);
		Tween1 = oldStacks.transform.DOMoveY(oldStacks.transform.position.y - 0.3f, settleDuration, false);
		yield return Tween1.WaitForCompletion();

		fallHappening = false;
	}

	void DestroyTiles() {
		int tileToDestroy = Random.Range(0, allTiles.Length);
		if (allTiles[tileToDestroy] != null){
			Destroy(allTiles[tileToDestroy]);
			allTiles[tileToDestroy] = null;
		} else {
			DestroyTiles();
		}
	}
}
