

//function ShowUserInfo() {
//    $(".userAndCart").css('visibility', 'visible');


//    $.ajax({
//        url: "/Account/GetLoginForm",
//        type: "GET",
//        success: function (result) {
//            $(".userAndCart").html(result);
//        }
//    });
//}

function ShowShoppingCart() {

}




$("#userLogo").mouseenter(function () {
    $(".userAndCart").css('visibility', 'visible');


    $.ajax({
        url: "/Account/GetLoginForm",
        type: "GET",
        success: function (result) {
            $(".userAndCart").html(result);
        }
    });
})

$("#userLogo").mouseleave(function () {
    Leaving();
})

$(".userAndCart").mouseleave(function () {
    Leaving();
})

function Leaving() {
    if ($(".userAndCart:hover").length === 0) {

        $(".userAndCart").html("");
        $(".userAndCart").css('visibility', 'hidden');
    }
}

//function HideUserInfo() {
    //if ($(".userAndCart:hover").length === 0) {

    //    $(".userAndCart").html("");
    //    $(".userAndCart").css('visibility', 'hidden');
    //}

//}

function HideShoppingCart() {

}

function StayAlive() {

}