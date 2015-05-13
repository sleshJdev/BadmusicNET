/************ИЗМЕНЕНИЕ ТЕМЫ
==========================================*/   
$('ul#weather').show();

$('li.sunny').click(function () {
    $('body').removeClass();
    $('body').addClass('sunny');
    return false;
});
$('li.rainy').click(function () {
    $('body').removeClass();
    $('body').addClass('rain');

    return false;
});
$('li.snowy').click(function () {
    $('body').removeClass();
    $('body').addClass('snow');
    return false;
});
$('li.default').click(function () {
    $('body').removeClass();
    $('body').addClass('default');
    return false;
});
 

/*ВСТАВКА КОНТЕКСТА ДЛЯ ПОИСКА В ИНТПУТ
==========================================*/
var _input = $('#appendedInputButtons');

$('#artist').click(function () {
    _input.val('Avril Lavigne');
})
$('#track').click(function () {
    _input.val('Back in Black');
})

/***************DRAG AND DROP
======================================================*/
$(document).ready(function () {

    var proccessUpload = $('#img_upload');
    var progressBar = $('.bar');
    var dropZone = $('#dropZone'),
        maxFileSize = 209123123; 

    $('#upload').click(function () {
        proccessUpload.show();
    });

  
    if (typeof (window.FileReader) == 'undefined') {
        dropZone.text('Не поддерживается браузером!');
        dropZone.addClass('error');
    }

    
    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        return false;
    };

 
    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        return false;
    };

   
    dropZone[0].ondrop = function (event) {
        event.preventDefault();
        dropZone.removeClass('hover');
        dropZone.addClass('drop');

        var file = event.dataTransfer.files[0];

 
        if (file.size > maxFileSize) {
            dropZone.text('Файл слишком большой!');
            dropZone.addClass('error');
            return false;
        }

      
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener('progress', uploadProgress, false);
        xhr.onreadystatechange = stateChange;
        xhr.open("POST", "/Music/Upload", true);
         
        xhr.setRequestHeader("Content-Type", "multipart/form-data");
        xhr.send(file);
    };

 
    function uploadProgress(event) {
        var percent = parseInt(event.loaded / event.total * 100);
        progressBar.css('width', percent + '%');
        dropZone.text('Загрузка: ' + percent + '%');
    }

   
    function stateChange(event) {
        if (event.target.readyState == 4) {
            if (event.target.status == 200) {
                dropZone.text('Загрузка успешно завершена!');
                progressBar.css('width', 100 + '%');
            } else {
                dropZone.text('Произошла ошибка!');
                dropZone.addClass('error');
            }
        }
    }
});

/*      ОБЛАКО ТЕГОВ
=================================================*/
var rnumber = Math.floor(Math.random() * 9999999);
var so = new SWFObject("../../Content/scripts/tagcloud.swf?r=" + rnumber, "tagcloudflash", "270", "200", "5");
so.addParam("wmode", "transparent");
so.addParam("allowScriptAccess", "always");
so.addParam("bgcolor", "#FF0000");
so.addVariable("tspeed", "400");
so.addVariable("distr", "true");
so.addVariable("mode", "tags");

$.getJSON("/Site/TagCloud", null, getGenres);

function getGenres(genres) {
    var tags = "<tags>";
    $.each(genres, function (i) {
        tags += "<a href='/Music/Search?IdGenre=" + this.Id + "'>";
        tags += this.Title;
        tags += "</a>";
    });
    tags += "</tags>";
    var value = Math.floor(Math.random() * 30);
    so.addVariable("minFontSize", 20);
    var value = Math.floor(Math.random() * 40);
    so.addVariable("maxFontSize", 40);
    so.addVariable("tcolor", "0x1111ff");
    so.addVariable("tcolor2", "0xffff11");
    so.addVariable("hicolor", "0x111111");
    so.addVariable("tagcloud", tags);
    so.write("3dcloud_block");
};