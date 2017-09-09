var loginIsDisplayed = false;
var menuIsDisplayed = false;
var menuOption = 0;


$("#userLogo").click(function (e) {
    e.stopPropagation();
    ShowLogin();
});

$(".userAndCartDiv").click(function (e) {
    e.stopPropagation();
});

//$("#menuList").click(function (e) {
//    e.stopPropagation();
//    menuIsDisplayed = false;
//    ShowMenu();
//})

$(".dropDownMenu").click(function (e) {
    e.stopPropagation();
})

$(document).click(function (e) {
    //e.stopPropagation();
    loginIsDisplayed = true;
    menuIsDisplayed = true;
    ShowLogin();
    ShowMenu();
});

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

    console.log(menuIsDisplayed);
    console.log(id);
    if (menuIsDisplayed === false) {

        $(".dropDownMenu").css('display', 'block');
        $.ajax({
            url: "/Product/GetDropDownMenu/" + id,
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
        else if (menuOption != 0)
        {
            menuIsDisplayed = false;
            ShowMenu(id);
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

function SetDisplayPic(source) {
    
    $.ajax({
        url: "/Product/GetPicture?id=" + source,
        type: "GET",
        success: function (result) {
            $(".mainPictureDiv").html(result);
        }
    });
}

$("#colorDropDown").change(function () {

    colorId = $("#colorDropDown").val();
    var tmp = window.location.href;
    window.location.href = tmp.substring(0, tmp.length - 1) + colorId;

});


function GetSelectedProduct(id) {

    window.location.href = window.location.href + "/Product/ProductItem/" + id;

    $.ajax({
        url: "/Product/ProductItem?id=" + id,
        type: "GET",
        success: function (result) {
            $(".productPage").html(result);
        }
    });
}
