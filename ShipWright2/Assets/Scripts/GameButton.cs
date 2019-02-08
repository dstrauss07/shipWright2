using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{


    private string buttonType = "PlayScreen";

    public void loadPlayScreen()
    {
        SceneManager.LoadScene("PlayScreen");
    }

    public void loadInventory()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void loadShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void loadGuestBookScreen()
    {
        SceneManager.LoadScene("GuestBook");
    }

}
