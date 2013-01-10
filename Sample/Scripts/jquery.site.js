$(function () { 
    var setupButton = function () {
        var slideModeOn = localStorage.getItem('slideModeOn'); 
        $('#slideMode').val(slideModeOn == 0 ? 'Off' : 'On');
    };
    
    $('#slideMode').click(function() {
        var slideModeOn = localStorage.getItem('slideModeOn');
        localStorage.setItem('slideModeOn', slideModeOn == 0 ? 1 : 0);
        setupButton();
        document.location.reload(true);
    });
    setupButton();
});