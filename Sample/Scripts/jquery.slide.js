$(function () { 
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
            selected.hide().removeClass('selected');
            sibling.show().addClass('selected');
                    
            if (event)
                event.preventDefault();
        }
    };
});