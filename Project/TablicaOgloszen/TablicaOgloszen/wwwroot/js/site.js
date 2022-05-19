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
    $('.bi').hover(function () {
        if ($(this).parents('.ratings').length) {
            $(this).addClass('rating-color');
            $(this).parent().parent().prevAll().children('a').children('.bi').addClass('rating-color');
            $(this).parent().parent().nextAll().children('a').children('.bi').removeClass('rating-color');
        }
    }, function () {
        if ($(this).parents('.ratings').length) {
            $(this).removeClass('rating-color');
            $(this).parent().parent().prevAll().children('a').children('.bi').removeClass('rating-color');
        }
    });
});