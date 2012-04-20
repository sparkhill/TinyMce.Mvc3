(function () {

    var baseUrl, downloadUrl, currPath;

    $(function () {
        baseUrl = tinyMCEPopup.getWindowArg('mediapicker_url');
        downloadUrl = tinyMCEPopup.getWindowArg('mediapicker_download_url');
        loadMedia('');
    });

    $("form.folder-creation").live('submit', function () {
        var t = $(this), url, data;


        url = baseUrl + "/CreateFolder?path=" + currPath;
        data = t.serialize();

        $.post(url, data, function () {
            loadMedia(currPath);
            $('.folder-creation input[type="text"]').val('');
        });

        return false;
    });

    $(".media-list a").live('click', function () {
        var t = $(this);

        if (t.data('isdirectory'))
            directoryClicked(t);
        else
            fileClicked(t);

        return false;
    });

    $(".breadcrumbs a").live('click', function () {
        var t = $(this);
        directoryClicked(t);
        return false;
    });

    function loadMedia(path) {
        var url = baseUrl + "/GetImages?" + $.param({ path: path });
        currPath = path;

        $.get(url, function (data) {
            deserializeTransport(data);
        });
    }

    function deserializeTransport(obj) {
        var i, b, m;

        $(".media-list li").remove();
        $(".breadcrumbs").html('');

        b = obj.Breadcrumbs;
        m = obj.Mediae;

        for (i = 0; i < m.length; i++) {
            addToMediaList(m[i]);
        }

        for (i = 0; i < b.length; i++) {
            addBreadcrumb(b[i]);
        }
    }

    function addToMediaList(m) {
        var list, name, url, isDirectory, item, contentType;

        list = $(".media-list");
        name = m.Name;
        url = m.Url;
        isDirectory = m.IsDirectory;
        contentType = m.ContentType;
        item = '<li><a class="' + contentType + '" href="#" data-isdirectory="' + isDirectory + '" data-path="' + url + '">' + name + '</a></li>';
        list.append(item);
    }

    function addBreadcrumb(b) {
        var name, path, container, a;
        container = $(".breadcrumbs");
        name = b.Name;
        path = b.Url;
        a = '<a href="#" data-path="' + path + '">' + name + '</a> / ';
        container.append(a);
    }

    function directoryClicked(e) {
        var path = e.data("path");
        loadMedia(path);
    }

    function fileClicked(e) {
        var url = e.data("path");

        url = downloadUrl + "?path=" + url;

        $("#ImageUrl").val(url);
    }

})();