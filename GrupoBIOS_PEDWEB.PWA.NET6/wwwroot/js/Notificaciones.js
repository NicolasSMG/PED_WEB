
async function obtenerEstatusPermisoNotificaciones() {
    const estatusPermiso = Notification.permission;
    if (estatusPermiso === 'denied') { return estatusPermiso; }

    const worker = await navigator.serviceWorker.getRegistration();
    const suscripcionExistente = await worker.pushManager.getSubscription();

    if (suscripcionExistente) { return "granted"; }
    else { return "default"; }
}

async function suscribirUsuario(llavePublica) {

    var permisoNotificacion = await Notification.requestPermission();

    if (permisoNotificacion !== 'granted') {
        return null;
    }

    const worker = await navigator.serviceWorker.getRegistration();
    const suscripcionExistente = await worker.pushManager.getSubscription();

    if (!suscripcionExistente) {
        const nuevaSuscripcion = await worker.pushManager.subscribe({
            userVisibleOnly: true,
            applicationServerKey: llavePublica
        });

        return construirRespuestaSuscripcion(nuevaSuscripcion);
    } else {
        return construirRespuestaSuscripcion(suscripcionExistente);
    }
}

async function desuscribirUsuario() {
    const worker = await navigator.serviceWorker.getRegistration();
    const suscripcionExistente = await worker.pushManager.getSubscription();

    if (suscripcionExistente) {
        suscripcionExistente.unsubscribe();
        return construirRespuestaSuscripcion(suscripcionExistente);
    }
}

function construirRespuestaSuscripcion(suscripcion) {
    return {
        url: suscripcion.endpoint,
        p256dh: arrayBufferToBase64(suscripcion.getKey('p256dh')),
        auth: arrayBufferToBase64(suscripcion.getKey('auth'))
    }
}

function arrayBufferToBase64(buffer) {
    // https://stackoverflow.com/a/9458996
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}