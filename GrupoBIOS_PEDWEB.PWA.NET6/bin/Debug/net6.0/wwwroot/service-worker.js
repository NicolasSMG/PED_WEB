
self.importScripts('./service-worker-assets.js');
//self.addEventListener('install', event => event.waitUntil(onInstall(event)));
//self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));
self.addEventListener('push', event => event.waitUntil(OnPush(event)));
self.addEventListener('notificationclick', event => event.waitUntil(OnNotificationClick(event)));
self.addEventListener('message', messageEvent => {
    if (messageEvent.data === 'skipWaiting') {
        return self.skipWaiting();
    }
});
const cacheNamePrefix = 'offline-cache-';
const cacheNameDynamic = 'dynamic-cache';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [/\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/];
const offlineAssetsExclude = [/^service-worker\.js$/, /\.scp.css$/];

async function onInstall(event) {
    console.info('Service worker: Install');

    // Fetch and cache all matching items from the assets manifest
    const assetsRequests = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
        .map(asset => new Request(asset.url));
    //.map(asset => new Request(asset.url, { integrity: asset.hash }));
    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));
}

async function onActivate(event) {
    console.info('Service worker: Activate');
    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}

async function onFetch(event) {
    //if (event.request.method !== 'GET') {
    return fetch(event.request);
    //}

    //let cachedResponse = null;

    //// For all navigation requests, try to serve index.html from cache
    //// If you need some URLs to be server-rendered, edit the following check to exclude those URLs
    //const shouldServeIndexHtml = event.request.mode === 'navigate';

    //const request = shouldServeIndexHtml ? 'index.html' : event.request;
    //const cache = await caches.open(cacheName);
    //cachedResponse = await cache.match(request);

    //if (cachedResponse) {
    //    return cachedResponse;
    //}

    //var respuesta = await obtenerYActualizar(event);

    //return respuesta;
}


async function OnPush(event) {
    const payload = event.data.json();
    self.registration.showNotification(payload.titulo, {
        data: { url: payload.url }
    });
}

async function OnNotificationClick(event) {
    event.notification.close();
    clients.openWindow(event.notification.data.url);
}

async function obtenerYActualizar(event) {
    try {

        const respuesta = await fetch(event.request);
        const contentType = respuesta.headers.get('content-type');

        let salvarEnCache = true;

        if (contentType) {
            salvarEnCache = !contentType.includes('text/html');
            if (salvarEnCache) {
                salvarEnCache = !contentType.includes('application/json');
            }
        }

        if (salvarEnCache) {
            const cache = await caches.open(cacheNameDynamic);
            await cache.put(event.request, respuesta.clone());
        }

        return respuesta;
    }
    catch {
        // Si hay un error, entonces no pudimos establecer la conexión.
        const cache = await caches.open(cacheNameDynamic);
        return cache.match(event.request);
    }
}/* Manifest version: 27853+lb */
