using UnityEngine;
using UnityEngine.UI;

public class Enter : MonoBehaviour
{
    [SerializeField] private Button enterVRButton;
    [SerializeField] private Button enterARButton;

    void Start()
    {
        if (enterVRButton != null)
            enterVRButton.onClick.AddListener(EnterVR);

        if (enterARButton != null)
            enterARButton.onClick.AddListener(EnterAR);
    }

    private void EnterVR()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval(@"
            if (navigator.xr) {
                navigator.xr.requestSession('immersive-vr')
                    .then(function(session) {
                        // This helps resume Unity's main loop timing if needed
                        unityInstance.Module.asmLibraryArg._emscripten_set_main_loop_timing(0, 0);
                        console.log('VR session started');
                    })
                    .catch(function(err) {
                        console.error('Failed to start VR session:', err);
                    });
            } else {
                console.error('WebXR not supported in this browser');
            }
        ");
#endif
    }

    private void EnterAR()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval(@"
            if (navigator.xr) {
                navigator.xr.requestSession('immersive-ar', {
                    optionalFeatures: ['dom-overlay'],
                    domOverlay: { root: document.body }
                })
                    .then(function(session) {
                        unityInstance.Module.asmLibraryArg._emscripten_set_main_loop_timing(0, 0);
                        console.log('AR session started');
                    })
                    .catch(function(err) {
                        console.error('Failed to start AR session:', err);
                    });
            } else {
                console.error('WebXR not supported in this browser');
            }
        ");
#endif
    }
}