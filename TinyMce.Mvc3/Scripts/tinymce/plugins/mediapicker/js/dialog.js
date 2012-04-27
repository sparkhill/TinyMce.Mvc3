tinyMCEPopup.requireLangPack();

var MediaPickerDialog = {
    init: function () {
        var f = document.forms[0];
        var content = tinyMCEPopup.editor.selection.getContent();
        $("#ImageTag").val(content);

        // Get the selected contents as text and place it in the input
        //f.someval.value = tinyMCEPopup.editor.selection.getContent();

        // if this is an image, load up the image tab

        // if this is a file link, load up the insert/upload file and set that tab to active.
    },

    insertImage: function () {
        // Insert the contents from the input into the document
        var src = $("#ImageUrl").val();
        var alt = $("#AltText").val();
        var cssClass = $("#CssClass").val();
        var cssStyles = $("#CssStyles").val();
        var alignment = $("#Alignment").val();
        var width = $("#Width").val();
        var height = $("#Height").val();
        var img = "<img {src}{alt}{cssClass}{cssStyles}{width}{height}/>";

        if (!src.isBlank()) {
            src = 'src="{1}" '.assign(src);
        }

        if (!alt.isBlank()) {
            alt = 'alt="{1}" '.assign(alt);
        }

        img = img.assign({
            src: src,
            alt: alt,
            cssClass: cssClass,
            cssStyles: cssStyles,
            width: width,
            height: height
        });

        alert(img);

        tinyMCEPopup.editor.execCommand('mceInsertContent', false, $("#ImageTag").val());
        tinyMCEPopup.close();
    },

    insertFile: function () {
        // Insert the contents from the input into the document
        tinyMCEPopup.editor.execCommand('mceInsertContent', false, $("#FileLink").val());
        tinyMCEPopup.close();
    }

};

tinyMCEPopup.onInit.add(MediaPickerDialog.init, MediaPickerDialog);