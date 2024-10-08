// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
//self.addEventListener('fetch', () => { });


self.addEventListener('install', (event) => {
    console.log('Service worker installed');
    // Skip waiting to activate the new service worker immediately
    self.skipWaiting();
});

self.addEventListener('activate', (event) => {
    console.log('Service worker activated');
    clients.claim(); // Take control of all pages immediately
});

self.addEventListener('fetch', (event) => {
    event.respondWith(
        caches.match(event.request).then((response) => {
            return response || fetch(event.request);
        })
    );
});

self.addEventListener('message', (event) => {
    if (event.data && event.data.type === 'CHECK_FOR_UPDATE') {
        self.skipWaiting();
    }
});
