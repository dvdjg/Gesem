// http://blog.apterainc.com/bid/313071/Turn-your-ASP-NET-MVC-app-into-a-Single-Page-Application-with-one-classy-Sammy-js-route
var Routing = function (appRoot, contentSelector, defaultRoute) {

    function getUrlFromHash(hash) {
        var url = hash.replace('#/', '');
        if (url === appRoot)
            url = defaultRoute;
        return url;
    }

    return {
        init: function () {
            Sammy(contentSelector, function () {
                this.get(/\#\/(.*)/, function (context) {
                    var url = getUrlFromHash(context.path);
                    context.load(url).swap();
                });
            }).run('#/');
        }
    };
}
