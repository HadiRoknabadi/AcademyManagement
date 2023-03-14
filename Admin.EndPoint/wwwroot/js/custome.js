function open_waiting(selector = 'body') {
    $(selector).waitMe({
        effect: 'facebook',
        text: 'لطفا صبر کنید ...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000'
    });
}

function close_waiting(selector = 'body') {
    $(selector).waitMe('hide');
}

/****** start CK Editor ******/
$(document).ready(function () {

    var editors = $("[ckeditor]");
    if (editors.length > 0) {
        open_waiting();

        $.getScript('/Admin/js/ckeditor.js', function () {


            $(editors).each(function (index, value) {

                var id = $(value).attr('ckeditor');
                ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'), {


                        language: 'fa',
                        image: {
                            toolbar: [
                                'imageTextAlternative',
                                'imageStyle:inline',
                                'imageStyle:block',
                                'imageStyle:side'
                            ]
                        },
                        table: {
                            contentToolbar: [
                                'tableColumn',
                                'tableRow',
                                'mergeTableCells'
                            ]
                        },
                        licenseKey: '',
                        simpleUpload: {
                            // The URL that the images are uploaded to.
                            uploadUrl: '/Uploader/UploadImage'
                        }


                    })
                    .then(editor => {
                        window.editor = editor;




                    })
                    .catch(error => {
                        console.error('Oops, something went wrong!');
                        console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
                        console.warn('Build id: f443yzl101re-knhuhha922ax');
                        console.error(error);
                    });
            });
            close_waiting();

        });

    }

});
/****** end CK Editor ******/


function FillPageId(pageId) {
    $("#PageId").val(pageId);
    $("#filter-form").submit();
}

function ImagePreview(imageId, imagePreviewId = 0) {
    document.getElementById(imageId).onchange = function () {
        var reader = new FileReader();

        reader.onload = function (e) {
            // get loaded data and render thumbnail.
            if (imagePreviewId == 0) {
                document.getElementById("ImagePreview").src = e.target.result;
            } else {
                document.getElementById(imagePreviewId).src = e.target.result;
            }

        };

        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
    };
}

function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 4000,
        theme: theme !== '' ? theme : 'success'
    })({
        title: title !== '' ? title : 'اعلان',
        message: decodeURI(text)
    });
}

$('[ajax-url-button]').on('click', function (e) {

    e.preventDefault();

    var url = $(this).attr('href');

    IsHide = $(this).attr('Hide');

    var itemId = $(this).attr('ajax-url-button');
    swal({
        title: 'اخطار',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید؟",
        icon: "warning",
        buttons: {
            catch: {
                text: "بله",
                value: "catch",
            },
            cancel: "خیر",

        },
    }).then((value) => {
        switch (value) {
            case "catch":
                $.get(url).then(result => {
                    switch (result.status) {
                        case "Success":
                            ShowMessage('موفقیت', result.message);
                            if (IsHide == true || IsHide == null) {
                                $('#ajax-url-item-' + itemId).hide(1500);

                            } else {
                                location.reload();
                            }
                            break;

                        case "Warning":
                            ShowMessage('خطا', result.message, 'warning');
                            break;

                        case "Error":
                            ShowMessage('خطا', result.message, 'error');
                            break;

                    }

                });
                break;
            default:
                swal({
                    title: 'پیغام',
                    text: 'عملیات لغو شد',
                    icon: "error",
                    buttons: 'بسیارخوب'
                });
                break;

        }
    });

});

function OnSuccessAddOrEditItem(res) {
    switch (res.status) {
        case 'Success':
            ShowMessage('اعلان موفقیت', res.message);
            $('.close').click();
            setTimeout(function () {
                location.reload();
            }, 2000)
            break;

        case 'Error':
            ShowMessage('اخطا', res.message, 'error');
            $('.close').click();
            break;

        case 'Warning':
            ShowMessage('خطا', res.message, 'warning');
            break;
    }
}



function createAutoCompleteForTerm(state) {
    var teachers = [];
    $.get("/Admin/GetTeachersJson", function (res) {

        teachers.push(JSON.parse(res));


    });
    var options = {

        url: function (phrase) {
            return "/Admin/GetLessonsJson?lessonName=" + phrase;

        },

        getValue: "name",

        list: {
            match: {
                enabled: true
            },

            onChooseEvent: function () {
                var value = $("#LessonName").getSelectedItemData().id;

                if (state === 'AddTerm') {
                    var isExistSelectedLesson = $('[selected-lesson][value="' + value + '"]');


                    if (isExistSelectedLesson.length === 0) {
                        var lessonName = $("#LessonName").getSelectedItemData().name;
                        var lessonIdNode = `<input name="LessonIds" selected-lesson="${value}" type="hidden" value="${value}" />`;

                        $('#add-term').append(lessonIdNode);


                        var randomNumber = Math.random();
                        var selectedLessonTagNode = `<row>
                        <div class="col-sm-6">
                        <div class="form-group">
                        <label>درس انتخاب شده</label>
                        <input class="form-control" value="${lessonName}" disabled/>
                        <span class="text-danger"></span>
                        </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>مدرس درس</label>
                                <select class="form-control" selection-id="${randomNumber}">
                                <option value="0">لطفا مدرس را انتخاب کنید</option>
                                </select>
                                <span class="text-danger"></span>
                            </div>
                        </div>
                        </row>`;

                        $('#term-details').append(selectedLessonTagNode);

                        if (teachers.length > 0) {
                            for (var i = 0; i < teachers.length; i++) {
                                var selectListNode = `<option value="${teachers[0][i].Id}">${teachers[0][i].FullName}</option>`;
                                $('[selection-id="' + randomNumber + '"]').append(selectListNode);
                            }
                        }
                        $("#LessonName").val('');
                        $("#LessonName").focus();

                    } else {
                        ShowMessage('اخطار', 'درس انتخاب شده تکراری است', 'warning');
                        $("#LessonName").val('');
                        $("#LessonName").focus();

                    }
                } else {
                    $('#LessonId').val(value);
                }




            }

        },

        theme: "square"
    };

    $("#LessonName").easyAutocomplete(options);




}

$('#addTermBtn').on('click', function (e) {
    e.preventDefault();

    var selectlists = $('[selection-id]');
    if (selectlists.length > 0) {
        for (var i = 0; i < selectlists.length; i++) {
            if (selectlists[i].value == 0) {
                selectlists[i].classList.add("border-1px-solid-red");
                ShowMessage('اخطار', 'لطفا مدرسی برای درس انتخاب شده مشخص کنید', 'warning');

            }

            selectlists[i].onchange =function(){TeacherSelectListOnChange(this)};


        }


    }

});

function TeacherSelectListOnChange(element) {
    var selectionId=element.attributes[1].value;

    $('[selection-id="' + selectionId + '"]').removeClass("border-1px-solid-red");

}


function CreateDatePicker(inputId) {
    kamaDatepicker(inputId, {
        placeholder: "1400/01/01",
        buttonsColor: "red",
        forceFarsiDigits: true,
        markToday: true,
        markHolidays: true
    });

}

function CheckImageIsNull(formId, btnSubmitId, fileInputId, name) {
    $("#" + btnSubmitId).on('click', function () {

        var fileInput = $("#" + fileInputId).val();
        if (fileInput == '') {
            ShowMessage('اخطار', `لطفا تصویری را برای ${name} انتخاب کنید`, 'warning');
            return false;
        }


        $("#" + formId).submit();

    });

}