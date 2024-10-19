window.ShowToastr = (type, message) => {
    if (type === "added") {
        toastr.success(message, "Added  Successful", { setTimeout: 2000 });
    }
    if (type === "logged") {
        toastr.success(message, "Login Successful", { setTimeout: 2000 });
    }
    if (type === "updated") {
        toastr.success(message, "Updated  Successful", { setTimeout: 2000 });
    }
    if (type === "deleted") {
        toastr.error(message, "Deleted  Successful", { setTimeout: 2000 });
    }
    if (type === "error") {
        toastr.error(message, "Error", { setTimeout: 2000 });
    }
    if (type === "Search") {
        toastr.error(message, "Search Not Exist", { setTimeout: 2000 });
    }          
}

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "1500",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

