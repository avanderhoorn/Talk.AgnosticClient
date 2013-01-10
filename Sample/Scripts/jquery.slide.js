$(function () { 
    var slideModeOn = localStorage.getItem('slideModeOn');

    if (slideModeOn == 1) {
        $('article:first').addClass('selected');
        $('article:not(.selected)').hide();
            
        $('.slide-navigation a:first').click(function (event) {
            trigger(-1, event);
        });
        $('.slide-navigation a:last').click(function (event) {
            trigger(1, event);
        });

        var trigger = function(type, event) {
            var selected = $('article.selected'),
                sibling = type === 1 ? selected.nextAll('article:first') : selected.prevAll('article:first');
                
            if (sibling.length == 1) {
                //Hide/show what we need
                selected.hide().removeClass('selected');
                sibling.show().addClass('selected');
            
                //Manage content
                $('.content .selected').removeClass('selected');
                $('.content li:eq(' + sibling.index('article') + ')').addClass('selected');

                //Hold back click event
                if (event)
                    event.preventDefault();
            }
        };
    }
    
    
    //Dynamically setup content
    var contentHtml = '<h3>Content</h3><ul class="content">';
    $('article h5:first-child').each(function () {
        contentHtml += '<li>' + $(this).text() + '</li>';
    });
    contentHtml += '</ul>';
    $('aside').append(contentHtml);
    
    if (slideModeOn == 1) {
        $('.content li:first').addClass('selected');
    }
});