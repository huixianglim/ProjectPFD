// import { AssemblyAI } from "./node_modules/assemblyai/dist/index.esm.js";

$(document).ready(() => {
    const recognition = new webkitSpeechRecognition();
    recognition.continuous = true;
    recognition.interimResults = true;

    var webkitstarted = false
    console.log($(".popupNavResult"))

    numbers = {
        "start": 1,
        "middle": 2,
        "final": 3,

    }

    recognition.lang = 'en-US'

    const DOWN_CUTOFF = window.innerWidth / 4
    var audioChunks = [];
    var audioRecorder;

    

    $(".button").on("mousedown", () => {
        console.log(webkitstarted)
        if (!webkitstarted) {

            recognition.start()
            $(".button").data("record", true)
            $(".button").addClass("active")
        }


    }

    )
    $(".button").on("mouseup", () => {
        recognition.stop()
       $(".button").removeClass("active")

        $(".button").data("record", false)
        webkitstarted = false

    })
    recognition.onstart = () => {
        webkitstarted = true
    }

    recognition.onresult = (event) => {
        const transcript = event.results[0][0].transcript;
        console.log(`Transcript: ${transcript}`);
        console.log(transcript in numbers)
        console.log($(".button").data("activated"))
        if ($("#popupNavContainer").css("display") !== "none") {

            if (Object.keys(numbers).includes(transcript)) {

                //console.log("test")
                //console.log($(".popupNavResult")[numbers[transcript] - 1])
                //$(".popupNavResult")[numbers[transcript] - 1].trigger("click");
                console.log(numbers[transcript])
                console.log($(".popupActivated"))
                var targetElement = $(".popupActivated")[numbers[transcript] - 1];
                targetElement.click(); // or targetElement[0].click();
            }
            else {
                if (!(transcript.includes("nav") || transcript.includes("close"))) {
                    if (transcript == "empty") {
                        $("#searchQueryInput2").val("");

                    }
                    else {
                        $("#searchQueryInput2").val(transcript);
                    }
                    $("#searchQueryInput2").trigger("input");
                }
                
            }

            if (transcript == "close") {
                $(".closeButtton").trigger("click")
            }


        }
        else {
            if (transcript.includes("nav")) {
                $(".navbar-links3").trigger("click")

            }
            else {
                if ($(".button").data("type") == "money") {
                    if (transcript.includes("$")) {
                        let amountWithoutDollarSign = transcript.replace(/\$/g, '');
                        let value = parseFloat(amountWithoutDollarSign)
                        if (!isNaN(value)) {
                            $("#Money").val(value.toFixed(2))
                        } else {
                            $(".testing").text("Invalid Amount")
                        }

                    }
                    else {
                        $(".testing").text("Invalid Amount")

                    }
                    if (transcript == "confirm") {
                        $("#form-sub").trigger("click");
                    }

                }
                else if ($(".button").data("type") == "search") {
                    const transcript = event.results[0][0].transcript;
                    if (!transcript.includes("close")) {
                        if (transcript == "empty") {
                            $("#searchQueryInput").val("");

                        }
                        else {
                            $("#searchQueryInput").val(transcript);
                        }
                        $("#searchQueryInput").trigger("input");
                    }
  

                }
            }

           


        }

        

      

        
    };

})