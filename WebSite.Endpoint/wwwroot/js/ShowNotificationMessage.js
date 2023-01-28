function ShowToastMessageNotification(title, text, theme) {
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


function ShowSweetAlertNotification(title, text, iconName) {
    console.log("test");
    swal({
        title: title !== '' ? title : 'اعلان',
        icon:iconName !== '' ? iconName : 'success',
        text:decodeURI(text)
      });
}

