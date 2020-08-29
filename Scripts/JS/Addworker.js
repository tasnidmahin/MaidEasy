

$(document).ready(function () {
    $('#submitbtn').click(function () {
        checked = $("input[type=checkbox]:checked").length;

        if (!checked) {
            // alert("You must check at least one checkbox.");
            document.getElementById("invalid-message").style.display = "block";
            return false;
        }
        if (checked) {
            document.getElementById("invalid-message").style.display = "hidden";
        }

    });
});
