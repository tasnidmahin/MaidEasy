window.onload = function () {
    render();
};

function render() {
    window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptha-container');
    recaptchaVerifier.render();
}

function phoneAuth(num) {
    // get the number
    //var number = document.getElementById('Phoneno').value;
    var number = num;
    console.log(number);
    // phone no authentication function of firebase
    //it takes two parameter first one is number,,,second one is recaptcha

    firebase.auth().signInWithPhoneNumber(number, window.recaptchaVerifier).then(function (confirmationResult) {
        //s is in lowercase
        window.confirmationResult = confirmationResult;
        coderesult = confirmationResult;
        console.log(coderesult);
        alert("Message sent");
    }).catch(function (error) {
        alert(error.message);
    });
}

function codeverify(cv) {
    var code = document.getElementById('verificationCode').value;
    coderesult.confirm(code).then(function (result) {
        alert("Congratulations!!! Your Phone No. is now verified.");
        var user = result.user;
        console.log(user);
        //document.getElementById("myForm").submit();
        if (cv == 1) {
            var url = "/Register/AddUser";
            window.location = url;
            return true;
        }
        if (cv == 2) {
            var url = "/Register/saveEditProfile";
            window.location = url;
            return true;
        }
    }).catch(function (error) {
        alert(error.message);
        console.log("FALSE");
        //var url = "http://aust.edu/";
        //window.location = url; 
        return false;
    });
}