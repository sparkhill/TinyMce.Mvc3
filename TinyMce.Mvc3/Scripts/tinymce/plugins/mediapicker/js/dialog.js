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