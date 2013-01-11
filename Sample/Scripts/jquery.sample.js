var sample = (function () {
    var metadata = {},
        listing = {},
        currentQuote = '',
        init = function (data) {
            $.get(metadata.Resources.Listing, function(data) {
                listing = data;
                render(data[0], 0);
            });
        },
        render = function(item, index) {
            currentQuote = item.Id;
            
            $('.layout').html('<div><h3></h3><table><thead><tr><th>Sell<th><th>Buy<th></tr></thead><tbody><tr><td class="sample-sell"><td><td class="sample-buy"><td></tr></tbody></table><div class="sample-news"><ul></ul></div><input type="button" value="Prev" class="sample-prev" /><input type="button" value="Next" class="sample-next" /><div>');
            
            var prev = $('.layout .sample-prev'),
                next = $('.layout .sample-next'),
                heading = $('.layout h3');
            
            if (index > 0) {
                prev.click(function() {
                    render(listing[index - 1], index - 1);
                });
            } else {
                prev.hide();
            }
            if (index != listing.length - 1) {
                next.click(function() {
                    render(listing[index + 1], index + 1);
                });
            } else {
                next.hide();
            }
            heading.html(item.Name);
            
            getQuotes(item.Id, findUri(item.Links, '/Rel/Sample/Quote'));
            getNews(item.Id);
        },
        getQuotes = function(currency, uri) {
            $.get(uri, { id: currency }, function(data) {
                if (data.Id == currentQuote) {
                    $('.sample-sell').html(data.Sell);
                    $('.sample-buy').html(data.Buy);
                    setTimeout(function() {
                        if (data.Id == currentQuote) {
                            getQuotes(currency, uri);
                        }
                    }, 1000);
                }
            });
        },
        getNews = function (currency) {
            if (metadata.Resources.News) {
                $.ajax({
                    url: metadata.Resources.News, 
                    tokens: { id: currency },
                    success: function(data) {
                        var list = $('.sample-news ul');
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            list.append('<li><a href="' + findUri(item.Links, '/Rel/Sample/News') + '">' + item.Title + '</a> - ' + item.Source + ' (' + item.Date + ')</li>');
                        }
                    }
                });
            }
        },
        findUri = function (links, rel) {
            for (var i = 0; i < links.length; i++) {
                if (links[i].Rel == rel) {
                    return links[i].Uri;
                }
            }
        };

    return {
        initMetadata: function(payload) {
            metadata = payload;
            init();
        }
    };
})();