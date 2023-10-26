// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(() => {

    const DOWN_CUTOFF = window.innerWidth / 4
    const RIGHT_CUTOFF = window.innerWidth - window.innerWidth / 4

    webgazer.setGazeListener((data, timestamp) => {

    }).begin()
    var audioChunks = [];
    var audioRecorder; // Declare the audioRecorder variable outside of the event handler

    // Initialize the audioRecorder and event listeners
    navigator.mediaDevices.getUserMedia({ audio: true })
        .then(function (stream) {
            audioRecorder = new MediaRecorder(stream);

            audioRecorder.addEventListener('dataavailable', e => {
                audioChunks.push(e.data);
            });
        })
        .catch(function (error) {
            console.error('Error accessing microphone:', error);
        });

    $(".button").click(() => {
        if ($(".button").data("record") == false) {
            alert("Recording has started")
            // Start recording
            audioChunks = []; // Clear any previous audio chunks
            audioRecorder.start();
            $(".button").data("record", true)
        } else {
            alert("Recording has stopped")

            // Stop recording
            audioRecorder.stop();

            // Event handler when recording is stopped
            audioRecorder.onstop = function () {
                // Combine audio chunks into a single Blob
                var audioBlob = new Blob(audioChunks, { type: 'audio/webm' });

                // Create a new Audio element to play the recorded audio
                var audioUrl = URL.createObjectURL(audioBlob);
                var audio = new Audio(audioUrl);
                audio.play();
                $(".button").data("record", false)

            };
        }
    });
});
