

//Confirm Passwords if it Match!
function IsMatch() {


    var pass = document.getElementById("password").value;
    var comPass = document.getElementById("confirmPassword").value

    console.log(pass);
    console.log(comPass);

    if (pass == comPass) {

        document.getElementById("messMatch").innerHTML = "Passwords  match!";
        document.getElementById("messMatch").style.color = "Green";

      


    }
    else {

        document.getElementById("messMatch").style.color = "Red";
        document.getElementById("messMatch").innerHTML = "Passwords do NOT match!";

     

    }

}






//Check if Username is Exist
function IsUsernameExist() {

    document.getElementById("checkUsername").innerHTML = "Checking......";

    var username = document.getElementById("username").value;

    if (username == null) {
        document.getElementById("checkUsername").innerHTML = "";
    }

    console.log(username);
    $.ajax({

        url: "/LoginRegisteration/IsUsernameExist",
        type: "POST",
        data: { username: username },
        success: function (data) {

            console.log(data);
            if (data == 1) {
                document.getElementById("checkUsername").innerHTML = "The Username is Exist";
                document.getElementById("checkUsername").style.color = "Red";
                document.getElementById("username").style.borderColor = "Red";
            }
            else {

                document.getElementById("checkUsername").innerHTML = "The Username is Available";
                document.getElementById("checkUsername").style.color = "Green";
                document.getElementById("username").style.borderColor = "Green";

            }
        }


    })

}


//Check if Email is Exist
function IsEmailExist() {

    document.getElementById("checkEmail").innerHTML = "Checking......";

    var email = document.getElementById("email").value;

    if (email == null) {
        document.getElementById("checkEmail").innerHTML = "";
    }

    console.log(email);

    $.ajax({

        url:"/LoginRegisteration/IsEmailExist",
        type: "POST",
        data: { email: email },
        success: function (data) {

            console.log(data);
            if (data == 1) {
                document.getElementById("checkEmail").innerHTML = "The Emial is Exist";
                document.getElementById("checkEmail").style.color = "Red";
                document.getElementById("email").style.borderColor = "Red";
            }
            else {

                document.getElementById("checkEmail").innerHTML = "The Emial is Available";
                document.getElementById("checkEmail").style.color = "Green";
                document.getElementById("email").style.borderColor = "Green";

            }
        }


    })


}






    //function Register()
    //{
    //    //Input Value
    //    var fullname = document.getElementById("fullName").value;
    //    var phone = document.getElementById("phone").value;
    //    var barthday = document.getElementById("barthday").value;
    //    var gender = document.getElementById("gender").value;
    //    var username = document.getElementById("username").value;
    //    var email = document.getElementById("email").value;
    //    var password = document.getElementById("password").value;
    //    var imageFile = document.getElementById("imageFile").value;




    //    //Js Object
    //    var register = new Object()

    //    register.Fullname = fullname;
    //    register.Email = email;
    //    register.Username = username;
    //    register.Phonenumber = phone;
    //    register.Barthday = barthday;
    //    register.ImageFile = imageFile;
    //    register.Gender = gender;





    //    console.log(register);

    //    $.ajax({
    //        url: "/LoginRegisteration/Register",
    //        type: "POST",
    //        data: { register: register },
    //        success: function(data) {

    //            console.log(data);




    //                //window.location.href = data.actionLink;


    //        }


    //    })


    //}