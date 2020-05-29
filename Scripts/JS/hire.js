







var sum = 0;


function myFunction() {

    var checkBox = document.getElementById("myCheck");

    var s1 = 50;

    if (checkBox.checked == true) {
        sum = sum + s1;
        document.getElementById("salary").innerHTML = sum;
    } else {
        sum = sum - s1;
        document.getElementById("salary").innerHTML = sum;
    }



}

function myFunction1() {

    var checkBox1 = document.getElementById("myCheck1");
    var s2 = 70;
    if (checkBox1.checked == true) {
        sum = sum + s2;
        document.getElementById("salary").innerHTML = sum;
    } else {
        sum = sum - s2;
        document.getElementById("salary").innerHTML = sum;
    }


}


function myFunction2() {

    var checkBox2 = document.getElementById("myCheck2");
    var s3 = 80;
    if (checkBox2.checked == true) {
        sum = sum + s3;
        document.getElementById("salary").innerHTML = sum;
    } else {
        sum = sum - s3;
        document.getElementById("salary").innerHTML = sum;
    }


}