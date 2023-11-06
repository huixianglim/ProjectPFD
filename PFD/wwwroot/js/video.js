import { GestureRecognizer, FilesetResolver } from "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@0.10.3";

var ForRedirect = {
    "Thumb_Up": "/Main/Index",
    "Thumb_Down": "/Main/PayNow",



}

let gestureRecognizer;
const video = document.getElementById("video");

async function setupGestureRecognizer() {
    const vision = await FilesetResolver.forVisionTasks(
        "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@0.10.3/wasm"
    );
    gestureRecognizer = await GestureRecognizer.createFromOptions(vision, {
        baseOptions: {
            modelAssetPath:
                "https://storage.googleapis.com/mediapipe-models/gesture_recognizer/gesture_recognizer/float16/1/gesture_recognizer.task",
            delegate: "GPU"
        },
        runningMode: "VIDEO"
    });
}

async function initCamera() {
    const constraints = {
        video: true
    };

    try {
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        video.srcObject = stream;
    } catch (error) {
        console.error("Error accessing webcam:", error);
    }
}

function predictWebcam() {
    if (gestureRecognizer && video.readyState === video.HAVE_ENOUGH_DATA) {
        const results = gestureRecognizer.recognizeForVideo(video, Date.now());
        var check = false

        if (results.gestures.length > 0) {
            const categoryName = results.gestures[0][0].categoryName;

            if (categoryName != "None" && categoryName in ForRedirect) {
                console.log(categoryName)
                setTimeout(() => {
                    window.location.href = ForRedirect[categoryName];

                },3000)
                return
            }
        } else {
            console.log("No gesture detected.");
        }
    }

    // Call this function again to keep predicting when the browser is ready.
    if (!check) {
        window.requestAnimationFrame(predictWebcam);

    }
  
}

async function start() {
    await setupGestureRecognizer();
    await initCamera();
    predictWebcam();
}

start();