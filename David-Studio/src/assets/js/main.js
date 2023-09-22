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
