﻿var loginIsDisplayed = false;
var cartIsDisplayed = false;
var menuIsDisplayed = false;
var menuOption = 0;

//******************************************   DOCUMENTZ   **************************************************//

$(document).ready(function () {
    $.ajax({
        url: "/Product/GetCartCount/",
        type: "GET",
        success: function (result) {
            if (result > 9) {
                result = "9+";
            }
            else if (result == "0") {
                result = "";
            }
            $("#cartCount").html(result);
        }
    });
});

$(document).click(function (e) {
    CheckIfDisplayed();
});

function CheckIfDisplayed() {

    if (loginIsDisplayed === true) {

        ShowLogin();
    }
    if (cartIsDisplayed === true) {
        ShowCart();
    }
    if (menuIsDisplayed === true) {
        ShowMenu(menuOption);
    }

}

//******************************************   DROPDOWN-MENU   *********************************************//


$("#skorMenuLi").click(function (e) {
    e.stopPropagation();
    ShowMenu(1);
});

$("#varumarkenMenuLi").click(function (e) {
    e.stopPropagation();
    ShowMenu(2);
});

$("#accessoarerMenuLi").click(function (e) {
    e.stopPropagation();
    ShowMenu(3);
});

$(".dropDownMenu").click(function (e) {
    e.stopPropagation();
});

function ShowMenu(id) {
    
    if (menuIsDisplayed === false) {
        CheckIfDisplayed();
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
        else if (menuOption != 0) {
            menuIsDisplayed = false;
            ShowMenu(id);
        }
    }
}

//******************************************   USER AND CART-MENU   ****************************************//

$("#userLogo").click(function (e) {
    e.stopPropagation();
    ShowLogin();
});

$("#cartLogo").click(function (e) {
    e.stopPropagation();
    ShowCart();
});

$("#cartCount").click(function (e) {
    e.stopPropagation();
    ShowCart();
});

$(".userAndCartDiv").click(function (e) {
    e.stopPropagation();
});

function ShowLogin() {
    if (loginIsDisplayed === false) {
        CheckIfDisplayed();
        $(".userAndCartDiv").css('display', 'block');
        $.ajax({
            url: "/Account/GetLoginForm",
            type: "GET",
            success: function (result) {
                $(".userAndCartDiv").html(result);
            }
        });
        loginIsDisplayed = true;
        cartIsDisplayed = false;
    }
    else {
        $(".userAndCartDiv").css('display', 'none');
        $(".userAndCartDiv").html("");
        loginIsDisplayed = false;
        cartIsDisplayed = false;
    }
}

function ShowCart() {
    
    if (cartIsDisplayed === false) {
        CheckIfDisplayed();
        $(".userAndCartDiv").css('display', 'block');
        $.ajax({
            url: "/Product/GetCartPartial",
            type: "GET",
            success: function (result) {
                $(".userAndCartDiv").html(result);
            }
        });
        cartIsDisplayed = true;
        loginIsDisplayed = false;
    }
    else {
        $(".userAndCartDiv").css('display', 'none');

        $(".userAndCartDiv").html("");
        cartIsDisplayed = false;
        loginIsDisplayed = false;
    }
}

//******************************************   PROFILE   **************************************************//


function GetProfile() {

    $.ajax({
        url: "/Account/MyProfile",
        type: "GET",
        success: function (result) {
            $(".profileContentDiv").html(result);
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

function GetOrders() {
    $.ajax({
        url: "/Profile/MyOrders",
        type: "GET",
        success: function (result) {
            $(".profileContentDiv").html(result);
        }
    });


}

function GetSpecificOrder(id) {
    $.ajax({
        url: "/Profile/MySpecificOrder/" + id,
        type: "GET",
        success: function (result) {
            $(".profileContentDiv").html(result);
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
//**************************************   CUSTOMER SUPPORT   *********************************************//


function GetCustomerSupport() {


}

function CustomerSupportMenuOption(id) {
 
    $.ajax({
        url: "/CustomerSupport/CustomerSupport/" + id,
        type: "GET",
        success: function (result) {
            $(".customerSupportContentDiv").html(result);
        }
    });

}

//******************************************   OTHER   ****************************************************//


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


function EnableButton() {
    if ($('#checkBoxTerms').is(':checked')) {
        $('#confirmOrderBtn').removeAttr("disabled");
    }
    else {
        $('#confirmOrderBtn').attr("disabled", "disabled");
    }
    
}

function ToggleCardInput(id) {
    if (id == 1) {
        $('.paymentMethodInnerInnerDiv').css("visibility", "visible")
    }
    else {
        $('.paymentMethodInnerInnerDiv').css("visibility", "hidden")
    }
}