/**
* Copyright (c) 2021 Vuplex Inc. All rights reserved.
*
* Licensed under the Vuplex Commercial Software Library License, you may
* not use this file except in compliance with the License. You may obtain
* a copy of the License at
*
*     https://vuplex.com/commercial-library-license
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using UnityEngine;
using Mediapipe.Unity.Sample.HandTracking;
namespace Vuplex.WebView.Demos {

    /// <summary>
    /// Sets up the CanvasWorldSpaceDemo scene, which displays a `CanvasWebViewPrefab`
    /// in a world space canvas.
    /// </summary>
    class CanvasWorldSpaceDemo : MonoBehaviour {

        CanvasWebViewPrefab _canvasWebViewPrefab;
        HardwareKeyboardListener _hardwareKeyboardListener;
        [SerializeField] CommunicatorWithBrowser Communicator;

        async void OnEnable()
        {

            // Use a desktop User-Agent to request the desktop versions of websites.
            // https://developer.vuplex.com/webview/Web#SetUserAgent
            Web.SetUserAgent(false);

            // The CanvasWebViewPrefab's `InitialUrl` property is set via the editor, so it
            // automatically loads that URL when it initializes.
            _canvasWebViewPrefab = GameObject.Find("CanvasWebViewPrefab").GetComponent<CanvasWebViewPrefab>();
            _setupKeyboards();

            // Wait for the CanvasWebViewPrefab to initialize, because the CanvasWebViewPrefab.WebView property
            // is null until the prefab has initialized.
            await _canvasWebViewPrefab.WaitUntilInitialized();

            // The CanvasWebViewPrefab has initialized, so now we can use the IWebView APIs
            // using its CanvasWebViewPrefab.WebView property.
            // https://developer.vuplex.com/webview/IWebView
            _canvasWebViewPrefab.WebView.UrlChanged += (sender, eventArgs) => {
                Debug.Log("URL changed: " + eventArgs.Url);
            };
        }

        void _setupKeyboards() {

            // Send keys from the hardware (USB or Bluetooth) keyboard to the webview.
            // Use separate `KeyDown()` and `KeyUp()` methods if the webview supports
            // it, otherwise just use `IWebView.HandleKeyboardInput()`.
            // https://developer.vuplex.com/webview/IWithKeyDownAndUp
            _hardwareKeyboardListener = HardwareKeyboardListener.Instantiate();
            _hardwareKeyboardListener.KeyDownReceived += (sender, eventArgs) => {
                var webViewWithKeyDown = _canvasWebViewPrefab.WebView as IWithKeyDownAndUp;
                if (webViewWithKeyDown == null) {
                    _canvasWebViewPrefab.WebView.HandleKeyboardInput(eventArgs.Value);
                } else {
                    webViewWithKeyDown.KeyDown(eventArgs.Value, eventArgs.Modifiers);
                }
            };
            _hardwareKeyboardListener.KeyUpReceived += (sender, eventArgs) => {
                var webViewWithKeyUp = _canvasWebViewPrefab.WebView as IWithKeyDownAndUp;
                if (webViewWithKeyUp != null) {
                    webViewWithKeyUp.KeyUp(eventArgs.Value, eventArgs.Modifiers);
                }
            };
        }

        void Update()
        {
            if (Communicator.ReadyToClickBrowser)
            {
                _canvasWebViewPrefab.WebView.Click(Communicator.pointBrowser);
                Communicator.ReadyToClickBrowser = false;
            }
            if (Communicator.DoClickBack)
            {
                _canvasWebViewPrefab.WebView.GoBack();
                Communicator.DoClickBack = false;
            }
            if (Communicator.DoClickForward)
            {
                _canvasWebViewPrefab.WebView.GoForward();
                Communicator.DoClickForward = false;
            }
            if (Communicator.DoClickReload)
            {
                _canvasWebViewPrefab.WebView.Reload();
                Communicator.DoClickReload = false;
            }
            if (Communicator.DoClickGoogle)
            {
                _canvasWebViewPrefab.WebView.LoadUrl("https://www.google.com");
                Communicator.DoClickGoogle = false;
            }

            if (Communicator.DoSendCommand)
            {
                _canvasWebViewPrefab.WebView.HandleKeyboardInput(Communicator.Command);
                Communicator.DoSendCommand = false;
            }

            if (Communicator.DoOpenURL)
            {
                _canvasWebViewPrefab.WebView.LoadUrl(Communicator.URL);
                Communicator.DoOpenURL = false;
            }
        }
    }
}
