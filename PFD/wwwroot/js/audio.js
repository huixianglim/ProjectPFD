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

    var email_selected_element = null;


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
        else if ($("#email_container").css("display") != "none") {
            console.log(transcript)


            if (transcript.includes("update")) {
                email_selected_element = "newEmail"
                $("#" + email_selected_element).focus()
            } else if (transcript.includes("confirm")){
                email_selected_element = "confirmEmail"
                $("#" + email_selected_element).focus()

            }
            else if (transcript.includes("close")) {
                let popup = $(".popup.email");
                let close = popup.find(".btn-close");  // Use .find() to get the element with the class "btn-close"
                close.click();

            }
            else if (transcript.includes("submit")) {
                $("#email_update > button").click()
            }

            else if (email_selected_element != null) {
                console.log(transcript.includes("at"))
                if (transcript.includes("at")) {
                    email_with_space = transcript.replace("at", "@");
                    final_email = email_with_space.replace(/ /g, '');
                    $("#"+email_selected_element).val(final_email)
                    element = document.getElementById(email_selected_element)
                    element.dispatchEvent(new Event('input'));

                }
                else if (transcript == "empty") {
                    $("#" + email_selected_element).val("")
                    //$(email_selected_element).trigger("input")
                    element = document.getElementById(email_selected_element)
                    element.dispatchEvent(new Event('input'));

                }
            }


        }
        else {
            console.log(transcript)
            if (transcript.includes("nav")) {
                $(".navbar-links3").trigger("click")

            }
            else if (transcript.includes("email")) {
                $("#updateEmail").trigger("click")

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

                            if (transcript == "empty") {
                                input.eq(current_page - 1).val("")
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