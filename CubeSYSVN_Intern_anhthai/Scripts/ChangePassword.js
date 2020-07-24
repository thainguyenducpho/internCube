function showChangePasswordClick() {
    showPopup("", "#changePasswordModal", "")
    $("input[name='PASSWORD']").val("")
    $("input[name='OLD_PASSWORD']").val("")
    $("input[name='CONFIRM_PASSWORD']").val("")
}

function btnChangePasswordClick() {
    var USERNAME = $(".USERNAME").text()
    var oldPassword = $("input[name='OLD_PASSWORD']").val()
    var newPassword = $("input[name='PASSWORD']").val()
    var cfmPassword = $("input[name='CONFIRM_PASSWORD']").val()

    if (!checkInput(oldPassword, newPassword, cfmPassword)) {
        return false
    }

    var UserInfo = {}
    UserInfo.USERNAME = USERNAME
    UserInfo.PASSWORD = newPassword
    UserInfo.OLD_PASSWORD = oldPassword
    UserInfo.CONFIRM_PASSWORD = cfmPassword
    $.ajax({
        type: "POST",
        url: window.applicationBaseUrl + "User/ChangePassword",
        data: "{UserInfo: " + JSON.stringify(UserInfo) + "}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.notify(data.message, {
                globalPosition: "top center",
                className: "success"
            })
            hidePopup("#changePasswordModal")
            showPopup("#messageErrorPass", "#errorModalPass", I0007)
        },
        error: function (response) {

            $("input[name='PASSWORD']").val("")
            $("input[name='OLD_PASSWORD']").val("")
            $("input[name='CONFIRM_PASSWORD']").val("")
            showPopup("#messageErrorPass", "#errorModalPass", response.responseText)
        }
    });
}

function checkInput(oldPassword, newPassword, cfmPassword) {
    var isSuccess = true
    //Check null
    if (oldPassword === "") {
        $("input[name = 'OLD_PASSWORD']").addClass("error")
        $(".MSG_OLD_PASSWORD").text(ERR0022)
        isSuccess = false
    }

    if (newPassword === "") {
        $("input[name = 'PASSWORD']").addClass("error")
        $(".MSG_PASSWORD").text(ERR0023)
        isSuccess = false
    }

    if (cfmPassword === "") {
        $("input[name = 'CONFIRM_PASSWORD']").addClass("error")
        $(".MSG_CONFIRM_PASSWORD").text(ERR0024)
        isSuccess = false
    }

    if (newPassword !== cfmPassword) {
        $("input[name = 'PASSWORD']").addClass("error")
        $("input[name = 'CONFIRM_PASSWORD']").addClass("error")
        $(".MSG_CONFIRM_PASSWORD").text(ERR0025)
        isSuccess = false
    }
    return isSuccess
}

function textChanged($this) {
    $("input[name = '" + $this.name + "']").removeClass("error")
    $(".MSG_" + $this.name).text("")

    if ($this.name === "PASSWORD" && $("input[name = 'CONFIRM_PASSWORD']").val() !== "") {
        $("input[name = 'CONFIRM_PASSWORD']").removeClass("error")
        $(".MSG_CONFIRM_PASSWORD").text("")
    } else if ($this.name === "CONFIRM_PASSWORD" && $("input[name = 'PASSWORD']").val() !== "") {
        $("input[name = 'PASSWORD']").removeClass("error")
        $(".MSG_CONFIRM_PASSWORD").text("")
    }
}