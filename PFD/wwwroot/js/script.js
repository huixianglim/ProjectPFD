

$("#searchQueryInput").on("input",(e) => {
    console.log("test")
    var contacts = document.getElementsByClassName("pay-row");
    for (let i = 0; i < contacts.length; i++) {
        let name = (contacts[i].dataset.name).toLowerCase()
        if (!name.includes(e.target.value.toLowerCase())) {
            console.log(name)
            contacts[i].setAttribute('style', 'display:none !important');


            console.log("Fail")
        } else {
            console.log(name)
            contacts[i].setAttribute('style', 'display:block !important');
            console.log("Success")


        }
    }



})