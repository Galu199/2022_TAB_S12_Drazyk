// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//window.onload = function () {
//    if (window.jQuery) {
//        // jQuery is loaded  
//        alert("Yeah!");
//    } else {
//        // jQuery is not loaded
//        alert("Doesn't Work");
//    }
//}

$(document).ready(function () {
    $('.bi-star').hover(function () {
        $(this).addClass('rating-color');
        $(this).parent().prevAll().children('.bi-star').addClass('rating-color');
        $(this).parent().nextAll().children('.bi-star').removeClass('rating-color');
    }, function () {
        $(this).removeClass('rating-color');
        $(this).parent().prevAll().children('.bi-star').removeClass('rating-color');
    });
});