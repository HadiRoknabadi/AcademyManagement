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
                ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'),
                    {


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
            }
            else {
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

                            }
                            else {
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
            setTimeout(function () { location.reload(); }, 2000)
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

/****** start product scripts ******/

$("[main_category_checkbox]").on('change', function (e) {

    isChecked = $(this).is(':checked');
    var selectedCategoryId = $(this).attr('main_category_checkbox');


    if (isChecked) {

        $('#sub_categories_' + selectedCategoryId).slideDown(300);

    }
    else {

        $('#sub_categories_' + selectedCategoryId).slideUp(300);
        var subCategories = $('[parent-category-id="' + selectedCategoryId + '"]');
        $('[parent-category-id="' + selectedCategoryId + '"]').prop('checked', false);
    }
});


$("#add_color_btn").on('click', function (e) {
    e.preventDefault();


    var colorName = $("#product_color_name_input").val();

    var colorCode = $("#product_color_code_input").val();

    var colorPrice = $("#product_color_price_input").val();


    if (colorName == '') {
        ShowMessage('اخطار', 'لطفا نام رنگ را وارد کنید', 'warning');
        return;
    }

    if (colorCode == '') {
        ShowMessage('اخطار', 'لطفا کد رنگ را وارد کنید', 'warning');
        return;
    }

    if (colorPrice == '') {
        ShowMessage('اخطار', 'لطفا قیمت رنگ را وارد کنید', 'warning');
        return;
    }


    var index = $('#list_of_product_colors tr').length;

    var isExistsSelectedColor = $('[color-name-hidden-input][value="' + colorName + '"]');
    if (isExistsSelectedColor.length === 0) {

        var colorNameNode = `<input type="hidden" value="${colorName}" name="ProductColors[${index}].colorName" color-name-hidden-input="${colorName}-${colorPrice}" />`;
        var colorCodeNode = `<input type="hidden" value="${colorCode}" name="ProductColors[${index}].colorCode" color-code-hidden-input="${colorName}-${colorPrice}"  />`;
        var colorPriceNode = `<input type="hidden" value="${colorPrice}" name="ProductColors[${index}].Price" color-price-hidden-input="${colorName}-${colorPrice}"  />`;
    
        $("#ProductForm").append(colorNameNode);
        $("#ProductForm").append(colorCodeNode);
        $("#ProductForm").append(colorPriceNode);
    
    
        var colorTableNode = `<tr color-table-item="${colorName}-${colorPrice}"> <td>${colorName}</td> <td> <div style="margin:0 auto;background-color:${colorCode};border-radius:100%;width:25px;height:25px">&nbsp</div> </td> <td>${colorPrice}</td> <td> <a class="btn btn-danger" onclick="removeProductColor('${colorName}-${colorPrice}')">حذف</a> </td> </tr>`;
    
        $("#list_of_product_colors").append(colorTableNode);
    
        $("#product_color_name_input").val('');
    
        $("#product_color_code_input").val('');
    
        $("#product_color_price_input").val('');
    }
    else{
        ShowMessage('اخطار', 'رنگ وارد شده تکراری می باشد', 'warning');
        $('#product_color_name_input').val('').focus();
    }




});


$("#add_feature_btn").on('click', function (e) {
    e.preventDefault();


    var feature = $("#product_feature_input").val();

    var featureValue = $("#product_feature_value_input").val();


    if (feature == '') {
        ShowMessage('اخطار', 'لطفا نام ویژگی را وارد کنید', 'warning');
        return;
    }

    if (featureValue == '') {
        ShowMessage('اخطار', 'لطفا مقدار ویژگی را وارد کنید', 'warning');
        return;
    }

    var currentFeaturesCount = $('#list_of_product_features tr');
    var index = currentFeaturesCount.length;

    var isExistSelectedFeature = $('[feature-hidden-input][value="' + feature + '"]');
    if (isExistSelectedFeature.length === 0) {
        var featureNode = `<input type="hidden" value="${feature}" name="ProductFeatures[${index}].FeatureTitle" feature-hidden-input="${feature}-${featureValue}" />`;
        var featureValueNode = `<input type="hidden" value="${featureValue}" name="ProductFeatures[${index}].FeatureValue" feature-value-hidden-input="${feature}-${featureValue}"  />`;

        $("#ProductForm").append(featureNode);
        $("#ProductForm").append(featureValueNode);


        var featureTableNode = `<tr feature-table-item="${feature}-${featureValue}"> <td>${feature}</td> <td>${featureValue}</td> <td> <a class="btn btn-danger" onclick="removeProductFeature('${feature}-${featureValue}')">حذف</a> </td> </tr>`;

        $("#list_of_product_features").append(featureTableNode);

        $("#product_feature_input").val('');

        $("#product_feature_value_input").val('');

    }
    else {
        ShowMessage('اخطار', 'نام ویژگی وارد شده تکراری است', 'warning');

    }



})

function removeProductFeature(index) {
    $('[feature-hidden-input="' + index + '"]').remove();
    $('[feature-value-hidden-input="' + index + '"]').remove();
    $('[feature-table-item="' + index + '"]').remove();
    reOrderProductFeatureHiddenInputs();
}


function removeProductColor(index) {
    $('[color-name-hidden-input="' + index + '"]').remove();
    $('[color-price-hidden-input="' + index + '"]').remove();
    $('[color-code-hidden-input="' + index + '"]').remove();
    $('[color-table-item="' + index + '"]').remove();
    reOrderProductColorHiddenInputs();
}


function reOrderProductColorHiddenInputs() {
    var hiddenColors = $('[color-name-hidden-input]');
    $.each(hiddenColors, function (index, value) {
        var hiddenColor = $(value);
        var colorId = $(value).attr('color-name-hidden-input');
        var hiddenPrice = $('[color-price-hidden-input="' + colorId + '"]');
        var hiddenCode = $('[color-code-hidden-input="' + colorId + '"]');
        $(hiddenColor).attr('name', 'ProductColors[' + index + '].ColorName');
        $(hiddenPrice).attr('name', 'ProductColors[' + index + '].Price');
        $(hiddenCode).attr('name', 'ProductColors[' + index + '].ColorCode');
    });
}


function reOrderProductFeatureHiddenInputs() {
    var hiddenFeatures = $('[feature-hidden-input]');
    $.each(hiddenFeatures, function (index, value) {
        var hiddenFeature = $(value);
        var featureId = $(value).attr('feature-hidden-input');
        var featureValue = $('[feature-value-hidden-input="' + featureId + '"]');
        $(hiddenFeature).attr('name', 'ProductFeatures[' + index + '].FeatureTitle');
        $(featureValue).attr('name', 'ProductFeatures[' + index + '].FeatureValue');
    });
}



function createAutoCompleteForProductDiscount(state) {
    var options = {

        url: function (phrase) {
            return "/Admin/Products-autocomplete?productName=" + phrase;
        },

        getValue: "title",

        list: {
            match: {
                enabled: true
            },

            onChooseEvent: function () {
                var value = $("#ProductName").getSelectedItemData().id;

                if (state === 'AddProductDiscount') {
                    var isExistSelectedProduct = $('[selected-product][value="' + value + '"]');
                    if (isExistSelectedProduct.length === 0) {
                        var productName = $("#ProductName").getSelectedItemData().title;
                        var productIdNode = `<input name="ProductIds" selected-product="${value}" type="hidden" value="${value}" />`;

                        $('#addProductDiscountForm').append(productIdNode);


                        var selectedProductTagNode = `<span selected-product="${value}" class="tag label label-info margin-right-5 margin-top-5"> ${productName} <span class="cursor-pointer" onclick="removeSelectedProduct('${value}')"><i class="fa fa-close"></i></span></span>`;

                        $('#selected-products').append(selectedProductTagNode);

                        $("#ProductName").val('');
                        $("#ProductName").focus();

                    }
                    else {
                        ShowMessage('اخطار', 'محصول انتخاب شده تکراری است', 'warning');
                        $("#ProductName").val('');
                        $("#ProductName").focus();

                    }
                }
                else {
                    $('#ProductId').val(value);
                }




            }

        },

        theme: "square"
    };

    $("#ProductName").easyAutocomplete(options);




}
function removeSelectedProduct(selectedProductNumber) {
    $('[selected-product="' + selectedProductNumber + '"]').remove();
}

function CreateDatePicker(inputId) {
    kamaDatepicker(inputId, { placeholder: "1400/01/01", buttonsColor: "red", forceFarsiDigits: true, markToday: true, markHolidays: true });

}

function CheckImageIsNull(formId,btnSubmitId, fileInputId,name) {
    $("#" + btnSubmitId).on('click',function () {

        var fileInput=$("#" + fileInputId).val();
        if (fileInput==''){
            ShowMessage('اخطار',`لطفا تصویری را برای ${name} انتخاب کنید`,'warning');
            return false;
        }


        $("#" + formId).submit();

    });

}



/****** end product scripts ******/

function ConfirmOrder(orderId)
{
    var url='/Admin/ConfirmOrder/'+orderId;
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
                            $('#btn-confirm-order').hide(1500);
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
}
