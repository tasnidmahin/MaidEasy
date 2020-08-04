

window.onload = function () {
    document.getElementById("edit").addEventListener("click", function () {
        document.getElementById("name").removeAttribute("readonly");
        document.getElementById("father").removeAttribute("readonly");
        document.getElementById("phone").removeAttribute("readonly");

        document.getElementById("present").removeAttribute("readonly");
        document.getElementById("permanent").removeAttribute("readonly");
        document.getElementById("choicebox").style.display = "block";
        document.getElementById("choicelist").style.display = "none";
        document.getElementById("selectbox1").style.display = "block";
        document.getElementById("selectbox2").style.display = "block";
        document.getElementById("selectboxhide").style.display = "none";
        document.getElementById("thanalist").style.display = "none";
        console.log('done');

    });

}


/*

document.getElementById("save").addEventListener("click", function () {

    document.getElementById("name").setAttribute("readonly");
    document.getElementById("father").setAttribute("readonly");
    document.getElementById("phone").setAttribute("readonly");

    document.getElementById("present").setAttribute("readonly");
    document.getElementById("permanent").setAttribute("readonly");
    document.getElementById("choicebox").style.display = "block";
    document.getElementById("choicelist").style.display = "none";
    document.getElementById("selectbox1").style.display = "block";
    document.getElementById("selectbox2").style.display = "block";
    document.getElementById("selectboxhide").style.display = "none";
    document.getElementById("thanalist").style.display = "none";
    console.log('do');
});


*/

/*
myfunction() {
    document.getElementById("name").removeAttribute("readonly");
    document.getElementById("father").removeAttribute("readonly");
    document.getElementById("phone").removeAttribute("readonly");

    document.getElementById("present").removeAttribute("readonly");
    document.getElementById("permanent").removeAttribute("readonly");

    document.getElementById("selectbox1").style.display = "block";
    document.getElementsById("selectboxhide").style.display = "none";
    console.log('done');

};

*/