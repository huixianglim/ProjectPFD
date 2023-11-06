// import { AssemblyAI } from "./node_modules/assemblyai/dist/index.esm.js";

$(document).ready(() => {
    const recognition = new webkitSpeechRecognition();
    recognition.continuous = true;
    recognition.interimResults = true

    recognition.lang = 'en-US'

    const DOWN_CUTOFF = window.innerWidth / 4
    const RIGHT_CUTOFF = window.innerWidth - window.innerWidth / 4

    const video = document.getElementById("video");
    var audioChunks = [];
    var audioRecorder;


    $(".button").on("mousedown", () => {
        recognition.start()
        $(".button").data("record", true)
        $(".button").addClass("active")

    }

    )
    $(".button").on("mouseup", () => {
        recognition.stop()
        $(".button").removeClass("active")

        $(".button").data("record", false)

    })
    recognition.onresult = (event) => {
        const transcript = event.results[0][0].transcript;
        console.log(`Transcript: ${transcript}`);
        $(".testing").text(transcript)
    };

})