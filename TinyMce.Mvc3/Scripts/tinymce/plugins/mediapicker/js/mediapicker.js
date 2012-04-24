var mediapicker = {
    baseUrl: '',
    downloadUrl: '',
    currPath: '',

    init: function () {
        var t = this;

        t.baseUrl = tinyMCEPopup.getWindowArg('mediapicker_url');
        t.downloadUrl = tinyMCEPopup.getWindowArg('mediapicker_download_url');
        t.loadMedia('');

        $("form.folder-creation").live('submit', function () {
            var url, data;
            url = t.baseUrl + "/CreateFolder?path=" + t.currPath;
            data = $(this).serialize();

            $.post(url, data, function () {
                t.loadMedia(t.currPath);
                $('.folder-creation input[type="text"]').val('');
            });

            return false;
        });

        $("form.upload-image").live('submit', function () {
            var url;
            url = t.baseUrl + "/UploadFile?path=" + t.currPath;
            $(this).append('<iframe id="mediapicker-frame" name="mediapicker-frame" height="0" width="0" frameborder="0"></iframe>');
            $(this).attr("target", "mediapicker-frame");
            $(this).attr("action", url);
            return true;
        });

        $(".media-list a").live('click', function () {
            if ($(this).data('isdirectory'))
                t.directoryClicked($(this));
            else
                t.fileClicked($(this));

            return false;
        });

        $(".breadcrumbs a").live('click', function () {
            t.directoryClicked($(this));
            return false;
        });
    },

    loadMedia: function (path) {
        var t = this;
        var url = t.baseUrl + "/GetImages?" + $.param({ path: path });
        t.currPath = path;

        $.get(url, function (data) {
            t.deserializeTransport(data);
        });
    },

    deserializeTransport: function (obj) {
        var t = this, i, b, m;

        $(".media-list li").remove();
        $(".breadcrumbs").html('');

        b = obj.Breadcrumbs;
        m = obj.Mediae;

        for (i = 0; i < m.length; i++) {
            t.addToMediaList(m[i]);
        }

        for (i = 0; i < b.length; i++) {
            t.addBreadcrumb(b[i]);
        }
    },

    addToMediaList: function (m) {
        var list, name, url, isDirectory, item, contentType;

        list = $(".media-list");
        name = m.Name;
        url = m.Url;
        isDirectory = m.IsDirectory;
        contentType = m.ContentType;
        item = '<li><a class="' + contentType + '" href="#" data-isdirectory="' + isDirectory + '" data-path="' + url + '">' + name + '</a></li>';
        list.append(item);
    },

    addBreadcrumb: function (b) {
        var name, path, container, a;
        container = $(".breadcrumbs");
        name = b.Name;
        path = b.Url;
        a = '<a href="#" data-path="' + path + '">' + name + '</a> / ';
        container.append(a);
    },

    directoryClicked: function (e) {
        var t = this, path = e.data("path");
        t.loadMedia(path);
    },

    fileClicked: function (e) {
        var t = this, url, noCacheUrl, img;
        url = e.data("path");
        url = t.downloadUrl + "?path=" + url;
        noCacheUrl = url + '&' + Number.random(1, 9999);
        img = '<img src="' + noCacheUrl + '" />';
        $("#image-preview").html(img);
        $("#ImageUrl").val(url);
    },

    // TODO: loading spinner and clear input needed.
    uploadComplete: function (code) {
        var t = this, form, iframe, image;
        form = $("form.upload-image");
        iframe = $("#mediapicker-frame");
        image = $("#TheFile");
        iframe.remove();
        form.attr('target', '');
        image.val('');
        t.loadMedia(t.currPath);
    }
};