import { GestureRecognizer, FilesetResolver } from "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@0.10.3";

var ForRedirect = {
    "Thumb_Up": ["Home", "Index"],
    "Thumb_Down": ["PayNow", "Paynow"]

}



let gestureRecognizer;
const video = document.getElementById("video");
var currentOutput;
var previousOutput;
var time = 1000
var cooldown = 0;

if (video.dataset.type == "confirmation") {
    ForRedirect = {
        "Thumb_Up": ["Home","Index"],
        "Thumb_Down": ["PayNow","Paynow"],
        "Open_Palm": ["Confirm"]


    }
}
else if (video.dataset.type == "home") {
    if ($("#tutorial").css('display') != 'none') {
        ForRedirect = {
            "Thumb_Up": ["Home", "Index"],
            "Thumb_Down": ["PayNow", "Paynow"],
            "Open_Palm": ["Closing Tutorial"]


        }
    }
}
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
        if (results.gestures.length > 0 && results.gestures[0][0].categoryName in ForRedirect) {
            var categoryName = results.gestures[0][0].categoryName;
            currentOutput = categoryName;
            if (currentOutput == previousOutput) {
                time -= 10
            }
            else {
                time = 1000

            }
            $(".overlay").text("Going to " + ForRedirect[categoryName][0])
            previousOutput = categoryName
            if (time == 0) {
                if (categoryName != "Open_Palm") {
                    window.location.href = "/Main/" + ForRedirect[categoryName][1];
                }
                else {
                    if (video.dataset.type == "confirmation") {
                        var parsedValue = parseInt($("#Money").val());
                        console.log(parsedValue <= 0);
                        if (isNaN(parsedValue) || parsedValue <= 0) {
                            $(".error").show();


                        }
                        else {
                            $("#form-sub").trigger("click");

                        }
                        time = 1000
                    }
                    else {

                        if ($("#tutorial").css('display') != 'none') {
                            $("#end-slideshow").trigger('click')
                            delete ForRedirect["Open_Palm"]; 
                        }
                        

                    }

                }
            }

        } else {
            $(".overlay").text("Nothing")
            cooldown += 8;
            if (cooldown == 1000) {
                time = 1000;

            }
        }
    }

    // Call this function again to keep predicting when the browser is ready.
    window.requestAnimationFrame(predictWebcam);

    
  
}

async function start() {
    await setupGestureRecognizer();
    await initCamera();
    predictWebcam();
}

start();