jQuery(document).ready(

function () {
    jQuery('.tabs .tab-links a').on('click', function (e) {
        var currentAttrValue = jQuery(this).attr('href');

        // Show/Hide Tabs
        jQuery('.tabs ' + currentAttrValue).show().siblings().hide();

        // Change/remove current tab to active
        jQuery(this).parent('li').addClass('active').siblings().removeClass('active');
        e.preventDefault();
    });



    function setNav() {

        var path = window.location.pathname;
        path = path.replace(/\$/, "");
        path = decodeURIComponent(path);

        $("nav a").each(function()
        {
            var href = $(this).attr('href');
            if (path.substring(0, href.length) == href) {
                $(this).closest('li').addClass('active');
            }
        }
        
        )

    }//active



}); //Function 1