using UnityEngine;

public class WalletLogic : MonoBehaviour
{
    public Transform player;
    public Camera _camera;
    public Camera InspectionCamera;
    public Transform InspectionWallet;
    void Start()
    {

    }


    void Update()
    {

    }

    public void Interacted()
    {
        player.GetComponent<PlayerMovement>().canMove = false;
        _camera.GetComponent<Camera>().enabled = false;
        InspectionCamera.GetComponent<Camera> ().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
