using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;

public class WorldController : MonoBehaviour {

    public GameObject gameMenu;
    public GameObject convertMenu;
    public GameObject headMountedDisplay;

    private List<GameObject> menuPath = new List<GameObject>();

    public GameObject savingsPilesParent;
    public GameObject categoriesParent;
    private Category mostRecentSavingsPile;
    private bool atLeastOneSavingsPileExists = false;
    private Category generalMoneyPile;
    private Category pointsPile;
    public GameObject store;
	private Store storeScript;
    public GameObject leftController;
    public GameObject rightController;
    private SteamVR_Controller.Device leftControllerDevice;
    private SteamVR_Controller.Device rightControllerDevice;
    private SteamVR_TrackedObject leftTrackedController;
    private SteamVR_TrackedObject rightTrackedController;


    private int numberOfSavingsPiles = 0;

    SavingsPile createSavingsPile(Vector3 position, Quaternion rotation) {
        atLeastOneSavingsPileExists = true;
        SavingsPile savingsPile = new SavingsPile("Savings Pile", 1000.0f, 100.0f, savingsPilesParent, position, rotation);
        numberOfSavingsPiles++;
        savingsPile.wc = this;
        return savingsPile;
    }

    Category createGeneralMoneyPile(Vector3 position, Quaternion rotation) {
        generalMoneyPile = new Category("Free Money", 2827.0f, categoriesParent, position, rotation);
        generalMoneyPile.wc = this;
        return generalMoneyPile;
    }

    Category createPointsPile(Vector3 position, Quaternion rotation) {
        pointsPile = new Category("Points", 0.0f, categoriesParent, position, rotation);

        return generalMoneyPile;
    }

    public void addPoints(float numPoints) {
        pointsPile.addMoney(numPoints);
    }

    void Awake() {
        createGeneralMoneyPile(new Vector3(0, 0, 0), Quaternion.identity);
        createPointsPile(new Vector3(7, 0, 0), Quaternion.identity);
        storeScript = store.GetComponent<Store>();
        storeScript.pointsScript = pointsPile;
    }

    /* Key mappings for actions
	 * WASD or arrow keys: Move the camera around
	 * C: Add a new category/savings pile
	 * X: Convert the money in the most recently created category into PS4 units
	 * Z: Convert money to Starbucks cups
	 * 0: Add 115 to the most recently created category
	 * 1: Add 1 to the most recently created category
	 * 2: Add 10 to the most recently created category
	 * 3: Add 100 to the most recently created category
	 * 4: Add 1000 to the most recently created category
	 */

    // Keeps track of inputs coming in from the real world. In-game input (like from menus and such) are handled within the respondToInGameInput method
    void Update() {
        respondToKeyboardInputs();
        respondToMotionControllerInput();

    }

    private void respondToMotionControllerInput()
    {
        leftTrackedController = leftController.GetComponent<SteamVR_TrackedObject>();
        rightTrackedController = rightController.GetComponent<SteamVR_TrackedObject>();
        leftControllerDevice = SteamVR_Controller.Input((int)leftTrackedController.index);
        rightControllerDevice = SteamVR_Controller.Input((int)rightTrackedController.index);

        if (leftControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) || rightControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("Joyustisjoiawefj;awjioefio;j;awefi");
        }
        if (leftControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) || rightControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            motionControllerMenuButtonPressed();
        }

    }

    private void motionControllerMenuButtonPressed()
    {
        Debug.Log("Menu button pressed");
        if (menuPath.Count > 0)
        {
            hideMenu(menuPath[menuPath.Count - 1]);
            menuPath.Clear();
        }
        else
        {
            showMenu(gameMenu);
            menuPath.Add(gameMenu);
        }
    }

    private void showMenu(GameObject menu)
    {
        setMenuPositionInFrontOfCamera(headMountedDisplay, menu);
        menu.SetActive(true);
    }

    private void hideMenu(GameObject menu)
    {
        menu.SetActive(false);
    }


    //Menu given is the current menu
    private void goBackToPreviousMenu()
    {
        if (menuPath.Count <= 1) return;

        hideMenu(menuPath[menuPath.Count - 1]);
        menuPath.RemoveAt(menuPath.Count - 1);
        showMenu(menuPath[menuPath.Count - 1]);

    }

    private void goToNextMenu(GameObject menu)
    {
        hideMenu(menuPath[menuPath.Count - 1]);
        showMenu(menu);
        menuPath.Add(menu);
    }

    private void setMenuPositionInFrontOfCamera(GameObject camera, GameObject menu)
    {
        menu.transform.position = camera.transform.position;

        // Get the forward direction unit vector, but without the y component
        Vector3 forward = camera.transform.forward;
        forward.Scale(new Vector3(1f, 0f, 1f));
        forward.Normalize();
        forward.Scale(new Vector3(0.8f, 0.8f, 0.8f));

        menu.transform.position += new Vector3(forward.x, -0.3f, forward.z);
        Quaternion rotation = Quaternion.LookRotation(forward);
        Quaternion menuRotation = menu.transform.rotation;
        menu.transform.rotation = rotation;
    }

    private void respondToKeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            mostRecentSavingsPile = createSavingsPile(new Vector3((numberOfSavingsPiles + 1) * 15, 0, 0), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            generalMoneyPile.convertVisualizationUnit("Starbucks");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            generalMoneyPile.convertVisualizationUnit("PS4");
        }

        if (atLeastOneSavingsPileExists)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                mostRecentSavingsPile.addMoney(115.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                mostRecentSavingsPile.addMoney(1.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                mostRecentSavingsPile.addMoney(10.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                mostRecentSavingsPile.addMoney(100.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                mostRecentSavingsPile.addMoney(1000.0f);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                mostRecentSavingsPile.convertVisualizationUnit("PS4");
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                mostRecentSavingsPile.convertVisualizationUnit("Starbucks");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                mostRecentSavingsPile.removeMoney(1000.0f);
            }
        }
    }

    private void respondToInGameInput(string input)
    {
        switch (input)
        {
            case "back":
                goBackToPreviousMenu();
                break;
            case "newSavings":
                mostRecentSavingsPile = createSavingsPile(new Vector3((numberOfSavingsPiles + 1) * 15, 0, 0), Quaternion.identity);
                break;
            case "convert":
                Debug.Log("Convert menu pressed");
                goToNextMenu(convertMenu);
                break;
            case "shop":
                break;
			case "convert-ps4":
				mostRecentSavingsPile.convertVisualizationUnit("PS4");
				break;
			case "convert-starbucks":
				mostRecentSavingsPile.convertVisualizationUnit("Starbucks");
				break;
			case "convert-gold":
				break;
		case "shop-helicopter":
			Debug.Log ("Shop heli");
			storeScript.buy ("helicopter");
			break;
		case "shop-car":
			Debug.Log ("Shop car");
			storeScript.buy ("car");
			break;
		case "shop-boat":
			Debug.Log ("Shop boat");
			storeScript.buy ("boat");
			break;
		default:
                break;
        }
    }

    // This method provides an attachment point for in-world keypads. Keypads call this method and provide the keys that are being pressed
    //, and this world controller handles the incoming input accordingly
    public void receiveKey(string keyValue)
    {
        respondToInGameInput(keyValue);

    }
}
