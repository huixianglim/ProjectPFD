// import { AssemblyAI } from "./node_modules/assemblyai/dist/index.esm.js";

$(document).ready(() => {
    const recognition = new webkitSpeechRecognition();
    //recognition.continuous = true;
    //recognition.interimResults = true;

    var webkitstarted = false

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

    })
    recognition.onstart = () => {
        webkitstarted = true
    }
    recognition.onend = () => {
        webkitstarted = false

    }

    var previousTranscript = "";


    recognition.onresult = async (event) => {
        const transcript = event.results[0][0].transcript;

        if ($("#popupNavContainer").css("display") != "none") {

            if (Object.keys(numbers).includes(transcript)) {

                //console.log("test")
                //console.log($(".popupNavResult")[numbers[transcript] - 1])
                //$(".popupNavResult")[numbers[transcript] - 1].trigger("click");
                var popupActivatedElements = $(".popupActivated");
                if (popupActivatedElements.length > numbers[transcript] - 1) {
                    var targetElement = $(".popupActivated")[numbers[transcript] - 1];
                    targetElement.click();
                }
             // or targetElement[0].click();
            }
            else {
                if (!(transcript.includes("nav") || transcript.includes("close"))) {
                    if (transcript == "empty") {
                       $("#searchQueryInput2").val("");

                    }
                    else {
                       $("#searchQueryInput2").val(transcript);
                    }

                    document.getElementById('searchQueryInput2').dispatchEvent(new Event('input', {
                        bubbles: true
                    }))
                    console.log("Test")
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
                    console.log(transcript)
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


                } else if ($(".button").data("type") == "feedback") {
              
                    console.log(transcript)
                    current_page = $(".button").data("page")
                    current_page = parseInt(current_page)
                    page = {
                        1: 3,
                        2: "",
                        3: 2

                    }
                    stars = {
                        "five": 0,
                        "four": 1,
                        "three": 2,
                        "two": 3,
                        "one": 4
                    }


                    if (transcript.includes("continue")) {

                        $("#nextBtn").trigger("click")
         
                    }
                    else if (transcript.includes("back")) {

                        if (current_page != 1) {
                                $("#prevBtn").trigger("click")

                        }

                    }
                    else {
                        const foundWord = Object.keys(stars).find(word => transcript.includes(word));

                        if (foundWord) {
                            rate_input = ".rate" + page[current_page]

                            if (transcript.includes("stars")) {
                                let stringWithoutStars = transcript.replace(" stars", "");


                                let value = stars[stringWithoutStars]
                                let starinput = $(rate_input+ " input")
                                starinput.eq(value).trigger("click")

                            }
                            else if (transcript.includes("star")) {
                                let stringWithoutStars = transcript.replace(" star", "");

                                let value = stars[stringWithoutStars]
                                let starinput = $(rate_input + " input")
                                starinput.eq(value).trigger("click")
                            }
                        }
                        else {
                            let input = $(".tab input[type = text]")

                            if (transcript.includes("empty")) {
                                input.eq(current_page - 1).val()
                            }
                            //console.log(input)
                            input.eq(current_page -1).val(transcript)

                        }
                    }


                    
                   
                }
            }

           


        }

        

      

        
    };

})