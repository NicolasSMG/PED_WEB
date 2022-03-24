
function verificarOnline() {
    return navigator.onLine;
}
function isOnline() {

    if (navigator.onLine) {
        mostrarToast('info', 'Online');
    }
    else {
        mostrarToast('info', 'Offline');
    }
}
function mostrarToast(icon, title) {
    Toast.fire({
        icon: icon,
        title: title
    });
}

async function generarImagenCanvas(canvasId) {
    let blob = await generarBlob(canvasId);
    var canvas = document.getElementById(canvasId);
    canvas.remove();
    var reader = new FileReader();
    reader.readAsDataURL(blob);
    return new Promise((function (resolve) {
        reader.onloadend = function () {
            var base64data = reader.result;
            resolve(base64data);
        }
    }));
}

function getBase64Image(imageId) {
    var img = document.getElementById(imageId);
    var canvas = document.createElement("canvas");
    canvas.height = img.naturalHeight;
    canvas.width = img.naturalWidth;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL;
}

async function generarBlob(canvasId) {
    var canvas = document.getElementById(canvasId);
    if (canvas !== null) {
        if (canvas.toBlob) {
            return new Promise(function (resolve) {
                canvas.toBlob(function (blob) {
                    resolve(blob);
                });
            });
        }
    }
}
const Toast = Swal.mixin({
    toast: true,
    position: 'bottom-start',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', swal.stopTimer)
        toast.addEventListener('mouseleave', swal.resumeTimer)
    }
})

function mostrarMensajeConfirmacion(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si'
        }).then((result) => {
            resolve(result.isConfirmed)
        })
    })
}

async function mostrarMensajeTextoConfirmacion(title, text, value) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            input: 'text',
            inputLabel: text,
            inputValue: value,
            showCancelButton: true,
            inputValidator: (value) => {
                if (!value) {
                    return 'Por favor ingrese la información!'
                }
            }
        }).then((result) => {
            resolve(result.value)
        })
    })

}

window.addEventListener('online', isOnline);
window.addEventListener('offline', isOnline);

navigator.serviceWorker.ready.then(function (reg) {
    // updatefound is fired if service-worker.js changes.
    reg.onupdatefound = function () {
        var installingWorker = reg.installing;

        installingWorker.onstatechange = function () {
            switch (installingWorker.state) {
                case 'installed':
                    if (navigator.serviceWorker.controller) {
                        Snackbar.show({
                            text: 'Hay una versión disponible, desea actualizar?',
                            actionText: 'Actualizar',
                            width: '475px',
                            actionTextColor: '#ffffff',
                            duration: 0,
                            onActionClick: function (element) {
                                element.style.opacity = "0";
                                reg.waiting.postMessage('skipWaiting');
                            }
                        });
                        break;
                    }
            };
        };
    }
});
var refreshing;
navigator.serviceWorker.addEventListener('controllerchange',
    function () {
        if (refreshing) return;
        refreshing = true;
        window.location.reload();
    }
);


