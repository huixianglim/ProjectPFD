// import { AssemblyAI } from "./node_modules/assemblyai/dist/index.esm.js";

$(document).ready(() => {
    const recognition = new webkitSpeechRecognition();
    recognition.continuous = true;
    recognition.interimResults = true;

    numbers = {
        "one": 1,
        "two": 2,
        "three": 3,
        "four": 4,
        "five": 5,
        "six": 6,
        "seven": 7,
        "eight": 8,
        "nine": 9,
        "ten": 10,

    }

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
        if (transcript.includes("$")) {
            let amountWithoutDollarSign = transcript.replace(/\$/g, '');
            $(".testing").text(amountWithoutDollarSign)

        }
        else {
            $(".testing").text("Invalid Amount")

        }
    };

})