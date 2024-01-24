// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$(".owl-carousel").owlCarousel({
//    loop: true,
//    margin: 10,
//    /*nav: true,*/
//    //dots: ($(".owl-carousel .item").length > 1) ? true : false,
//    loop: false,
//    responsive: {
//        0: {
//            items: 1
//        },
//        600: {
//            items: 3
//        },
//        1000: {
//            items: 3
//        }
//    }
//});


//var topItem = 0,
//    leftItem = 0,
//    popupHeight = 600;

//$(".owl-carousel .item").on("click", function (e) {
//    var $this = $(this),
//        $parent = $this.parents(".owl-carousel-wrap"),
//        content = $this.html(),
//        $popup = $parent.find(".popup");

//    topItem = $this.offset().top - $parent.offset().top + $this.height() / 2;
//    leftItem = $this.offset().left - $parent.offset().left + $this.width() / 2;

//    $popup.css({
//        top: topItem,
//        left: leftItem,
//        width: 0,
//        height: 0
//    });

//    $popup.html(content).stop().animate(
//        {
//            top: -((popupHeight - $this.parent().outerHeight()) / 2),
//            left: 0,
//            width: "100%",
//            height: popupHeight,
//            opacity: 1
//        },
//        400
//    );
//});

//$(".popup").on("click", function (e) {
//    $(this).stop().animate(
//        {
//            width: 0,
//            height: 0,
//            top: topItem,
//            left: leftItem,
//            opacity: 0
//        },
//        400
//    );
//});