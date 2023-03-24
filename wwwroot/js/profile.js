$(document).ready(function () {
    var src_img = $("#img_profile").attr("src");
    $("#btn_upload").click(function () {
        $("#user_img").trigger("click");
    });
    $("#user_img").change(function () {
        var file = this.files[0];
        var file_type = file.type;
        if (!file_type.startsWith("image/")) {
            $("#user_img").val("");
            swal("Warning!", "Please upload an image file!", "warning");
        } else {
            $("#img_profile").attr("src", URL.createObjectURL(file));
            $("#btn_upload").css("display", "none");
            $("#btn_wrapper").css("display", "");
        }
    });
    $("#cancel_img").click(function () {
        $("#user_img").val("");
        $("#img_profile").attr("src", src_img);
        $("#btn_upload").css("display", "");
        $("#btn_wrapper").css("display", "none");
    });

    $("#change_img").click(function () {
        var formData = new FormData();
        formData.append("user_img", $("#user_img").get(0).files[0]);
        $.ajax({
            type: "POST",
            url: "/Profile/Index?handler=UploadImage",
            contentType: false,
            processData: false,
            data: formData,
            headers: {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response == "error") {
                    swal("Error!", "Upload Error!", "error");
                } else {
                    src_img = response;
                    $("#cancel_img").trigger("click");
                    swal("Success!", "Change profile image successfull!", "success");
                }
            },
            error: function (xhr, status, error) {
                swal("Error!", "Server Error!", "error");
            }
        });
    });

    $("#edit_profile").click(function () {
        var form_profile = $("#form_profile");
        if (form_profile.valid()) {
            $.ajax({
                type: "POST",
                url: "/Profile/Index?handler=UpdateProfile",
                data: form_profile.serialize(),
                headers: {
                    RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    swal("Success!", "Update profile successfull!", "success");
                },
                error: function (xhr, status, error) {
                    swal("Error!", "Server Error!", "error");
                }
            });
        }
    });

    $("#change_password").click(function () {
        var form_password = $("#form_password");
        if (form_password.valid()) {
            $.ajax({
                type: "POST",
                url: "/Profile/Index?handler=ChangePassword",
                data: form_password.serialize(),
                headers: {
                    RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    switch (response) {
                        case 0:
                            swal("Success!", "Change password successfull!", "success");
                            form_password.trigger("reset");
                            break;
                        case 1:
                            swal("Warning!", "New and confirm password doesn't match!", "warning");
                            break;
                        case 2:
                            swal("Error!", "Your password incorrect!", "error");
                            break;
                    }
                },
                error: function (xhr, status, error) {
                    swal("Error!", "Server Error!", "error");
                }
            });
        }
    });
});
