
const video = document.getElementById("video");


var count = 500;
var webcamStream;

function start() {
    Promise.all([
        faceapi.nets.ssdMobilenetv1.loadFromUri(`/models`),
        faceapi.nets.faceRecognitionNet.loadFromUri(`/models`),
        faceapi.nets.faceLandmark68Net.loadFromUri(`/models`),
    ]).then(startWebcam);

    setTimeout(stopWebcam, 8000);



}

function stopWebcam() {
    if (webcamStream) {
        const tracks = webcamStream.getTracks();

        tracks.forEach(track => {
            track.stop();
        });

        // Reset the video element's source to stop displaying the webcam feed
        video.srcObject = null;
        $(".container-vid").hide()
        $(".overlay").hide()

    }
    $(".face-error").show();
}

function startWebcam() {
    $(".container-vid2").show()

    navigator.mediaDevices
        .getUserMedia({
            video: true,
            audio: false,
        })
        .then((stream) => {
            video.srcObject = stream;
            webcamStream = stream;
            $(".container-vid").show()
            $(".overlay").show()


            $(".container-vid2").hide()
        })
        .catch((error) => {
            console.error(error);
        });
}

function trainModel() {
    const labels = ["HuiXiang", "Kenan", "Wesley"];
    return Promise.all(
        labels.map(async (label) => {
            const descriptions = [];
            for (let i = 1; i <= 1; i++) {
                const img = await faceapi.fetchImage(`/labels/${label}/${i}.jpg`);
                const detections = await faceapi
                    .detectSingleFace(img)
                    .withFaceLandmarks()
                    .withFaceDescriptor();
                if (!detections) {
                    throw new Error(`no faces detected for ${label}`)
                }
                descriptions.push(detections.descriptor)
                console.log(detections)
                console.log("Hi")
            }
            return new faceapi.LabeledFaceDescriptors(label, descriptions)


        })
    );
}

video.addEventListener("play", async () => {
    const labeledFaceDescriptors = await trainModel();
    const faceMatcher = new faceapi.FaceMatcher(labeledFaceDescriptors);


    const computedStyle = window.getComputedStyle(video);

    // Access the 'height' property value
    const heightValue = parseInt(computedStyle.getPropertyValue('height'));

    const widthValue = parseInt(computedStyle.getPropertyValue('width'));

    const displaySize = { width: widthValue, height: heightValue };

    setInterval(async () => {
        const detections = await faceapi
            .detectAllFaces(video)
            .withFaceLandmarks()
            .withFaceDescriptors();

        const resizedDetections = faceapi.resizeResults(detections, displaySize);

        const results = resizedDetections.map((d) => {
            return faceMatcher.findBestMatch(d.descriptor);
        });
        console.log(results[0]);

        if (count <= 0 && results[0].label !='unknown' && results.length != 0) {
            await $("#face_verify").val(results[0].label)
            setTimeout(() => {
                $(".faceSubmit").submit()
            },1500)

        }
        else if (results.length == 1) {
            if (results[0].label != 'unknown') {
                count -= 30;
            }
            $(".overlay").html(`Scanning...`)
        }
        else {
            count = 500;
            $(".overlay").html("Invalid Scan. Try to position yourself better.")
        }
    }, 100);
});