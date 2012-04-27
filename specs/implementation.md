## Settings

all settings should be applied from the web.config or in tinymce setup.  Server specific settings will be applied in the web.config
while client specific settings will be applied in the javascript call of tinymce.

web.config settings:

- MediaDirectory: this will be the physical or relative location of the storage container for this plugin

tinymce settings:

- mediapicker_url: this is the url of the controller tinymce will use to get files, upload files, and create directories
- mediapicker_download_url: this is the url of the file download action
- mediapicker_restful: the default mediapicker download url will create download urls using the following format: controller/action?path=<path>.  If you
are all about using restful urls, this mode will reformat the url like so: controller/action/path.  This, of course, will require additional setup in the
global.asax.

## Browse Images Tab

this tab will insert an <img /> tag into tinymce

- browsable folder list
- breadcrumbs
- create folder form
- upload image form
- image url
- alt text
- class
- style
- alignment
- width
- height
- lock aspect ratio

- insert
- cancel

## Browse Other Files Tab

this tab will insert an <a> tag into tinymce

- browsable folder list
- breadcrumbs
- create folder form
- upload file form
- file url
- class
- style

- insert
- cancel

## File Browser/Breadcrumb Object

on each request, image upload, or directory creation, the controller will
return an object that encapsulates a list of breadcrumb objects and a list
of file browser objects.  These objects will be deserialized and displayed
with javascript.

### breadcrumb object

- Name
- Url
- must be in proper order

### file browser object

- IsDirectory
- Url
- Name
- Icon
- Size
- must be in proper order