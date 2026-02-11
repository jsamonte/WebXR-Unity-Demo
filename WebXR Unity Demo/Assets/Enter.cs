using UnityEngine;
using UnityEngine.UI;
using WebXR;  // Make sure this namespace is available after importing the package

public class XRSessionStarter : MonoBehaviour
{
    [SerializeField] private Button enterVRButton;
    [SerializeField] private Button enterARButton;

    private WebXRManager webXRManager;

    void Awake()
    {
        webXRManager = WebXRManager.Instance;  // Usually a singleton
        if (webXRManager == null)
        {
            Debug.LogError("WebXRManager not found!");
            return;
        }
    }

    void Start()
    {
        if (enterVRButton != null)
        {
            enterVRButton.onClick.AddListener(OnEnterVR);
            // Optional: Hide/disable if VR not supported
            // enterVRButton.gameObject.SetActive(webXRManager.isVRSupported);  // Check API for support check
        }

        if (enterARButton != null)
        {
            enterARButton.onClick.AddListener(OnEnterAR);
        }
    }

    private void OnEnterVR()
    {
        webXRManager?.EnterVR();  // Or: webXRManager.StartSession("immersive-vr");
    }

    private void OnEnterAR()
    {
        webXRManager?.EnterAR();  // Or: webXRManager.StartSession("immersive-ar");
    }
}