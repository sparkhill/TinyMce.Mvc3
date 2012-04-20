/**
 * editor_plugin_src.js
 *
 * Copyright 2009, Moxiecode Systems AB
 * Released under LGPL License.
 *
 * License: http://tinymce.moxiecode.com/license
 * Contributing: http://tinymce.moxiecode.com/contributing
 */

(function () {
    // Load plugin specific language pack
    tinymce.PluginManager.requireLangPack('mediapicker');

    tinymce.create('tinymce.plugins.MediaPickerPlugin', {
        /**
        * Initializes the plugin, this will be executed after the plugin has been created.
        * This call is done before the editor instance has finished it's initialization so use the onInit event
        * of the editor instance to intercept that event.
        *
        * @param {tinymce.Editor} ed Editor instance that the plugin is initialized in.
        * @param {string} url Absolute URL to where the plugin is located.
        */
        init: function (ed, url) {
            // Register the command so that it can be invoked by using tinyMCE.activeEditor.execCommand('mceMediaPicker');
            ed.addCommand('mceMediaPicker', function () {
                ed.windowManager.open({
                    file: url + '/dialog.htm',
                    width: 760 + parseInt(ed.getLang('mediapicker.delta_width', 0)),
                    height: 500 + parseInt(ed.getLang('mediapicker.delta_height', 0)),
                    inline: 1
                }, {
                    plugin_url: url, // Plugin absolute URL
                    mediapicker_url: ed.getParam('mediapicker_url'), // mp url param
                    mediapicker_download_url: ed.getParam('mediapicker_download_url')
                });
            });

            // Register mediapicker button
            ed.addButton('mediapicker', {
                title: 'mediapicker.desc',
                cmd: 'mceMediaPicker',
                image: url + '/img/mediapicker.png'
            });

            // Add a node change handler, selects the button in the UI when a image is selected
            ed.onNodeChange.add(function (ed, cm, n) {
                cm.setActive('mediapicker', n.nodeName == 'IMG');
            });
        },

        /**
        * Creates control instances based in the incomming name. This method is normally not
        * needed since the addButton method of the tinymce.Editor class is a more easy way of adding buttons
        * but you sometimes need to create more complex controls like listboxes, split buttons etc then this
        * method can be used to create those.
        *
        * @param {String} n Name of the control to create.
        * @param {tinymce.ControlManager} cm Control manager to use inorder to create new control.
        * @return {tinymce.ui.Control} New control instance or null if no control was created.
        */
        createControl: function (n, cm) {
            return null;
        },

        /**
        * Returns information about the plugin as a name/value array.
        * The current keys are longname, author, authorurl, infourl and version.
        *
        * @return {Object} Name/value array containing information about the plugin.
        */
        getInfo: function () {
            return {
                longname: 'MediaPicker plugin',
                author: 'Some author',
                authorurl: 'http://tinymce.moxiecode.com',
                infourl: 'http://wiki.moxiecode.com/index.php/TinyMCE:Plugins/mediapicker',
                version: "1.0"
            };
        }
    });

    // Register plugin
    tinymce.PluginManager.add('mediapicker', tinymce.plugins.MediaPickerPlugin);
})();