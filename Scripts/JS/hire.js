



$(document).ready(function () {
    $('#checkBtn').click(function () {
        checked = $("input[type=checkbox]:checked").length;

        if (!checked) {
            // alert("You must check at least one checkbox.");
            document.getElementById("invalid-message").style.display ="block";
            return false;
        }

    });
});




var sum = 0;


function myFunction(dID, element_id , value , t,  sal) {


    console.log(value);
    var time = t;
    var checkBox = document.getElementById(element_id);

    var s1 = sal;

    if (checkBox.checked == true) {
        sum = sum + (s1 * time);
        document.getElementById("salary").value = sum;
        //document.getElementById("checked_value").setAttribute("value", value);
        document.getElementById(dID).setAttribute("value", value);
    } else {
        sum = sum - (s1 * time);
        document.getElementById("salary").value = sum;
        //document.getElementById("checked_value").setAttribute("value", "null");
        document.getElementById(dID).setAttribute("value", "null");
    }


    document.getElementById("sal").setAttribute("value", sum);
}

