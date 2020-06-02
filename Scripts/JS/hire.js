







var sum = 0;


function myFunction(t) {

    var time = t;
    var checkBox = document.getElementById("myCheck");

    var s1 = 50;

    if (checkBox.checked == true) {
        sum = sum + (s1 * time);
        document.getElementById("salary").innerHTML = sum;
        document.getElementById("check1").setAttribute("value", "Cooking");
    } else {
        sum = sum - (s1 * time);
        document.getElementById("salary").innerHTML = sum;
        document.getElementById("check1").setAttribute("value", "null");
    }


    document.getElementById("sal").setAttribute("value", sum);
}

function myFunction1(t) {

    var time = t;
    var checkBox1 = document.getElementById("myCheck1");
    var s2 = 70;
    if (checkBox1.checked == true) {
        sum = sum + (s2 * time);
        document.getElementById("salary").innerHTML = sum;
        document.getElementById("check2").setAttribute("value", "Washing Clothes");
    } else {
        sum = sum - (s2 * time);
        document.getElementById("salary").innerHTML = sum;
        document.getElementById("check2").setAttribute("value", "null");
    }

    document.getElementById("sal").setAttribute("value", sum);
}


function myFunction2(t) {

    var time = t;
    var checkBox2 = document.getElementById("myCheck2");
    var s3 = 80;
    if (checkBox2.checked == true) {
        sum = sum + (s3 * time);
        document.getElementById("salary").innerHTML = sum;
        document.getElementById("check3").setAttribute("value", "Home Cleaning");
    } else {
        sum = sum - (s3 * time);
        document.getElementById("salary").innerHTML = sum;
        document.getElementById("check3").setAttribute("value", "null");
    }

    document.getElementById("sal").setAttribute("value", sum);
}

