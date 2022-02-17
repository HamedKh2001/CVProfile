$(document).ready(function () {
    LoadCkeditor();
});

function LoadCkeditor() {

    if (!document.getElementById("Ckeditor"))
        return;

    $("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");
    CKEDITOR.replace('Ckeditor', { customConfig:'/ckeditor4/ckeditor/config.js' });
}

