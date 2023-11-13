// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var page = $('#mainPage')

    $('button[data-toggle="add-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            page.html(data)
            page.find('.modal').modal('show');
        })
    })

    page.on('click', '[data-save="add-modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        var dataAsArray = form.serializeArray();


        console.log(actionUrl)
        console.log(sendData)
        console.log(dataAsArray)

        var name = dataAsArray[0].value;
        var phoneNumber = dataAsArray[1].value;
        var jobTitle = dataAsArray[2].value;
        var birthDate = dataAsArray[3].value;

        if (!parseBirthDate(birthDate) && !parsePhoneNumber(phoneNumber)) {
            alert("Wrong BirthDate! Format: MM/DD/YYYY\nWrong PhoneNumber! Format: +375 (xx) 123-45-67")
        } else if (!parsePhoneNumber(phoneNumber)) {
            alert("Wrong PhoneNumber! Format: +375 (44) 123-45-67")
        } else if (!parseBirthDate(birthDate)) {
            alert("Wrong BirthDate! Format: MM/DD/YYYY")
        } else {
            $.post(actionUrl, sendData).done(function (data) {
                page.find('.modal').modal('hide');
                location.reload();
            })
        }
    })
})

function parseBirthDate(inputDate) {
    let pattern = /^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/;
    var timestamp = Date.parse(inputDate);
    var dateObject = new Date(timestamp);

    if (pattern.test(inputDate) && dateObject == 'Invalid Date') {
        return false;
    } else {
        return true;
    }
}

function parsePhoneNumber(inputPhoneNumber) {
    let pattern = /^\+375 \((17|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$/;

    if (pattern.test(inputPhoneNumber)) {
        return true;
    } else {
        return false;
    }
}