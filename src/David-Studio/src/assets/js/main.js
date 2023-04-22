// jQuery to collapse the navbar on scroll
$(window).scroll(function() {
    var nav = $('.navbar');
    if (nav.length) {
        if (nav.offset().top > 50) {
            $(".navbar-custom").addClass("top-nav-collapse");
        } else {
            $(".navbar-custom").removeClass("top-nav-collapse");
        }
    }
});

// jQuery to add padding for path
// jQuery for page scrolling feature - requires jQuery Easing plugin
$( document ).ready(function() {
    if($('#path').length) {
        $(".navbar-custom").addClass("path-padding");
    }
});
