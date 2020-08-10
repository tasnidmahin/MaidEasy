

window.onload = function () {


 

    document.getElementById("edit").addEventListener("click", function () {
        /*if (document.getElementById("edit").innerHTML == "Save") {
            document.getElementById("edit").innerHTML = "Edit";
            document.getElementById("name").readOnly = true;
            document.getElementById("father").readOnly = true;
            document.getElementById("phone").readOnly = true;

            document.getElementById("present").readOnly = true;
            document.getElementById("permanent").readOnly = true;
            document.getElementById("choicebox").style.display = "none";
            document.getElementById("choicelist").style.display = "block";
            document.getElementById("selectbox1").style.display = "none";
            document.getElementById("selectbox2").style.display = "none";
            document.getElementById("selectboxhide").style.display = "block";
            document.getElementById("thanalist").style.display = "block";
            console.log('do');

            var url = "/AdminWorker/SaveWorkerData";
            window.location = url;
        }
        else {
            document.getElementById("edit").innerHTML = "Save";*/


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

            document.getElementById("edit").style.display = "none";
            document.getElementById("save").style.display = "block";
            console.log('done');
        //}

    /* code for save button 
        
       
            document.getElementById("edit").innerHTML = "Edit";
            document.getElementById("name").readOnly = true;
            document.getElementById("father").readOnly = true;
            document.getElementById("phone").readOnly = true;

            document.getElementById("present").readOnly = true;
            document.getElementById("permanent").readOnly = true;
            document.getElementById("choicebox").style.display = "none";
            document.getElementById("choicelist").style.display = "block";
            document.getElementById("selectbox1").style.display = "none";
            document.getElementById("selectbox2").style.display = "none";
            document.getElementById("selectboxhide").style.display = "block";
            document.getElementById("thanalist").style.display = "block";
            console.log('do');
          
        

      */
      
        

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