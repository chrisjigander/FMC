var loginIsDisplayed = false;
var menuIsDisplayed = false;
var menuOption = 0;

function ShowLogin() {
    if (loginIsDisplayed === false) {

        $(".userAndCartDiv").css('display', 'block');
        $.ajax({
            url: "/Account/GetLoginForm",
            type: "GET",
            success: function (result) {
                $(".userAndCartDiv").html(result);
            }
        });
        loginIsDisplayed = true;
    }
    else {
        $(".userAndCartDiv").css('display', 'none');

        $(".userAndCartDiv").html("");
        loginIsDisplayed = false;

    }
}

function ShowMenu(id) {

    if (menuIsDisplayed === false) {

        $(".dropDownMenu").css('display', 'block');
        $.ajax({
            url: "/Product/GetDropDownMenu",
            type: "GET",
            success: function (result) {
                $(".dropDownMenu").html(result);
            }
        });
        menuIsDisplayed = true;
        menuOption = id;
    }
    else {
        if (id === menuOption) {
            $(".dropDownMenu").css('display', 'none');

            $(".dropDownMenu").html("");
            menuIsDisplayed = false;
            menuOption = 0;
        }
        menuOption = id;
    }
}
function GetProfile() {

    $.ajax({
        url: "/Account/MyProfile",
        type: "GET",
        success: function (result) {
            $(".profileContentDiv").html(result);
            //ShowLogin();
        }
    });
}
function EditProfile() {
    $.ajax({
        url: "/Profile/EditProfile",
        type: "GET",
        success: function (result) {
            $(".profileContentDiv").html(result);
        }
    });
}
function EditedProfile(editedUser) {
    //window.location.href = window.location.host + "/Home/Index";
    //console.log(window.location.host);
    //console.log(window.location.host + "/Home/Index");

    //$("#editForm").submit(e){
    //    e.preventDefault();
    //    $.post("@URL.Action('")
    //};
   
    $.ajax({
        url: "/Profile/SaveEdit",
        type: "POST",
        data: editedUser,
        success: function (result) {
            GetProfile();
        }
    });

}
function ProfileMenuOption(option) {

    switch (option) {
        case 1:
            GetProfile();
            break;
        case 2:
            GetOrders();
            break;
        case 3:
            EditProfile();
            break;
    }

}

