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